using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
//using Newtonsoft.Json;
using Stratego.Personnages;

namespace Stratego
{
	[Serializable]
	public class Map : ISerializable
	{
		// chemin du background map
		private readonly string aireJeu = @"Ressources\Images\fonds.png";
		private readonly Bitmap fond;

		// nombre de cases
		public const int CasesX = 10;
		public const int CasesY = 10;
		private Personnage[,] grille;
		private readonly List<Point> casesInterdites;
		
		// taille de chaque case
		private const int LongueurCase = 61;
		private const int HauteurCase = 53;

		// position de la première case
		private const int OffsetX = 4;
		private const int OffsetY = 131;

		public const bool Pixel = false;
		public const bool Coord = true;

		public Personnage[,] Grille
		{
			get => grille;
			set => grille = value;
		}

		public Map(List<Point> casesInterdites)
		{
			fond = new Bitmap(aireJeu);
			
			this.casesInterdites = casesInterdites;
			grille = new Personnage[CasesX, CasesY];
		}

		public Map()
		{

		}

		public void SetPositionPiece(Point point, Personnage idElement)
		{
			grille[point.X, point.Y] = idElement;
		}

		public (int, int, int) DeplacePiece(Point source, Point destination)
		{
			Personnage attaquant = grille[source.X, source.Y];

			if (attaquant.Deplacement < 1) return (-1, -1, -1);  // si la pièce peut se déplacer
			if (!SansObstacle(source, destination)) return (-1, -1, -1);  // si il n'y a pas d'obstacle sur son chemin
			int collision = attaquant.Collision(attaquant, grille[destination.X, destination.Y]);
					
			switch (collision)
			{
				case Personnage.Vide:
					SetPositionPiece(destination, attaquant); // définit nouvelle position
					SetPositionPiece(source, null); // supprime l'ancienne position

					attaquant.Position = destination;

					return (Personnage.Vide, -1, -1);
				
				case Personnage.Defenseur: // si le défenseur gagne
					SetPositionPiece(source, null); // supprime la position de l'attaquant

					attaquant.Meurt();

					return (Personnage.Defenseur, attaquant.Id, -1);
				
				case Personnage.Attaquant: // si l'attaquant gagne
				{
					Personnage defenseur = grille[destination.X, destination.Y];
					defenseur.Meurt();
						
					SetPositionPiece(destination, attaquant); // l'attaquant prend la place du défenseur
					SetPositionPiece(source, null); // supprime l'ancienne position

					attaquant.Position = destination;

					return (Personnage.Attaquant, defenseur.Id, -1);
				}
				case Personnage.Egalite:
				{
					Personnage defenseur = grille[destination.X, destination.Y];
						
					attaquant.Meurt();
					defenseur.Meurt();
						
					SetPositionPiece(source, null); // supprime les 2 pièces de la grille
					SetPositionPiece(destination, null);
						
					return (Personnage.Egalite, defenseur.Id, attaquant.Id);
				}
				default:
					return (-1, -1, -1);
			}
		}

		private bool SansObstacle(Point source, Point destination)
		{
			int sens = 1;

			if (source.X != destination.X) // si le déplacement est horizontal
			{
				if (source.X > destination.X)
					sens = -1;
				
				for (int x = source.X; x != destination.X; x += sens)
				{
					if (x != source.X && x != destination.X)
					{
						if (grille[x, source.Y] != null || !CaseAutorisee(new Point(x, source.Y)))
							return false;
					}
				}
			} // sinon il est vertical
			else
			{
				if (source.Y > destination.Y)
					sens = -1;
				
				for (int y = source.Y; y != destination.Y; y += sens)
				{
					if (y != source.Y && y != destination.Y)
					{
						if (grille[source.X, y] != null || !CaseAutorisee(new Point(source.X, y)))
							return false;
					}
				}
			}

			return true;
		}

		public Personnage GetPiece(Point point, bool typeCoord = Coord)
		{
			if (typeCoord == Pixel) // si on passe le point en px, on le convertit d'abord en coord
				point = PxToCoord(point);
			
			if(PositionValide(point, Coord))
				return grille[point.X, point.Y];
			
			return null;
		}

