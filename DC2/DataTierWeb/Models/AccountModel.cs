using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataTierWeb.Models
{
    public class AccountModel
    {
        public uint id { get; set; }
        public uint amount { get; set; }

        public uint userId { get; set; }
    }
}