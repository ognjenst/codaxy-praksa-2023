using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Scanning.Services
{
    public interface ISshClientService
    {
        public SshCommand ExecuteCommand(string command);
        public void Connect();
    }
}
