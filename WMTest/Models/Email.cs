using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace WMTest.Models
{
    public class Email
    {
        public int ID { get; set; }
        public User Sender { get; set; }
        public string Subject { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email address is invalid.")]
        public string Recipient { get; set; }

        public string Body { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy hh:mm:ss}")]
        
        public DateTime SentDate { get; set; }

        public string Attachments{ get; set; }
    }
}