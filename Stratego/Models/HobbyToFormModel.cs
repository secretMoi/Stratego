using Stratego.Reseau.Models;
using Stratego.Reseau.Protocols;

namespace Stratego.Models
{
	public static class HobbyToFormModel<T> where T : Tcp
	{
		public static T TcpConnection { get; set; }

		public static InitModel InitModel { get; set; }
	}
}
