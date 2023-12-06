using EticaretApp.Application.Abstractions.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Infsrastructure.Services2.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public Application.DTO_s.Token CreateAccessToken()
        {
            EticaretApp.Application.DTO_s.Token token = new();

            //SecurityKey'in simetriğini alıyoruz.
            SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            //Şifrelenmiş kimliği oluşturuyoruz.
            SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            //Oluşturulacak token ayarlarını veriyoruz.
            token.Expiration = DateTime.UtcNow.AddMinutes(10);
            JwtSecurityToken jwtSecurityToken = new(
                audience : _configuration["Token:Audience"],
                issuer : _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials : signingCredentials
                );

            //Token oluşturucu sınıfından bir örnek alalım.
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
          token.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            //string refreshToken = CreateRefreshToken();
            token.RefreshToken = CreateRefreshToken();

            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
           using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);

            return Convert.ToBase64String(number);
        }
    }
}
