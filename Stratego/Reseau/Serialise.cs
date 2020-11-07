﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Stratego.Reseau.Models;

namespace Stratego.Reseau
{
	public class Serialise
	{
		/**
		 * <summary>Converti un objet en un tableau de byte</summary>
		 * <param name="obj">Objet à sérialiser</param>
		 * <returns>L'objet sérialisé</returns>
		 */
		public static byte[] ObjectToByteArray(object obj)
		{
			BinaryFormatter bf = new BinaryFormatter();
			using (var ms = new MemoryStream())
			{
				bf.Serialize(ms, obj);
				return ms.ToArray();
			}
		}

		/**
		 * <summary>Converti un tableau de byte en un objet</summary>
		 * <param name="arrBytes">Tableau de byte à désérialiser</param>
		 * <returns>L'objet C#</returns>
		 */
		public static T ByteArrayToObject<T>(byte[] arrBytes) where T : class, IModelReseau
		{
			using (var memStream = new MemoryStream())
			{
				var binForm = new BinaryFormatter();

				memStream.Write(arrBytes, 0, arrBytes.Length);
				memStream.Seek(0, SeekOrigin.Begin);
				var obj = binForm.Deserialize(memStream);

				return obj as T;
			}
		}
	}
}
