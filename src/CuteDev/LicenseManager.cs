using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace CuteDev
{

    public class UserInfo
    {
        public string Mac { get; set; }

        public string Cpu { get; set; }

        public string MusteriKodu { get; set; }

        public int AgentSayisi { get; set; }
    }
    
    public static class LicenseManager
    {
        
        public static void CheckLisans(UserInfo usr)
        {
            string mac = GetMac();
            string cpu = GetCpuID();

            if (usr == null || usr.Cpu != cpu || usr.Mac != mac || usr.AgentSayisi <= 0 || String.IsNullOrEmpty(usr.MusteriKodu) )
                throw new Exception("Lisans geçerli olmadığından işleminiz iptal edildi!");
        }

        public static void CheckAgentLicenseCount(decimal userCount)
        {

            UserInfo usrInfo = Config.Lisans;
            if (usrInfo.AgentSayisi < userCount)
                throw new Exception(String.Format("Lisansınız {0}  Agent için geçerlidir!",usrInfo.AgentSayisi));
        }

        public static string GetMac()
        {
            try
            {
                ManagementClass manager = new ManagementClass("Win32_NetworkAdapterConfiguration");
                foreach (ManagementObject obj in manager.GetInstances())
                {
                    if ((bool)obj["IPEnabled"])
                    {
                        return obj["MacAddress"].ToString();
                    }
                }
            }
            catch (Exception)
            {

                throw new Exception("Lisans kontrol işleminde hata oluştu. mac!");
            }

            return String.Empty;
        }

        public static string GetCpuID()
        {
            string sProcessorID = "";

            try
            {
                string sQuery = "SELECT ProcessorId FROM Win32_Processor";

                ManagementObjectSearcher oManagementObjectSearcher = new ManagementObjectSearcher(sQuery);

                ManagementObjectCollection oCollection = oManagementObjectSearcher.Get();

                foreach (ManagementObject oManagementObject in oCollection)
                {

                    sProcessorID = (string)oManagementObject["ProcessorId"];

                }
            }
            catch (Exception)
            {

                throw new Exception("Lisans kontrol işleminde hata oluştu. cpu!");
            }


            return (sProcessorID);

        }


    }
}
