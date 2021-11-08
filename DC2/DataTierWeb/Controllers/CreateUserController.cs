using BankDB;
using DataTierWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DataTierWeb.Controllers
{
    public class CreateUserController : ApiController
    {
        //cretaing object of bandBD.
        public static BankDB.BankDB Instance { get; } = new BankDB.BankDB();
        //To access the functions provided under userAccessInterfaces 
        private static UserAccessInterface user = Instance.GetUserAccess();

  

        // POST: api/CreateUser
        public uint Post([FromBody]UsersModel value)
        {
            //creating a user, and setting the firstname and lastname of respective user.
            uint userID = user.CreateUser();
            user.SelectUser(userID);
            user.SetUserName(value.fname, value.lname);
            Instance.SaveToDisk();

            return userID;
        }


        //all the other functions which are not used in this project

        /*
        // GET: api/CreateUser
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CreateUser/5
        public string Get(int id)
        {
            return "value";
        }
        // PUT: api/CreateUser/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CreateUser/5
        public void Delete(int id)
        {
        }*/
    }
}
