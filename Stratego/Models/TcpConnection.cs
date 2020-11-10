using Stratego.Reseau.Clients;
using Stratego.Reseau.Serveurs;

namespace Stratego.Models
{
	public class TcpConnection
	{
		public ServerTcpController Server { get; set; }
		public ClientTcpController Client { get; set; }
		public TcpType Type { get; set; }

		public enum TcpType
		{
			Server,
			Client
		};
	}
}
