using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Stratego.Reseau.Models;

namespace Stratego.Reseau.Clients
{
	public class ClientTcpController : TcpConnection
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
			}
		}

		/**
		 * <summary>Lance l'écoute sur le serveur</summary>
		 * <returns>Les données wrappées dans le model T demandé, null si une erreur</returns>
		 */
		public async Task<T> ReceiveAsync<T>() where T : class, IModelReseau
		{
			return await ReceiveAsync<T>(_client.GetStream());
		}

		/**
		 * <summary>Lance l'écoute sur le serveur</summary>
		 * <param name="callback">Méthode à rappeler lorsque des données arrivent</param>
		 */
		public async Task ReceiveCallbackAsync<T>(Action<T> callback) where T : class, IModelReseau
		{
			callback?.Invoke(await ReceiveAsync<T>());
		}

		/**
		 * <summary>Envoie un model au client</summary>
		 * <param name="data">Model qui implémente <see cref="IModelReseau"/> à envoyer</param>
		 * <returns>true si tout s'est bien passé, false sinon</returns>
		 */
		public async Task<bool> SendAsync(IModelReseau data)
		{
			return await SendAsync(data, _client.GetStream());
		}

		public void Close()
		{
			_client.Close();
		}
	}
}
