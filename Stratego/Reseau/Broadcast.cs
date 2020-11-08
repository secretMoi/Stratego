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

		public async void BroadcastAsync(object source, ElapsedEventArgs e)
		{
			await _udp.SendAsync(_data, new IPEndPoint(IPAddress.Broadcast, _destinationPort));

		}

		public void EndBroadcast()
		{
			_timer.Enabled = false;
			_timer.Dispose();

			_udp.Connection.EnableBroadcast = false;
		}
	}
}
