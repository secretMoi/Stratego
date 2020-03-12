using System;
using System.IO;

namespace Stratego
{
	class MusiqueFond : Son
	{
		protected static string repertoireSons = @"Ressources\Sons\Fond\";
		public MusiqueFond(string cheminFichier, Extension typeExtension = Extension.MP3) : base(cheminFichier, typeExtension, repertoireSons)
		{
			
		}

		public static string[] ListeSons()
		{
			if (Directory.Exists(repertoireSons))
			{
				string[] musiques = Directory.GetFiles(repertoireSons, "*.mp3");
				string[] musiquesRetour = new string[musiques.Length];
				string chaineTemp;

				for (int i = 0; i < musiques.Length; i++)
				{
					chaineTemp = musiques[i].Remove(0, repertoireSons.Length);
					musiquesRetour[i] = chaineTemp.Remove(chaineTemp.Length - 4, 4);
				}

				return musiquesRetour;
			}

			return null;
		}
	}
}
