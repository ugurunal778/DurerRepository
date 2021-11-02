using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Facade
{
    public class UserFacade : FacadeBase, IUserFacade
    {
        public bool CheckLoginUser(string username, string pass)
        {
            username = username.ToLower();
            pass = pass.ToLower();
            var value = EntityModel.User.Any(x => x.Username == username && x.Password == pass);
            return value;
        }
    }
}
