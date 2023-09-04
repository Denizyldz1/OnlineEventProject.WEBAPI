

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineEvent.Abstract.Services;
using OnlineEvent.Core.Configurations;
using OnlineEvent.Core.Entities;
using OnlineEvent.Model;
using OnlineEvent.Model.TokenModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace OnlineEvent.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly CustomTokenOption _tokenOption;

        public TokenService(UserManager<AppUser> userManager,IOptions<CustomTokenOption> options)
        {
            _userManager = userManager;
            _tokenOption = options.Value;
        }
        // Refresh token üretmek için random olarak
        private string CreateRefreshToken()
        {
            //return Guid.NewGuid().ToString();
            // Microsoft'un kütüphanesi ile üreteceğiz. Guid'den daha uniq üretim için
            var numberByte = new Byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte);
        }
        private IEnumerable<Claim>GetClaims(AppUser appUser,List<String> audiences) 
        {
            // Proje'de her kullanıcı için bir role mevcut
            var role = _userManager.GetRolesAsync(appUser);
            role.Wait();
            var userList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,appUser.Id.ToString()),
                new Claim(ClaimTypes.Email,appUser.Email),
                // Isteklerde role onemli olduğu için kullanıcı role ekledik
                new Claim(ClaimTypes.Role,role.Result.First()),
                //Her token için token ıd veriyoruz
               new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                // UserName kaldırabiliriz projemiz de userName null getirebiliriz.
                new Claim(ClaimTypes.Name,appUser.UserName)
            };

            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return userList;

        }

        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim>();

            claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString());

            return claims;


        }
        public ClientTokenModel CreateClientToken(Client client)
        {
            // Token süresi
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);

            //  Token imzası
            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
                (
                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaimsByClient(client),
                signingCredentials: signingCredentials
                );
            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);
            var tokenDto = new ClientTokenModel()
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpiration,
            };

            return tokenDto;
        }

        public TokenModel CreateToken(AppUser user)
        {
            // Token süresi
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);

            //  Token imzası
            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
                (
                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaims(user, _tokenOption.Audience),
                signingCredentials: signingCredentials
                );
            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);
            var tokenDto = new TokenModel()
            {
                AccessToken = token,
                RefreshToken=CreateRefreshToken(),
                AccessTokenExpiration=accessTokenExpiration,
                RefreshTokenExpiration= refreshTokenExpiration
            };

            return tokenDto;

        }
    }
}
