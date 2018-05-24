using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Validator
    {
        public bool validateData(user usr)
        {
            if(usr.address == "")
                return false;
            if(usr.age <= 0)
                return false;
            if (usr.dob == null)
                return false;
            if (usr.email == "")
                return false;
            if (usr.imageName == "")
                return false;
            if (usr.login == "")
                return false;
            if (usr.name == "")
                return false;
            if (usr.nic == "")
                return false;
            if (usr.passwd == "")
                return false;
            return true;
        }
    }
}
