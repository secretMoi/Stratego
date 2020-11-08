using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Timers;
using Stratego.Reseau.Models;
using Stratego.Reseau.Protocols;

namespace Stratego.Reseau
{
	public class Broadcast
	{
		private readonly Udp _udp;
		private Timer _timer;
		private IModelReseau _data;
		private int _destinationPort;

		private bool _state = true;
		private readonly object cadenas = new object();

		public Udp Udp => _udp;

		public bool ServerState
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
					_udp?.Connection.Close();
					Console.WriteLine(@"Server is OFF");
				}
			}
		}

		public Broadcast(Udp udp)
		{
			_udp = udp;
		}

		/**
		 * <summary>Démarrve un serveur recevant des messages broadcastés</summary>
		 * <param name="callback">Fonction à appeler lorsque le serveur recoit un message</param>
		 */
		public async Task ReceiveBroadCastAsync(Action<InitModel> callback = null)
		{
			while (ServerState)
			{
				UdpReceiveResult clientData;
				try
				{
					clientData = await _udp.Connection.ReceiveAsync();
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

		/**
		 * <summary>Lance un message de broadcast sur le réseau local</summary>
		 * <param name="model">Données à envoyer dans le message, implémente <see cref="IModelReseau"/></param>
		 * <param name="destinationPort">Port de destination des données</param>
		 */
		public void LaunchBroadcast(IModelReseau model, int destinationPort)
		{
			_data = model;
			_destinationPort = destinationPort;

			_udp.Connection.EnableBroadcast = true;

			_timer = new Timer();
			_timer.Elapsed += BroadcastAsync;
			_timer.Interval = 500;
			_timer.Enabled = true;
		}

		/**
		 * <summary>Envoie un message broadcasté</summary>
		 */
		public async void BroadcastAsync(object source, ElapsedEventArgs e)
		{
			await _udp.SendAsync(_data, new IPEndPoint(IPAddress.Broadcast, _destinationPort));

		}

		/**
		 * <summary>Termine et ferme les connexions utilisées lors du broadcast</summary>
		 */
		public void EndBroadcast()
		{
			_timer.Enabled = false;
			_timer.Dispose();

			_udp.Connection.EnableBroadcast = false;
		}
	}
}
