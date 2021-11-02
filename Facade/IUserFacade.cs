using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    [ServiceContract]
    public interface IUserFacade
    {
        [OperationContract]
        bool CheckLoginUser(string userName, string pass);
    }
}
