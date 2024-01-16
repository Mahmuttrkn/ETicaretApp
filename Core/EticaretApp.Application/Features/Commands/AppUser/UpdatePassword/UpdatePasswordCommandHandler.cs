using EticaretApp.Application.Abstractions.Services;
using EticaretApp.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Commands.AppUser.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
    {
        private readonly IUserService _userService;

        public UpdatePasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.Password != request.PasswordConfirm)
            {
                throw new PasswordChangeFailedException("Şifreler eşleşmiyor.");
            }
           await _userService.UpdatePasswordAsync(request.UserId,request.ResetToken,request.Password);
            return new();
        }
    }
}
