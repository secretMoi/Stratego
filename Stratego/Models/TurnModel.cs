using System.Collections.Generic;
using Stratego.Personnages;
using Stratego.Reseau.Models;

namespace Stratego.Models
{
	public class TurnModel : IModelReseau
	{
		public List<Rectangle> PositionPieces { get; set; }
		public Personnage[,] Grille { get; set; }
	}
}
