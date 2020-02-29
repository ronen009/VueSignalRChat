using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace server.Logic
{
    public static class JwtHandler
    {
        //---- for jwt 
        private static string mPrivateKey = "A9mv&PpkD&rbZZkaQPB5$9%u#2ZpDPgFfG5^d%cu#9_E!?T?smwf^K&KZnt82+R&"; 
        private static string mIssuer = "Ronen009"; 
        private static string mAudience = "for chat users, validation for id";
        //----



        private static ClaimsIdentity CreateClaims(int iUserId , string iUserName , DateTime iDate) {

            ClaimsIdentity claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, iUserId.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Name, iUserName));
            claims.AddClaim(new Claim(ClaimTypes.Expiration , iDate.AddMinutes(60).ToString()));

            //claims.AddClaim(new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(any_object_To_Add)));
            return claims;
        }

        private static TokenValidationParameters GetValidParams() {

            var tokenParams = new TokenValidationParameters() {
                ValidIssuer = mIssuer,
                ValidAudience = mAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(mPrivateKey))
            };

            return tokenParams;
        }

        public static string CreateJwt(int iUserId, string iUserName, DateTime iDate) {

            var tokenCreator = new JwtSecurityTokenHandler();
            var claims = CreateClaims(iUserId, iUserName, iDate);

           
            SigningCredentials signCred = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.Default.GetBytes(mPrivateKey)),
                SecurityAlgorithms.HmacSha256Signature);


            var token = tokenCreator.CreateJwtSecurityToken(issuer: mIssuer, 
                                                            audience: mAudience, 
                                                            subject: claims , 
                                                            signingCredentials: signCred);

            var ret = tokenCreator.WriteToken(token);

            return ret;
        }

        public static bool IsTokenValid(string iToken , out int oUserID) {

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validParams = GetValidParams();

                SecurityToken validateToken;
                IPrincipal principal = tokenHandler.ValidateToken(iToken, validParams, out validateToken); // if fails thorws excpetion.

                oUserID = int.Parse(((System.Security.Claims.ClaimsPrincipal)principal).Claims.First().Value);

                return true;
            }
            catch (Exception ex) {
                //write to log
                oUserID = -1;
                return false;
            } 
        }   
    }
}
