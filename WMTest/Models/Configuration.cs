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

        [Required(ErrorMessage = "Parameter Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Parameter Value is required")]
        public string Value { get; set; }
    }
}