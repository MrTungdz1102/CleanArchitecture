﻿using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;

namespace CleanArchitecture.WebUI.Services.Implementations
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _accessor;
        public TokenProvider(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public void ClearToken()
        {
            _accessor.HttpContext?.Response.Cookies.Delete(Constants.TokenCookie);
        }

        public string? GetToken()
        {
            string? token = null;
            bool? hasToken = _accessor.HttpContext?.Request.Cookies.TryGetValue(Constants.TokenCookie, out token);
            return hasToken is true ? token : null;
        }

        public void SetToken(string token)
        {
            _accessor.HttpContext?.Response.Cookies.Append(Constants.TokenCookie, token);
        }
    }
}
