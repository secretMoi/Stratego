using System;
using System.Net;

namespace Stratego.Reseau.Models
{
	[Serializable]
	public class InitModel : IModelReseau
	{
		public IPEndPoint Address { get; set; }

		public string MachineName { get; set; }

		public string Token { get; set; }
	}
}
