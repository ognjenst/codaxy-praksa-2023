using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Notifications.Services
{
    public interface ISlackService
    {
        Task SendMessage(string message);
    }
}
