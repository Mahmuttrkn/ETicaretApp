using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Commands.Basket.DeleteItemToBasket
{
    public class DeleteItemToBasketCommandRequest : IRequest<DeleteItemToBasketCommandResponse>
    {
        public string basketItemId { get; set; }
    }
}
