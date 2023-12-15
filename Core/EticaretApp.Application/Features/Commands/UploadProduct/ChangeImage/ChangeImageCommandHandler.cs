using EticaretApp.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Commands.UploadProduct.ChangeImage
{
    public class ChangeImageCommandHandler : IRequestHandler<ChangeImageCommandRequest, ChangeImageCommandResponse>
    {
        private readonly IProductImageWriterRepository _productImageWriterRepository;

        public ChangeImageCommandHandler(IProductImageWriterRepository productImageWriterRepository)
        {
            _productImageWriterRepository = productImageWriterRepository;
        }

        public async Task<ChangeImageCommandResponse> Handle(ChangeImageCommandRequest request, CancellationToken cancellationToken)
        {
            var query = _productImageWriterRepository.Table.Include(p => p.Products)
                .SelectMany(p => p.Products, (pif, p) =>
                new
                {
                    pif,
                    p
                });

            var data = await query.FirstOrDefaultAsync(p => p.p.Id == Guid.Parse(request.productId) && p.pif.Showcase);

            data.pif.Showcase = false;

            var image = await query.FirstOrDefaultAsync(p => p.pif.Id == Guid.Parse(request.productId));

            image.pif.Showcase = true;


          await  _productImageWriterRepository.SaveAsync();

            return new();
            
        }
    }
}
