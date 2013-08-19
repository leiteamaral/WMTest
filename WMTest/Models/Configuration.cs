using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WMTest.Models
{
    public class Configuration
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Server { get; set; }

        [Required]
        public int Port { get; set; }

        [Required]
        public bool SSL { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }
    }
}