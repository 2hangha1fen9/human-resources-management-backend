using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Common
{
    /// <summary>
    /// Jwt工具类
    /// </summary>
    public static class JwtHelper
    {
        /// <summary>
        /// 颁发者
        /// </summary>
        private static readonly string ISSUER = "HumanResourcesManagementBackend.Api";
        /// <summary>
        /// 使用范围
        /// </summary>
        private static readonly string AUDIENCE = "ALL";
        /// <summary>
        /// 解密密钥
        /// </summary>
        private static readonly string KEY = "dfkRh{+oHWB]6,YF}+)4[)O[LH]b9dq7";
        /// <summary>
        /// Jwt验证参数
        /// </summary>
        private static readonly TokenValidationParameters CONFIG = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = ISSUER,
            ValidAudience = AUDIENCE,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY)),
        };

        /// <summary>
        /// 验证Token是否有效并返回自定义的信息(无效抛出异常)
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Token自定义内容</returns>
        public static string VerifyWithPayLoad(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, CONFIG, out SecurityToken validatedToken);
            return principal.Claims.FirstOrDefault(c => c.Type == "payLoad").Value;
        }

        /// <summary>
        /// 验证Token是否有效
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool Verify(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, CONFIG, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 颁发Token
        /// </summary>
        /// <param name="body">Token自定义内容体</param>
        /// <param name="periodValidity">有效期（默认60分钟）</param>
        /// <returns></returns>
        public static string Publish(string payLoad = "", int periodValidity = 60)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(KEY);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = ISSUER,
                    Audience = AUDIENCE,
                    Subject = new ClaimsIdentity(new[] { new Claim("payLoad", payLoad) }),
                    Expires = DateTime.UtcNow.AddMinutes(periodValidity), // 过期时间
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                return tokenString;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
