using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Queries.ProductImageFile.GetProductImage
{
    public class GetProductImageQueryRequest : IRequest<List<GetProductImageQueryResponse>>
    {
        public string Id { get; set; }
    
    }
}
