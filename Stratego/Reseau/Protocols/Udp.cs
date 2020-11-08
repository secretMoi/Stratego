using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Stratego.Reseau.Models;

namespace Stratego.Reseau.Protocols
{
	public class Udp
	{
		private UdpClient _udp;

		public string Token { get; }
		public UdpClient Connection => _udp;

		public Udp(int port)
		{
			OpenPort(port);

			Token = Reseau.CreateToken();
		}

		private void OpenPort(int startPort)
		{
			int tentatives = -1;
			bool connected = false;
			while (tentatives < 5 && connected == false)
			{
				try
				{
					tentatives++;
					_udp = new UdpClient(startPort + tentatives);
					connected = true;
					startPort += tentatives;
				}
				catch (Exception e)
				{
					Console.WriteLine(@"Impossible d'ouvrir un socket UDP " + e.Message);
				}
			}
		}

		public async Task SendAsync(IModelReseau model, IPEndPoint destination)
		{
			byte[] data = Serialise.ObjectToByteArray(model);

			await _udp.SendAsync(data, data.Length, destination);
		}
	}
}
