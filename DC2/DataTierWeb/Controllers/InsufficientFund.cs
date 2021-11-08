using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DataTierWeb.Controllers
{
    //exception to be thrown when there is insufficient fund
    [DataContract]
    public class InsufficientFund
    {

        [DataMember]
        public string Issue { get; set; }
    }
}