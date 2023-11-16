using EticaretApp.Application.Repositories;
using EticaretApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EticaretApp.Application.Features.Commands.UploadProduct.DeleteProductImage
{
    public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommandRequest, DeleteProductImageCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductImageWriterRepository _productImageWriterRepository;

        public DeleteProductImageCommandHandler(IProductReadRepository productReadRepository, IProductImageWriterRepository productImageWriterRepository)
        {
            _productReadRepository = productReadRepository;
            _productImageWriterRepository = productImageWriterRepository;
        }

        public async Task<DeleteProductImageCommandResponse> Handle(DeleteProductImageCommandRequest request, CancellationToken cancellationToken)
        {
           EticaretApp.Domain.Entities.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            EticaretApp.Domain.Entities.ProductImageFile? productImageFile = product?.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));


            if (productImageFile != null)
                product?.ProductImageFiles.Remove(productImageFile);

            await _productImageWriterRepository.SaveAsync();
            return new();
        }
    }
}
