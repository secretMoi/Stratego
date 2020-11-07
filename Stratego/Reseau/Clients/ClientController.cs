using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Stratego.Reseau.Clients
{
	public class ClientController
	{
		private const int Port = 32430;

		public async Task<PingReply> PingAsync(string ip)
		{
			return await new Ping().SendPingAsync(ip, 100);
		}

		public Task<string> PingTask(string ip)
		{
			//todo changer en une req custom pour que les serveurs répondent
			return Task.Run(() => PingAsync(ip)).ContinueWith(task =>
				{
					//IPHostEntry repEntry = null;

					string res = null;

					if (task.Result.Status == IPStatus.Success)
						res = task.Result.Address.ToString();
					//repEntry = Dns.GetHostEntry(task.Result.Address);

					return res;
				}
			);
		}

		public async Task BroadCastAsync()
		{
			var client = new UdpClient();
			var requestData = Encoding.ASCII.GetBytes("Stratego_" + Environment.MachineName);

			client.EnableBroadcast = true;
			await client.SendAsync(requestData, requestData.Length, new IPEndPoint(IPAddress.Broadcast, Port));

			client.Close();
		}
	}
}
