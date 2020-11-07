using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Stratego.Reseau
{
	public class Reseau
	{

		public static IPAddress GetLocalIpAddress()
		{
			var host = Dns.GetHostEntry(Dns.GetHostName());

			return host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
		}
    }
}
