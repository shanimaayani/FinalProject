//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using Project.Model;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel;
//using System.Security.Claims;
//using System.Text;
//using Azure.Core;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Principal;
//using Microsoft.AspNetCore.Http;
//using Newtonsoft.Json;

//namespace Project.Middleware
//{
//    public class AuthenticationMiddleware
//    {

//        private readonly RequestDelegate _next;
//        private readonly ILogger<AuthenticationMiddleware> _logger;

//        private static IConfiguration _config;
//        public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger, IConfiguration config)
//        {
//            _next = next;
//            _logger = logger;
//            _config = config;

//        }


//        public async Task InvokeAsync(HttpContext context)
//        {
//            try
//            {
//                var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
//                var handler = new JwtSecurityTokenHandler();
//                var b = context.Request.Headers["Authorization"].ToString();
//                var tokenSecure = handler.ReadToken(context.Request.Headers["Authorization"]) as SecurityToken;
//                var validations = new TokenValidationParameters
//                {
//                    ValidateIssuerSigningKey = true,
//                    IssuerSigningKey = new SymmetricSecurityKey(key),
//                    ValidateIssuer = false,
//                    ValidateAudience = false
//                };
//                var claims = handler.ValidateToken(b, validations, out tokenSecure);
//                var prinicpal = (ClaimsPrincipal)Thread.CurrentPrincipal;
//                User user = new User();
//                int a;
//                int.TryParse(claims.Claims.FirstOrDefault(x => x.Type == "PrimaryGroupSid")?.Value ?? "", out a);
//                user.Id = a;
//                bool role = Convert.ToBoolean(claims.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Role).Value ?? "");
//                user.IsAdmin = role;
//                //email = claims.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
//                //if (email != null)
//                //{
//                //    user.Email = email.Value;
//                //}
//                context.Items["User"] = user;
//                await _next(context);

//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "An error occurred in the middleware.");

//            }
//        }

        
//        }
//}
////var handler = new JwtSecurityTokenHandler();
////string authHeader = context.Request.Headers["Authorization"];
////authHeader = authHeader?.Replace("bearer ", "") ?? authHeader;
////var jsonToken = handler.ReadToken(authHeader) as JwtSecurityToken;
//////var identity = context.User.Identity as ClaimsIdentity;
//////var userClaims = identity.Claims;
////int f = 0;
////var user = new User
////{
////    Id = Convert.ToInt32(jsonToken.Claims.FirstOrDefault(o => o.Type == ClaimTypes.PrimaryGroupSid)?.Value),
////    Name = jsonToken.Claims.First(o => o.Type == ClaimTypes.Name)?.Value,
////    UserName = jsonToken.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
////    PhonNumber = jsonToken.Claims.FirstOrDefault(o => o.Type == ClaimTypes.OtherPhone)?.Value,
////    Email = jsonToken.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
////    Address = jsonToken.Claims.FirstOrDefault(o => o.Type == ClaimTypes.StreetAddress)?.Value,
////    IsAdmin = Convert.ToBoolean(jsonToken.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value)
////};
////var user2 = new User
////{
////    Id = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.PrimaryGroupSid)?.Value),
////    Name = userClaims.First(o => o.Type == ClaimTypes.Name)?.Value,
////    UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
////    PhonNumber = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.OtherPhone)?.Value,
////    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
////    Address = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.StreetAddress)?.Value,
////    IsAdmin = Convert.ToBoolean(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value)
////};
////var middlewareUser = context.Items["Authorization"] as User;
////context.Items["Authorization"] = user;

////await _next(context);