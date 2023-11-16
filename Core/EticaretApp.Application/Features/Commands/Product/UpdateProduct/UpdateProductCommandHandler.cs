using EticaretApp.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriterRepository _productWriterRepository;

        public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriterRepository productWriterRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriterRepository = productWriterRepository;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            EticaretApp.Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);
            product.Stock = request.Stock;
            product.Name = request.Name;
            product.Price = request.Price;
            await _productWriterRepository.SaveAsync();
            return new();

        }
    }
}
    

