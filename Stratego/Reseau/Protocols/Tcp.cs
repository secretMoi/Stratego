﻿using System;
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
		protected async Task<T> ReceiveAsync<T>(NetworkStream flux) where T : class, IModelReseau
		{
			T data = null;

			try
			{
				await Task.Run(() =>
				{
					Catcher.LogInfo("En attente d'un message...");

					byte[] buffer = new byte[1024];
					using (MemoryStream ms = new MemoryStream())
					{
						int numBytesRead = flux.Read(buffer, 0, buffer.Length);
						while (flux.DataAvailable || numBytesRead > 0)
						{
							if(numBytesRead == 0)
								numBytesRead = flux.Read(buffer, 0, buffer.Length);

							Catcher.LogInfo($"Réception de {numBytesRead} bytes");

							ms.Write(buffer, 0, numBytesRead);
							numBytesRead = 0;
						}

						data = Serialise.MemoryStreamToObject<T>(ms); // converti les octets en un model demandé

						Catcher.LogInfo($"Message de longueur {ms.ToArray().Length} reçu");
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

					binaryWriter.Write(byteData); // envoie le model sous forme de bytes[]

					Catcher.LogInfo($"Message de longueur {byteData.Length} envoyé");
				});

				return true;
			}

			catch (Exception e)
			{
				Catcher.LogError(@"Impossible d'envoyer un message TCP : " + e.Message);
				return false;
			}
		}
	}
}
