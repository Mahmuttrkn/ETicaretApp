using EticaretApp.Application.Abstractions.Services;
using EticaretApp.Application.DTO_s;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Commands.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
    {
        private readonly IAuthService _authService;

        public RefreshTokenLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
        {
          Token token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
            return new RefreshTokenLoginCommandResponse()
            {
                Token = token,
            };
        }
    }
}
