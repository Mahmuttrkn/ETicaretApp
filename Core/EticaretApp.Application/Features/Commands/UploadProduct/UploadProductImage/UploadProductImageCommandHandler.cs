using EticaretApp.Application.Abstractions.Storage;
using EticaretApp.Application.Repositories;
using EticaretApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Commands.UploadProduct.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
       private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriterRepository _productWriterRepository;
        private readonly IStorageService _storageService;
        private readonly IProductImageWriterRepository _productImageWriterRepository;

        public UploadProductImageCommandHandler(IProductReadRepository productReadRepository, IProductWriterRepository productWriterRepository, IStorageService storageService, IProductImageWriterRepository productImageWriterRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriterRepository = productWriterRepository;
            
            _storageService = storageService;
            _productImageWriterRepository = productImageWriterRepository;
        }

        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {

            List<(string fileName, string pathOrContainerName)> result = await _storageService.UpluoadAsync("photo-images", request.Files);

            EticaretApp.Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);



            await _productImageWriterRepository.AddRangeAsync(result.Select(r => new ProductImageFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainerName,
                Storage = _storageService.StorageName,
                // CreateDate = DateTime.Now,
                Products = new List<EticaretApp.Domain.Entities.Product>() { product }
            }).ToList());

            await _productImageWriterRepository.SaveAsync();
            return new();
        }
    }
}
