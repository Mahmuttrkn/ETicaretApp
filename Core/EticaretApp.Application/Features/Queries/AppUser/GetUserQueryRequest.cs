using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Queries.AppUser
{
    public class GetUserQueryRequest: IRequest<GetUserQueryResponse>
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
