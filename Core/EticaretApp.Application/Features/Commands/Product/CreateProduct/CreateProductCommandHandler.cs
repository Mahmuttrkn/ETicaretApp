using EticaretApp.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWriterRepository _productWriterRepository;

        public CreateProductCommandHandler(IProductWriterRepository productWriterRepository)
        {
            _productWriterRepository = productWriterRepository;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriterRepository.AddAsync(new()
            {

                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            });
            await _productWriterRepository.SaveAsync();
            return new();
        }
    }
}
