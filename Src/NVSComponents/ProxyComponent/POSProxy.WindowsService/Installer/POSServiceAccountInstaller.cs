using System.Diagnostics.CodeAnalysis;
using Volvo.NVS.Utilities.WindowsServices.Services;

namespace Volvo.POS.Proxy.WindowsService.Installer
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class POSServiceAccountInstaller : NVSWinServiceInstaller
    {
        /*public POSServiceAccountInstaller()
        {
            //SetAccount(ServiceAccount.User, @"vcn\user", "password");
        }*/
    }
}