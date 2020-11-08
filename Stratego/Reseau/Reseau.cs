using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Stratego.Reseau
{
	public class Reseau
	{

		/**
		 * <summary>Obtiens l'adresse ip locale de la machine</summary>
		 * <returns>Retourne une <see cref="IPAddress"/></returns>
		 */
		public static IPAddress GetLocalIpAddress()
		{
			var host = Dns.GetHostEntry(Dns.GetHostName());

			return host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
		}

		/**
		 * <summary>Crée un token unique</summary>
		 */
		public static string CreateToken()
		{
			byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
			byte[] key = Guid.NewGuid().ToByteArray();
			return Convert.ToBase64String(time.Concat(key).ToArray());
		}
    }
}
