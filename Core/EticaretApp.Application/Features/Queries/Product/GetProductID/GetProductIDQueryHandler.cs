using EticaretApp.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EticaretApp.Domain.Entities;

namespace EticaretApp.Application.Features.Queries.Product.GetProductID
{

    public class GetProductIDQueryHandler : IRequestHandler<GetProductIDQueryRequest, GetProductIDQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetProductIDQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetProductIDQueryResponse> Handle(GetProductIDQueryRequest request, CancellationToken cancellationToken)
        {
          EticaretApp.Domain.Entities.Product product =  await _productReadRepository.GetByIdAsync(request.Id, false);
            return new()
            {
                Name= product.Name,
                Price= product.Price,
                Stock=product.Stock
            };
        }
    }
}
