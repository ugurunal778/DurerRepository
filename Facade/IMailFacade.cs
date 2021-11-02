using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.ServiceModel;

namespace Facade
{
    [ServiceContract]
    public interface IMailFacade
    {
        [OperationContract]
        void SendMail(string body, string fullName, string title);
    }
}
