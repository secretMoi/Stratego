using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Stratego.Reseau.Models;

namespace Stratego.Reseau.Protocols
{
	public class Udp
	{
		private readonly UdpClient _udp;

		public string Token { get; }
		public UdpClient Connection => _udp;

		public Udp(int port)
		{
			//_udp = new UdpClient();
			int tentatives = -1;
			bool connected = false;
			while (tentatives < 5 && connected == false)
			{
				try
				{
					tentatives++;
					_udp = new UdpClient(port + tentatives);
					connected = true;
					port += tentatives;
				}
				catch (Exception e)
				{
					Console.WriteLine(@"Impossible d'ouvrir un socket UDP " + e.Message);
				}
			}

			Token = Reseau.CreateToken();
		}

		public async Task SendAsync(IModelReseau model, IPEndPoint destination)
		{
			byte[] data = Serialise.ObjectToByteArray(model);

			await _udp.SendAsync(data, data.Length, destination);
		}
	}
}
