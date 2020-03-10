using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
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
			//XmlTextReader configurationXml = new XmlTextReader(FichierConfiguration);
			elementConfiguration = new Dictionary<string, string>();

			string nom = null, valeur = null;
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.ConformanceLevel = ConformanceLevel.Fragment;
			settings.IgnoreWhitespace = true;
			settings.IgnoreComments = true;
			XmlReader xReader = XmlReader.Create(FichierConfiguration, settings);
			while (xReader.Read())
			{

				switch (xReader.NodeType)
				{
					case XmlNodeType.Element:
						nom = xReader.Name;
						break;
					case XmlNodeType.Text:
						valeur = xReader.Value;
						break;
				}

				if (nom == null || valeur == null) continue; // tant qu'on a pas le nom et le nb de pièces on continue de parcourir

				elementConfiguration.Add(nom, valeur);

				// reset les valeurs pour lire la prochaine pièce
				nom = null;
				valeur = null;
			}

			xReader.Close();
		}

		public string GetOption(string cle)
		{
			if (elementConfiguration.TryGetValue(cle, out string valeur))
				return valeur;

			return null;
		}

		public void SetOption(string cle, string valeur)
		{
			if(elementConfiguration.ContainsKey(cle))
				elementConfiguration[cle] = valeur;
			else
				elementConfiguration.Add(cle, valeur);
		}

		public void Enregistre()
		{
			XmlWriter xmlWriter = XmlWriter.Create(FichierConfiguration);

			xmlWriter.WriteStartDocument();
			xmlWriter.WriteStartElement("config");

			foreach (KeyValuePair<string, string> element in elementConfiguration)
			{
				xmlWriter.WriteStartElement(element.Key);
				xmlWriter.WriteString(element.Value);
				xmlWriter.WriteEndElement();
			}

			xmlWriter.WriteEndDocument();
			xmlWriter.Close();
		}
	}
}
