using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Stratego.Personnages;
using Stratego.Reseau.Models;

namespace Stratego.Models
{
	[Serializable]
	public class TurnModel : IModelReseau
	{
		public PartieActuelle PartieActuelle { get; set; }
		public string BoutonRemplir { get; set; }
		public bool EtatBoutonRemplir { get; set; }
		public string Historique { get; set; }

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("PartieActuelle", PartieActuelle, typeof(PartieActuelle));
			info.AddValue("BoutonRemplir", BoutonRemplir, typeof(string));
			info.AddValue("EtatBoutonRemplir", EtatBoutonRemplir, typeof(bool));
			info.AddValue("Historique", Historique, typeof(string));
		}

		// deserialise
		public TurnModel(SerializationInfo info, StreamingContext context)
		{
			PartieActuelle = (PartieActuelle)info.GetValue("PartieActuelle", typeof(PartieActuelle));
			BoutonRemplir = (string)info.GetValue("BoutonRemplir", typeof(string));
			EtatBoutonRemplir = (bool)info.GetValue("EtatBoutonRemplir", typeof(bool));
			Historique = (string)info.GetValue("Historique", typeof(string));
		}

		public TurnModel()
		{
			
		}
	}
}
