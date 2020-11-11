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
		public List<Rectangle> PositionPieces { get; set; }
		public Personnage[,] Grille { get; set; }

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Grille", Grille, typeof(Personnage[,]));
			info.AddValue("PositionPieces", PositionPieces, typeof(List<Rectangle>));
		}

		// deserialise
		public TurnModel(SerializationInfo info, StreamingContext context)
		{
			Grille = (Personnage[,])info.GetValue("Grille", typeof(Personnage[,]));
			PositionPieces = (List<Rectangle>)info.GetValue("PositionPieces", typeof(List<Rectangle>));
		}

		public TurnModel()
		{
			
		}
	}
}
