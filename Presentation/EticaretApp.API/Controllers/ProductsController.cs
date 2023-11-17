
using EticaretApp.Application.Abstractions.Storage;
using EticaretApp.Application.Features.Commands.Product.CreateProduct;
using EticaretApp.Application.Features.Commands.Product.DeleteProduct;
using EticaretApp.Application.Features.Commands.Product.UpdateProduct;
using EticaretApp.Application.Features.Commands.UploadProduct.DeleteProductImage;
using EticaretApp.Application.Features.Commands.UploadProduct.UploadProductImage;
using EticaretApp.Application.Features.Queries.Product.GetAllProduct;
using EticaretApp.Application.Features.Queries.Product.GetProductID;
using EticaretApp.Application.Features.Queries.ProductImageFile.GetProductImage;
using EticaretApp.Application.Repositories;
using EticaretApp.Application.RequestParameters;
using EticaretApp.Application.ViewModuls.Products;
using EticaretApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EticaretApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
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

        private readonly IMediator _mediator;




        public ProductsController(IProductWriterRepository productWriterRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IFileWriterRepository writerRepository, IProductImageWriterRepository productImageWriterRepository, IInvoiceFileWriterRepository invoiceFileWriterRepository, IFileReadRepository fileReadRepository, IProductImageReadRepository productImageReadRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IStorageService storageService, IConfiguration configuration, IMediator mediator)
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
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
          GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }

        [HttpGet("{Id}")]

        public async Task<IActionResult> GetID([FromRoute]GetProductIDQueryRequest productIDQueryRequest)
        {

          GetProductIDQueryResponse response =  await _mediator.Send(productIDQueryRequest);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
           CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
           
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
            return Ok(response);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute]DeleteProductCommandRequest deleteProductCommandRequest)
        {
            DeleteProductCommandResponse response = await _mediator.Send(deleteProductCommandRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery,FromForm]UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {
            uploadProductImageCommandRequest.Files = Request.Form.Files;
            UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommandRequest);

            return Ok();
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetImages([FromRoute]GetProductImageQueryRequest productImageQueryRequest)
        {
            List<GetProductImageQueryResponse> response = await _mediator.Send(productImageQueryRequest);
            return Ok(response);


        }
        [HttpDelete("[action]/{id}")]

        public async Task<IActionResult> DeleteProductImage([FromRoute]DeleteProductImageCommandRequest productImageCommandRequest, [FromQuery] string imageId)
        {
            productImageCommandRequest.ImageId = imageId;
            DeleteProductImageCommandResponse response = await _mediator.Send(productImageCommandRequest);
            return Ok();
        }

    }
}
