using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Linq;

namespace GlobalShopping.Lib
{
    public class NetworkHelper
    {
        public static bool IsPortInUse(int port)
        {
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

            if (ipEndPoints.Any(endPoint => endPoint.Port == port))
            {
                return true;
            }

            IPEndPoint[] ipUdpEndPoints = ipProperties.GetActiveUdpListeners();


            return false;
        }
    }
}
