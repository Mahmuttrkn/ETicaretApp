using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Commands.Order.ShoppingCompleted
{
    public class ShoppingCompletedCommandRequest: IRequest<ShoppingCompletedCommandResponse>
    {
        public string Id { get; set; }
    }
}
