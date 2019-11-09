using System;
using System.Drawing;
using System.Windows.Forms;

namespace Stratego
{
    public class Map
    {
        public readonly string AireJeu = @"C:\Users\winmo\RiderProjects\Stratego\Stratego\images\fonds.png";

        private const int casesX = 10;
        private const int casesY = 10;
        private const int longueurCase = 61;
        private const int hauteurCase = 53;

        public const int OffsetX = 4;
        public const int OffsetY = 131;

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

        // retourne les coordonées de 0 à casesX d'une case
        // grâce à des coordonées de la case quelconques
        /*public Point TrouveCase(Point point)
        {
            // si en dehors de la grille on renvoie (-1; -1)
            if (point.X < OffsetX || point.X > OffsetX + longueurCase * casesX ||
                point.Y < OffsetY || point.Y > OffsetY + hauteurCase * casesY)
            {
                point.X = -1;
                point.Y = -1;
                return point;
            }
            
            point.X = (point.X - OffsetX) / longueurCase;
            point.Y = (point.Y - OffsetY) / hauteurCase;
            
            return point;
        }*/
        
        // retourne les coordonées en px d'une case
        // grâce à des coordonées de la case quelconques
        public Point TrouveCase(Point point)
        {
            // si en dehors de la grille on renvoie (-1; -1)
            if (point.X < OffsetX || point.X > OffsetX + longueurCase * casesX ||
                point.Y < OffsetY || point.Y > OffsetY + hauteurCase * casesY)
            {
                point.X = -1;
                point.Y = -1;
                return point;
            }

            point.X -= OffsetX;
            point.Y -= OffsetY;
            
            point.X = (point.X / longueurCase) * longueurCase;
            point.Y = (point.Y / hauteurCase) * hauteurCase;
            
            point.X += OffsetX;
            point.Y += OffsetY;
            
            return point;
        }

        public Point DebutCase(Point point) // trouve le debut d'une case (en px) grace à ses coord 0 à casesX
        {
            point.X = (point.X * longueurCase) + OffsetX;
            
            if(point.Y < casesY / 2)
                point.Y = (point.Y * hauteurCase) + OffsetY;
            else
                point.Y = (point.Y * (hauteurCase-1)) + OffsetY + 4;

            return point;
        }

        public bool PositionValide(int x, int y) // vérifie que la position est bien dans la grille de jeu
        {
            // les marges permettent de ne pas bloquer la pièce trop fréquemment lors des déplacements sur les bords
            int margeX = 20;
            int margeY = 20;
            
            if (
                x > OffsetX - margeX && x < OffsetX + longueurCase * (casesX-1) + margeX &&
                y > OffsetY - margeY && y < OffsetY + hauteurCase  * (casesY-1) + margeY
            )
                return true;

            return false;
        }
    }
}