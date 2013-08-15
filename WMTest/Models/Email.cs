using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WMTest.Models
{
    public class Email
    {
        public int ID { get; set; }
        public User Sender { get; set; }
        public string Subject { get; set; }
        public User Recipient { get; set; }
        public string Body { get; set; }
        public DateTime SentDate { get; set; }
    }
}