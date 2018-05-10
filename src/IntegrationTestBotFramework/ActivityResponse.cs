using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestBotFramework
{
    class ActivityResponse
    {
        public List<Activity> activities { get; set; }
        public string watermark { get; set; }
    }
}
