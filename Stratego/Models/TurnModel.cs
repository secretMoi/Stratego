using System.Collections.Generic;
using Stratego.Reseau.Models;

namespace Stratego.Models
{
	public class TurnModel : IModelReseau
	{
		public List<Rectangle> PositionPieces { get; set; }
		public Dictionary<string, int> ListePieces { get; set; }
		public Map Map { get; set; }
	}
}
