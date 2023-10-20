
using EticaretApp.Application.Abstractions.Storage;
using EticaretApp.Application.Repositories;
using EticaretApp.Application.RequestParameters;
using EticaretApp.Application.ViewModuls.Products;
using EticaretApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        readonly IConfiguration _configuration;




        public ProductsController(IProductWriterRepository productWriterRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IFileWriterRepository writerRepository, IProductImageWriterRepository productImageWriterRepository, IInvoiceFileWriterRepository invoiceFileWriterRepository, IFileReadRepository fileReadRepository, IProductImageReadRepository productImageReadRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IStorageService storageService, IConfiguration configuration)
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
            _configuration = configuration;
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

            return Ok(await _productReadRepository.GetByIdAsync(id, false));
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
        public async Task<IActionResult> Upload(string id)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UpluoadAsync("photo-images", Request.Form.Files);

            Product product = await _productReadRepository.GetByIdAsync(id);

            

            await _productImageWriterRepository.AddRangeAsync(result.Select(r => new ProductImageFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainerName,
                Storage = _storageService.StorageName,
               // CreateDate = DateTime.Now,
                Products = new List<Product>() { product }
            }).ToList());

            await _productImageWriterRepository.SaveAsync();

            return Ok();
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetImages(string id)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));


            return Ok(product.ProductImageFiles.Select(p => new
            {
                Path= $"{_configuration["BaseStorageUrl"]}/{p.Path}",
                p.FileName
            }));


        }

    }
}