		// retourne les coordonnées d'une case dans l'unité souhaitée
		// grâce aux coordonnées de la case quelconques
		public Point TrouveCase(Point point, bool typeCoord = Pixel)
		{
			// si en dehors de la grille on renvoie (-1; -1)
			if (!PositionValide(point))
			{
				point.X = -1;
				point.Y = -1;
				
				return point;
			}
			
			if (typeCoord == Pixel) // si l'on souhaite les coordonnées en px d'une case
			{
				point.X -= OffsetX;
				point.Y -= OffsetY;

				point.X = (point.X / LongueurCase) * LongueurCase;
				point.Y = (point.Y / HauteurCase) * HauteurCase;
			
				point.X += OffsetX;
				point.Y += OffsetY;
			}
			else // si l'on souhaite les coordonnées comme un tableau
			{
				point = PxToCoord(point);
			}
			
			return point;
		}

		public bool PositionValide(Point point, bool typeCoord = Pixel) // vérifie que la position en px est bien dans la grille de jeu
		{
			if (typeCoord == Pixel)
			{
				if (
					point.X >= OffsetX && point.X < OffsetX + LongueurCase * CasesX &&
					point.Y >= OffsetY && point.Y < OffsetY + HauteurCase  * CasesY
				)
					return true;
			}
			else
			{
				if(
					point.X >= 0 && point.X <= CasesX &&
					point.Y >= 0 && point.Y <= CasesY)
					
					return true;
			}
			
			return false;
		}

		// recoit des coordonnées en pixels, et retourne la distance en nombre de cases
		private int Distance(Point source, Point destination)
		{
			if (!PositionValide(source, Coord) && !PositionValide(destination, Coord))
				return -1;

			int distance = (int) 
				Math.Ceiling(
					Math.Sqrt(
						Math.Pow(destination.X - source.X, 2) + 
						Math.Pow(destination.Y - source.Y, 2)
					)
				);

			return distance;
		}

		public static Point CoordToPx(Point point)
		{
			point.X = OffsetX + point.X * LongueurCase;
			point.Y = OffsetY + point.Y * HauteurCase;

			return point;
		}
		
		public Point PxToCoord(Point point)
		{
			point.X = (point.X - OffsetX) / LongueurCase;
			point.Y = (point.Y - OffsetY) / HauteurCase;

			return point;
		}

		private static bool DeplacementLineaire(Point origine, Point destination)
		{
			return origine.X == destination.X || origine.Y == destination.Y;
		}

		public Personnage TrouvePersoParId(int id)
		{
			for (int y = 0; y < CasesY; y++)
			{
				for (int x = 0; x < CasesX; x++)
				{
					if (grille[x, y] != null) // si l'endroit de la grille n'est pas vide
					{
						if (grille[x, y].Id == id) // si l'id correspond
							return grille[x, y];
					}
				} 
			}

			return null;
		}

		public bool CaseAutorisee(Point destination)
		{
			foreach (Point point in casesInterdites)
				if (destination.X == point.X && destination.Y == point.Y)
					return false;

			return true;
		}

		public bool ConditionsDeplacement(int id, Point origine, Point destination)
		{
			return
				TrouvePersoParId(id).Deplacement >= Distance(origine, destination)
				&& origine != destination // si on ne replace pas la pièce au même endroit
				&& CaseAutorisee(destination) // vérifie que la position ne fait pas partie des cases interdites
				&& DeplacementLineaire(origine, destination) // si la pièce ne se déplace pas en diagonal
				&& SansObstacle(origine, destination);
		}

		public Bitmap Fond => fond;

		// serialise
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Fond", aireJeu, typeof(string));
			info.AddValue("Grille", grille, typeof(Personnage[,]));
			info.AddValue("CasesInterdites", casesInterdites, typeof(List<Point>));
		}
		
		// deserialise
		public Map(SerializationInfo info, StreamingContext context)
		{
			aireJeu = (string) info.GetValue("Fond", typeof(string));
			if (File.Exists(aireJeu))
				fond = new Bitmap(aireJeu);

			grille = (Personnage[,]) info.GetValue("Grille", typeof(Personnage[,]));
			casesInterdites = (List<Point>) info.GetValue("CasesInterdites", typeof(List<Point>));
		}
	}
}