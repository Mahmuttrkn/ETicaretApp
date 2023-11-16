using EticaretApp.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Commands.Product.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        private readonly IProductWriterRepository _productWriterRepository;

        public DeleteProductCommandHandler(IProductWriterRepository productWriterRepository)
        {
            _productWriterRepository = productWriterRepository;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriterRepository.RemoveAsync(request.Id);
            await _productWriterRepository.SaveAsync();
            return new();
        }
    }
}
