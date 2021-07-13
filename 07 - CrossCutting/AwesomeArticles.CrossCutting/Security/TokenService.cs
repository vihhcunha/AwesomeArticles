using AutoMapper.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AwesomeArticles.CrossCutting.Security
{
    public class TokenService : ITokenService
    {
        private static RandomNumberGenerator Rng = RandomNumberGenerator.Create();
        private static readonly string MyJwkLocation = Path.Combine(Environment.CurrentDirectory, "mysecretkey.json");

        public Token GerarToken(Guid idUser, string email, string name)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Loadkey();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Email, email),
                    new Claim("Id", idUser.ToString())
                }),
                Expires = DateTime.Now.AddHours(9),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            Token token = new Token();
            token.TokenJWT = tokenHandler.WriteToken(securityToken);
            token.DataExpiracao = tokenDescriptor.Expires.Value;

            return token;
        }

        private static SecurityKey Loadkey()
        {
            if (File.Exists(MyJwkLocation))
                return JsonSerializer.Deserialize<JsonWebKey>(File.ReadAllText(MyJwkLocation));

            var newKey = CreateJWK();
            File.WriteAllText(MyJwkLocation, JsonSerializer.Serialize(newKey));
            return newKey;
        }

        private static JsonWebKey CreateJWK()
        {
            var symetricKey = new HMACSHA256(GenerateKey(64));
            var jwk = JsonWebKeyConverter.ConvertFromSymmetricSecurityKey(new SymmetricSecurityKey(symetricKey.Key));
            jwk.KeyId = Base64UrlEncoder.Encode(GenerateKey(16));
            return jwk;
        }

        private static byte[] GenerateKey(int bytes)
        {
            var data = new byte[bytes];
            Rng.GetBytes(data);
            return data;
        }
    }
}
