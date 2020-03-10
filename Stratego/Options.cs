using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Stratego
{
	[Serializable]
	public class Options : ISerializable
	{
		private const string FichierConfiguration = @"config.ini";
		private Dictionary<string, string> elementConfiguration;

		// sérialise
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("ElementConfiguration", elementConfiguration, typeof(Dictionary<string, string>));
		}

		// deserialise
		public Options(SerializationInfo info, StreamingContext context)
		{
			elementConfiguration = (Dictionary<string, string>)info.GetValue("ElementConfiguration", typeof(Dictionary<string, string>));
		}

		public Options()
		{
			ChargeFichier();
		}

		private void ChargeFichier()
		{
			XmlTextReader configurationXml = new XmlTextReader(FichierConfiguration);
			elementConfiguration = new Dictionary<string, string>();

			string nom = null, valeur = null;

			while (configurationXml.Read()) // parcours le fichier XML
			{
				if (configurationXml.NodeType == XmlNodeType.Element && configurationXml.Name == "nom") // récupère le nom
					nom = configurationXml.ReadElementString();
				if (configurationXml.NodeType == XmlNodeType.Element && configurationXml.Name == "valeur") // récupère le nb de pièces
					valeur = configurationXml.ReadElementString();

				if (nom == null || valeur == null) continue; // tant qu'on a pas le nom et le nb de pièces on continue de parcourir

				elementConfiguration.Add(nom, valeur);

				// reset les valeurs pour lire la prochaine pièce
				nom = null;
				valeur = null; // remet à 0 le nombre de pièces à chaque tour de boucle
			}
		}

		public string GetOption(string cle)
		{
			if (elementConfiguration.TryGetValue(cle, out string valeur))
				return valeur;

			return null;
		}

		public void SetOption(string cle, string valeur)
		{
			if (elementConfiguration.TryGetValue(cle, out string valeurResultat))
				elementConfiguration[valeurResultat] = valeur;
			else
				elementConfiguration.Add(cle, valeur);
		}
	}
}
