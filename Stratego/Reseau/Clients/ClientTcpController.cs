using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Stratego.Reseau.Models;

namespace Stratego.Reseau.Clients
{
	public class ClientTcpController
	{
		private InitModel _initModel;
		private TcpClient _client;

		/**
		 * <summary>Se connecte à un serveur</summary>
		 * <param name="serveur">Informations du serveur auquel se connecter</param>
		 */
		public async Task ConnectAsync(InitModel serveur)
		{
			_initModel = serveur;

			try
			{
				await _client.ConnectAsync(_initModel.Address.Address, _initModel.Address.Port);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				throw;
			}
		}
	}
}
