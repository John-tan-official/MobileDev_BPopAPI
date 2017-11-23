using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BPopAPI.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Team { get; set; }
        public string Points { get; set; }
    }
}