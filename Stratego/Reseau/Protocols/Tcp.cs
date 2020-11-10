using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Stratego.Core;
using Stratego.Reseau.Models;

namespace Stratego.Reseau.Protocols
{
	public class Tcp
	{
		/**
		 * <summary>Lance l'écoute sur le serveur</summary>
		 * <returns>Les données wrappées dans le model T demandé, null si une erreur</returns>
		 */
		protected async Task<T> ReceiveAsync<T>(TcpClient tcpClient) where T : class, IModelReseau
		{
			T data = null;

			try
			{
				await Task.Run(() =>
				{
					Catcher.LogInfo("En attente d'un message...");

					// lit le premier message, contenant la longueur du second
					/*byte[] lengthData = new byte[4];
					flux.Read(lengthData, 0, 4); // This method blocks until at least one byte is read.
					int length = BitConverter.ToInt32(lengthData, 0);
					Catcher.LogInfo($"Message de longueur {length} attendu");

					byte[] byteData = new byte[length];*/
					using (NetworkStream flux = tcpClient.GetStream())
					{
						byte[] buffer = new byte[1024];
						using (MemoryStream ms = new MemoryStream())
						{
							int numBytesRead;
							while ((numBytesRead = flux.Read(buffer, 0, buffer.Length)) > 0)
							{
								Catcher.LogInfo($"Réception de {numBytesRead} bytes");
								ms.Write(buffer, 0, numBytesRead);
							}

							Catcher.LogInfo(numBytesRead.ToString());
							data = Serialise.ByteArrayToObject<T>(ms.ToArray()); // converti les octets en un model demandé

							//flux.Read(lengthData, 0, length); // This method blocks until at least one byte is read.
							Catcher.LogInfo($"Message de longueur {ms.ToArray().Length} reçu");

							//data = Serialise.ByteArrayToObject<T>(byteData); // converti les octets en un model demandé
						}
					}
				});
			}
			catch (Exception e)
			{
				Catcher.LogError(@"Impossible de recevoir un message TCP : " + e.Message);
			}

			return data;
		}

		/**
		 * <summary>Envoie un model au client</summary>
		 * <param name="data">Model qui implémente <see cref="IModelReseau"/> à envoyer</param>
		 * <param name="flux">Flux <see cref="NetworkStream"/> à envoyer</param>
		 * <returns>true si tout s'est bien passé, false sinon</returns>
		 */
		protected virtual async Task<bool> SendAsync(IModelReseau data, NetworkStream flux)
		{
			try
			{
				await Task.Run(() =>
				{
					BinaryWriter binaryWriter = new BinaryWriter(flux); // converti le flux en binaire

					byte[] byteData = Serialise.ObjectToByteArray(data);

					//Send(binaryWriter, BitConverter.GetBytes(byteData.Length));

					binaryWriter.Write(byteData); // envoie le model sous forme de bytes[]

					Catcher.LogInfo($"Message de longueur {byteData.Length} envoyé");
				});

				return true;
			}

			catch (Exception e)
			{
				Catcher.LogError(@"Impossible d'envoyer un message TCP" + e.Message);
				return false;
			}
		}

		protected void Send(BinaryWriter sender, byte[] data)
		{
			try
			{
				sender.Write(data);
			}
			catch (Exception e)
			{
				Catcher.LogError($"Impossible d'envoyer le message le message TCP de longueur {data.Length} : " + e.Message);
			}
		}
	}
}
