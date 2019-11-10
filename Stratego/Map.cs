using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Stratego
{
    public class Map
    {
        // chemin du background map
        public readonly string AireJeu = @"C:\Users\winmo\RiderProjects\Stratego\Stratego\images\fonds.png";

        // nombre de cases
        private const int casesX = 10;
        private const int casesY = 10;
        
        // taille de chaque case
        private const int longueurCase = 61;
        private const int hauteurCase = 53;

        // position de la première case
        public const int OffsetX = 4;
        public const int OffsetY = 131;

        public const bool Pixel = false;
        public const bool Coord = true;

        public Map()
        {
        }

        public int posPieceX(int colonne)
        {
            return OffsetX + longueurCase * colonne;
        }
        public int posPieceY(int ligne)
        {
            return OffsetY + hauteurCase * ligne;
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

                point.X = (point.X / longueurCase);
                point.X *= longueurCase;
                point.Y = (point.Y / hauteurCase);
                point.Y *= hauteurCase;
            
                point.X += OffsetX;
                point.Y += OffsetY;
            }
            else // si l'on souhaite les coordonnées comme un tableau
            {
                point.X = (point.X - OffsetX) / longueurCase;
                point.Y = (point.Y - OffsetY) / hauteurCase;
            }
            
            return point;
        }

        public Point PxToCoord(Point point)
        {
            // si en dehors de la grille on renvoie (-1; -1)
            if (!PositionValide(point))
            {
                point.X = -1;
                point.Y = -1;
                
                return point;
            }
            
            point.X = (point.X - OffsetX) / longueurCase;
            point.Y = (point.Y - OffsetY) / hauteurCase;

            return point;
        }

        public bool PositionValide(Point point, bool activeMarge = false) // vérifie que la position est bien dans la grille de jeu
        {
            int margeX = 0, margeY = 0;
            if (activeMarge)
            {
                // les marges permettent de ne pas bloquer la pièce trop fréquemment lors des déplacements sur les bords
                margeX = 20;
                margeY = 20;
            }

            if (
                point.X >= OffsetX - margeX && point.X < OffsetX + longueurCase * casesX + margeX &&
                point.Y >= OffsetY - margeY && point.Y < OffsetY + hauteurCase  * casesY + margeY
            )
                return true;

            return false;
        }

        // recoit des coordonnées en pixels, et retourne la distance en nombre de cases
        public int Distance(Point source, Point destination)
        {
            // conversion des px en coordonnées
            source = PxToCoord(source);
            destination = PxToCoord(destination);

            int distance = (int) 
                Math.Ceiling(
                    Math.Sqrt(
                        Math.Pow(destination.X - source.X, 2) + 
                        Math.Pow(destination.Y - source.Y, 2)
                    )
                );

            return distance;
        }
    }
}