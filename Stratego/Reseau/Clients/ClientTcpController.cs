using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Stratego.Core;
using Stratego.Reseau.Models;
using Stratego.Reseau.Protocols;

namespace Stratego.Reseau.Clients
{
	public class ClientTcpController : Tcp
	{
		private readonly TcpClient _client;
		private NetworkStream _flux;

		public ClientTcpController()
		{
			_client = new TcpClient();
		}

		/**
		 * <summary>Se connecte à un serveur</summary>
		 * <param name="serveur">Informations du serveur auquel se connecter</param>
		 */
		public async Task<bool> ConnectAsync(IPEndPoint serveur)
		{
			try
			{
				await _client.ConnectAsync(serveur.Address, serveur.Port);
				_flux = _client.GetStream();
				Catcher.LogInfo($@"Connecté au serveur TCP {serveur.Address}:{serveur.Port}");

				return true;
			}
			catch (Exception e)
			{
				Catcher.LogError($@"Impossible de se connecter au serveur TCP {e.Message}");

				return false;
			}
		}

		/**
		 * <summary>Lance l'écoute sur le serveur</summary>
		 * <returns>Les données wrappées dans le model T demandé, null si une erreur</returns>
		 */
		public async Task<T> ReceiveAsync<T>() where T : class, IModelReseau
		{
			return await ReceiveAsync<T>(_flux, _client.ReceiveBufferSize);
			//return await ReceiveAsync<T>(_flux, 1024*1024*32);
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
			return await SendAsync(data, _flux);
		}

		public void Close()
		{
			_client.Close();
		}
	}
}
