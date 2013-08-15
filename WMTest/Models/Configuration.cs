using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WMTest.Models
{
    public class Configuration
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}