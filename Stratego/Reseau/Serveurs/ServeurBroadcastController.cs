using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Stratego.Reseau.Models;

namespace Stratego.Reseau.Serveurs
{
	public class ServeurBroadcastController
	{
		private int port;
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
					_broadcastServer?.Close();
					Console.WriteLine(@"Server is OFF");
				}
			}
		}

		public ServeurBroadcastController(int port = 32430)
		{
			this.port = port;
		}

		public async Task ReceiveBroadCastAsync(Action<InitModel> callback = null)
		{
			int tentatives = -1;
			bool connected = false;
			while (tentatives < 5 && connected == false)
			{
				try
				{
					tentatives++;
					_broadcastServer = new UdpClient(port + tentatives);
					connected = true;
					port += tentatives;
				}
				catch (Exception e)
				{
					Console.WriteLine(@"Impossible d'ouvrir un serveur UDP " + e.Message);
				}
			}

			while (State)
			{
				UdpReceiveResult clientData;
				try
				{
					clientData = await _broadcastServer.ReceiveAsync();
					var clientRequest = Serialise.ByteArrayToObject<InitModel>(clientData.Buffer);

					Console.WriteLine(
						$@"Received {clientRequest.MachineName} from {clientRequest.Address.Address}:{clientRequest.Address.Port}");

					//await RespondAsync(clientRequest.Address);
					callback?.Invoke(clientRequest);
				}
				catch (Exception e)
				{
					Console.WriteLine(@"Impossible de recevoir des données UDP " + e.Message);
				}
			}
		}
	}
}
