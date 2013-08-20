using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMTest.Models
{
    public class Chart
    {
        public Chart(string data, int val)
        {
            Data = data;
            Val= val;
        }

        public string Color { get; set; }
        public string Data { get; set; }
        public int Val { get; set; }
    }
}