using EticaretApp.Application.DTO_s.User;
using EticaretApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDTO> CreateAync(CreateUserDTO model);
        Task UpdateRefreshToken(string refreshToken, AppUser appUser, DateTime accessTokenDate, int refreshTokenLifeTime);
    }
}
