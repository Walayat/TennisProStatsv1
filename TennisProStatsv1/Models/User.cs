using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TennisProStatsv1.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }
        public UserType UserType { get; set; }
        public bool IsValid { get; set; }
    }
}