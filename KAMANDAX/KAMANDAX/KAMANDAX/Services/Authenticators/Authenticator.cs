using KAMANDAX.JWT;
using KAMANDAX.Models;
using KAMANDAX.Models.Responses;
using KAMANDAX.Services.RefreshTokenRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAMANDAX.Services.Authenticators
{
    public class Authenticator
    {
        private readonly JwtGenerator _jwtGenerator;
        private readonly RefreshGenerator _refreshGenerator;
        private readonly RefreshTokenService _refreshTokenService;

        public Authenticator(JwtGenerator jwtGenerator, 
            RefreshGenerator refreshGenerator, 
            RefreshTokenService refreshTokenService)
        {
            _jwtGenerator = jwtGenerator;
            _refreshGenerator = refreshGenerator;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<AuthenticatedUserResponse> Authenticate(User user)
        {
            string accessToken = _jwtGenerator.GenerateToken(user);
            string refreshToken = _refreshGenerator.GenerateToken();

            RefreshToken rToken = new RefreshToken()
            {
                Token = refreshToken,
                UserId = user.Id
            };

            await _refreshTokenService.Create(rToken);

            return new AuthenticatedUserResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
