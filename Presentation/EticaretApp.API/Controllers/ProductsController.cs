
using EticaretApp.Application.Abstractions.Storage;
using EticaretApp.Application.Repositories;
using EticaretApp.Application.RequestParameters;
using EticaretApp.Application.ViewModuls.Products;
using EticaretApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EticaretApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriterRepository _productWriterRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileWriterRepository _writerRepository;
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IProductImageWriterRepository _productImageWriterRepository;
        private readonly IProductImageReadRepository _productImageReadRepository;
        private readonly IInvoiceFileWriterRepository _invoiceFileWriterRepository;
        private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        private readonly IStorageService _storageService;




        public ProductsController(IProductWriterRepository productWriterRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IFileWriterRepository writerRepository, IProductImageWriterRepository productImageWriterRepository, IInvoiceFileWriterRepository invoiceFileWriterRepository, IFileReadRepository fileReadRepository, IProductImageReadRepository productImageReadRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IStorageService storageService)
        {
            _productWriterRepository = productWriterRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _writerRepository = writerRepository;
            _productImageWriterRepository = productImageWriterRepository;
            _invoiceFileWriterRepository = invoiceFileWriterRepository;
            _fileReadRepository = fileReadRepository;
            _productImageReadRepository = productImageReadRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _storageService = storageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            await Task.Delay(1500);
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreateDate,
                p.UpdateDate

            }).ToList();
            return Ok(new
            {
                totalCount,
                products
            });
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetID(string id)
        {
           
            return Ok(await _productReadRepository.GetByIdAsync(id,false));
        }


        [HttpPost]
        public async Task<IActionResult> Post(Product_Create_ViewModel model)
        {
           
            await _productWriterRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            });
            await _productWriterRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Put(Product_Update_ViewModel model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Price = model.Price;
            product.Name = model.Name;
            await _productWriterRepository.SaveAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriterRepository.RemoveAsync(id);
            await _productWriterRepository.SaveAsync();
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
          var datas = await _storageService.UpluoadAsync("resource/files", Request.Form.Files);
            await _productImageWriterRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.pathOrContainerName,
                CreateDate = DateTime.Now,
                Storage = _storageService.StorageName

            }).ToList());
            await _productImageWriterRepository.SaveAsync();
            return Ok();
        }
    }
}
