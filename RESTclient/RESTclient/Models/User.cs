﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTclient.Models
{
    class User
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
