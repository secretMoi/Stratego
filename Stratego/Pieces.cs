using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;

namespace Stratego
{
	[Serializable]
	public class Pieces : ISerializable
	{
		private const int DimensionX = 58;
		private const int DimensionY = 50;
		
		private static readonly string prefixeSource = @"Ressources\Images\";
		private const string format = ".jpg";

		private readonly string imageSource; // liste des images

		private Bitmap image;

		public Pieces(string type)
		{
			imageSource = prefixeSource + type + format;
			if(File.Exists(imageSource))
				image = new Bitmap(imageSource);
		}

		public Point Dimension => new Point(DimensionX, DimensionY);

		public Bitmap Image => image;

		public int Longueur => DimensionX;
		public int Hauteur => DimensionY;

		public void CentrePiece(ref Point point)
		{
			point.X -= Longueur / 2;
			point.Y -= Hauteur / 2;
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Image", imageSource, typeof(string));
		}
		
		// deserialise
		public Pieces(SerializationInfo info, StreamingContext context)
		{
			imageSource = (string) info.GetValue("Image", typeof(string));
			if (File.Exists(imageSource))
				image = new Bitmap(imageSource);
		}
	}
}