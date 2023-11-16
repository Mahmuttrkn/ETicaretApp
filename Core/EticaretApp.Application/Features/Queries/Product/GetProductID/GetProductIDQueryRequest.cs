using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Queries.Product.GetProductID
{
    public class GetProductIDQueryRequest : IRequest<GetProductIDQueryResponse>
    {
        public string Id { get; set; }
    }
}
