using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat_th.Services
{
    public class NullMailServices : IMailService
    {
        private readonly ILogger<NullMailServices> _logger;
        public NullMailServices(ILogger<NullMailServices> logger)
        {
            _logger = logger;
        }
        public void SendMessage(string to,string subject,string body)
        {
            _logger.LogInformation($"To: {to} Subject: {subject} Body: {body}");
        }
    }
}
