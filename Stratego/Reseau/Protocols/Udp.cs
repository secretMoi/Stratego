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

		public Udp()
		{
			_udp = new UdpClient();
			Token = Reseau.CreateToken();
		}

		public async Task SendAsync(IModelReseau model, IPEndPoint destination)
		{
			byte[] data = Serialise.ObjectToByteArray(model);

			await _udp.SendAsync(data, data.Length, destination);
		}
	}
}
