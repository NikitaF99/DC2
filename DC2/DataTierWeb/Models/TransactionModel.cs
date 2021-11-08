using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataTierWeb.Models
{
    public class TransactionModel
    {
        public uint id { get; set; }
        public uint amount { get; set; }

        public uint sender { get; set; }
        public uint receiver { get; set; }
    }
}