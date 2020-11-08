using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Stratego.Core;
using Stratego.Reseau.Models;

namespace Stratego.Reseau.Clients
{
	public class ClientTcpController : Tcp
	{
		private InitModel _initModel;
		private TcpClient _client;

		public ClientTcpController()
		{
			_client = new TcpClient();
		}

		/**
		 * <summary>Se connecte à un serveur</summary>
		 * <param name="serveur">Informations du serveur auquel se connecter</param>
		 */
		public async Task ConnectAsync(IPEndPoint serveur)
		{
			try
			{
				//todo erreur
				await _client.ConnectAsync(serveur.Address, serveur.Port);
				Catcher.LogInfo($@"Connecté au serveur TCP {serveur.Address}:{serveur.Port}");
			}
			catch (Exception e)
			{
				Catcher.LogError(e.Message);
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
