using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WMTest.Models
{
    public class User
    {
        public int ID { get; set; }
        
        [StringLength(50)]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        
        [StringLength(20)]
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        
        [StringLength(50)]
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email address is invalid.")]
        public string Email { get; set; }
        
        [StringLength(20)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }

}