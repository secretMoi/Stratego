using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Stratego.Reseau.Models;

namespace Stratego.Reseau.Serveurs
{
	public class ServeurController
	{
		private const int Port = 32430;
		private UdpClient _broadcastServer;

		private bool _state = true;
		private readonly object cadenas = new object();

		public bool State
		{
			get => _state;
			set
			{
				lock (cadenas)
				{
					_state = value;
				}

				if (value)
				{
					Console.WriteLine(@"Server is ON");
				}
				else
				{
					_broadcastServer.Close();
					Console.WriteLine(@"Server is OFF");
				}
			}
		}

		public async Task ReceiveBroadCastAsync(Action<InitModel> callback = null)
		{
			_broadcastServer = new UdpClient(Port);

			while (State)
			{
				UdpReceiveResult clientData;
				try
				{
					clientData = await _broadcastServer.ReceiveAsync();
					var clientRequest = Serialise.ByteArrayToObject<InitModel>(clientData.Buffer);

					Console.WriteLine(
						$@"Received {clientRequest.MachineName} from {clientRequest.Address.Address}:{clientRequest.Address.Port}");

					callback?.Invoke(clientRequest);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
		}
	}
}
