using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestService.Models
{
    public class AuthenticateRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}