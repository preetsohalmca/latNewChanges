using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Volvo.NVS.Core.Configuration;
using Volvo.NVS.Logging;

namespace Volvo.LAT.MVCWebUIComponent.App_Start.Logging
{
    public static class NVSLogging
    {
        public static void Configure()
        {
            LibraryConfigurator.Current.ConfigureNvsLogging(builder => builder
                .SetLevel("trace")
                .SetFolderName("logs/")
                .SetSystem("Volvo/NVS/POS"));
        }
    }
}