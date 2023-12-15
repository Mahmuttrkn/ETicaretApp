using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Commands.UploadProduct.ChangeImage
{
    public class ChangeImageCommandRequest: IRequest<ChangeImageCommandResponse>
    {
        public string imageId { get; set; }
        public string productId { get; set; }

    }
}
