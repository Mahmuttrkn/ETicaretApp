using EticaretApp.Application.Abstractions.Services;
using EticaretApp.Application.DTO_s.User;
using EticaretApp.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EticaretApp.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
          CreateUserResponseDTO responseDTO = await _userService.CreateAync(new()
            {
                Email = request.Email,
                NameSurname=request.NameSurname,
                Username = request.Username,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm
                
            });
            return new()
            {
                Message = responseDTO.Message,
                Succeeded = responseDTO.Succeeded,
            };
        }
    }
}
