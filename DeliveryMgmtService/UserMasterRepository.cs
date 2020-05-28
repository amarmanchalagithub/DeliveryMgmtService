using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;

namespace DeliveryMgmtService
{
    public class UserMasterRepository 
    {

        public bool ValidateUser(string username, string password)
        {
            if (username == "amarnath" && password == "Am@rn2t8")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}