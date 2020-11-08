using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Stratego.Reseau.Models;

namespace Stratego.Reseau.Serveurs
{
	public class ServeurTcpController : TcpConnection
	{
		private TcpListener _serveur;
		private TcpClient _client;

		/**
		 * <summary>Démarre le serveur</summary>
		 * <param name="config">Configuration du serveur dans un <see cref="IPEndPoint"/></param>
		 * <returns>true si la connexion a pu s'effectuer, false sinon</returns>
		 */
		public async Task<bool> ListenAsync(IPEndPoint config)
		{
			_serveur = new TcpListener(config);

			try
			{
				await Task.Run(() =>
				{
					_serveur.Start();
					_client = _serveur.AcceptTcpClient(); // accepte une connexion client
				});

				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);

				return false;
			}
			
		}

		/**
		 * <summary>Lance l'écoute sur le serveur</summary>
		 * <returns>Les données wrappées dans le model T demandé, null si une erreur</returns>
		 */
		public async Task<T> ReceiveAsync<T>() where T : class, IModelReseau
		{
			return await ReceiveAsync<T>(_client.GetStream());
			/*T data = null;

			try
			{
				await Task.Run(() =>
				{
					NetworkStream flux = _client.GetStream(); // recoit le flux
					BinaryReader binaryReader = new BinaryReader(flux); // converti le flux en binaire
					data = Serialise.ByteArrayToObject<T>(binaryReader.ReadBytes(int.MaxValue)); // converti les octets en un model demandé

				});
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			return data;*/
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
			try
			{
				await Task.Run(() =>
				{
					NetworkStream flux = new NetworkStream(_serveur.Server); // recoit le flux

					BinaryWriter binaryWriter = new BinaryWriter(flux); // converti le flux en binaire

					binaryWriter.Write(Serialise.ObjectToByteArray(data)); // envoie le model sous forme de bytes[]
				});

				return true;
			}

			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
		}


		public void Close()
		{
			_client.Close();
			_serveur.Stop();
		}
	}
}
