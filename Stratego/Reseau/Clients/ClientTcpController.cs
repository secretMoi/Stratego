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
		 * <summary>Lance l'écoute</summary>
		 * <returns>Les données wrappées dans le model T demandé, null si une erreur</returns>
		 */
		/*public async Task<T> ReceiveAsync<T>() where T : class, IModelReseau
		{
			T data = null;

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

			return data;
		}*/
	}
}
