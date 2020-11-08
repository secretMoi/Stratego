using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Stratego.Core;
using Stratego.Reseau.Models;

namespace Stratego.Reseau
{
	public class Tcp
	{
		/**
		 * <summary>Lance l'écoute sur le serveur</summary>
		 * <returns>Les données wrappées dans le model T demandé, null si une erreur</returns>
		 */
		protected async Task<T> ReceiveAsync<T>(NetworkStream flux) where T : class, IModelReseau
		{
			T data = null;

			try
			{
				await Task.Run(() =>
				{
					BinaryReader binaryReader = new BinaryReader(flux); // converti le flux en binaire
					data = Serialise.ByteArrayToObject<T>(binaryReader.ReadBytes(int.MaxValue)); // converti les octets en un model demandé

				});
			}
			catch (Exception e)
			{
				Catcher.LogError(@"Impossible de recevoir un message TCP" + e.Message);
			}

			return data;
		}

		/**
		 * <summary>Envoie un model au client</summary>
		 * <param name="data">Model qui implémente <see cref="IModelReseau"/> à envoyer</param>
		 * <returns>true si tout s'est bien passé, false sinon</returns>
		 */
		protected virtual async Task<bool> SendAsync(IModelReseau data, NetworkStream flux)
		{
			try
			{
				await Task.Run(() =>
				{
					BinaryWriter binaryWriter = new BinaryWriter(flux); // converti le flux en binaire

					binaryWriter.Write(Serialise.ObjectToByteArray(data)); // envoie le model sous forme de bytes[]
				});

				return true;
			}

			catch (Exception e)
			{
				Catcher.LogError(@"Impossible d'envoyer un message TCP" + e.Message);
				return false;
			}
		}
	}
}
