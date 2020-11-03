using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace CookieService
{
    public interface ICookieSvc
    {
        void SetCookie(string key, string value, int? expireTime, bool isSecure, bool isHttpOnly);
        void SetCookie(string key, string value, int? expireTime);
        void DeleteCookie(string key);
        void DeleteAllCookies(IEnumerable<string> cookiesToDelete);
        string Get(string key);
        string GetUserIP();
        string GetUserCountry();
        string GetUserOS();
    }
}
