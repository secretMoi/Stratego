using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Timers;
using Stratego.Reseau.Models;

namespace Stratego.Reseau.Clients
{
	public class ClientController
	{
		private const int Port = 32430;
		private string _token = Reseau.CreateToken();

		public async void BroadCastAsync(object source, ElapsedEventArgs e)
		{
			var client = new UdpClient();

			var requestData = Serialise.ObjectToByteArray(new InitModel
			{
				Address = new IPEndPoint(Reseau.GetLocalIpAddress(), Port),
				MachineName = Environment.MachineName,
				Token = _token
			});

			client.EnableBroadcast = true;

			await client.SendAsync(requestData, requestData.Length, new IPEndPoint(IPAddress.Broadcast, Port));

			client.Close();
		}

		public void LaunchBroadcast()
		{
			Timer aTimer = new Timer();
			aTimer.Elapsed += BroadCastAsync;
			aTimer.Interval = 500;
			aTimer.Enabled = true;
		}
	}
}