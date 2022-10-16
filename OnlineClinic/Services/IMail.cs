using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineClinic.Services
{
    public interface IMail
    {
        bool SendEmail(string recipient,string subject,string body);
    }
}
