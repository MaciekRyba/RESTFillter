using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace RestService.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public User(int id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }
    }
}