using EticaretApp.Application.Abstractions.Hubs;
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
        private readonly IProductHubService _productHubService;

        public CreateProductCommandHandler(IProductWriterRepository productWriterRepository, IProductHubService productHubService)
        {
            _productWriterRepository = productWriterRepository;
            _productHubService = productHubService;
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
            await _productHubService.ProductEditMessageAsync($"{request.Name} isminde ürün eklenmiştir.");
            return new();
        }
    }
}
