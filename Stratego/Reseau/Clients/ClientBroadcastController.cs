using System;
using System.Net;
using System.Net.Sockets;
using System.Timers;
using Stratego.Reseau.Models;

namespace Stratego.Reseau.Clients
{
	public class ClientBroadcastController
	{
		private const int Port = 32430;

		private string _token;
		private UdpClient _broadcast;
		private byte[] _initModel;
		private Timer _timer;

		public async void BroadCastAsync(object source, ElapsedEventArgs e)
		{
			await _broadcast.SendAsync(_initModel, _initModel.Length, new IPEndPoint(IPAddress.Broadcast, Port));
		}

		public void LaunchBroadcast()
		{
			_broadcast = new UdpClient();
			_token = Reseau.CreateToken();
			_initModel = Serialise.ObjectToByteArray(new InitModel
			{
				Address = new IPEndPoint(Reseau.GetLocalIpAddress(), 32530),
				MachineName = Environment.MachineName,
				Token = _token
			});

			_broadcast.EnableBroadcast = true;

			_timer = new Timer();
			_timer.Elapsed += BroadCastAsync;
			_timer.Interval = 500;
			_timer.Enabled = true;
		}

		public void EndBroadcast()
		{
			_timer.Enabled = false;
			_timer.Dispose();
			_broadcast.Close();
		}
	}
}