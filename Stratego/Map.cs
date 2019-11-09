using System;
using System.Drawing;

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

        public Tuple<int, int> TrouveCase(Point point)
        {
            if(point.X < OffsetX || point.X > OffsetX + longueurCase * casesX ||
               point.Y < OffsetY || point.Y > OffsetY + hauteurCase * casesY)
                return new Tuple<int, int>(-1, -1);
            
            int caseX = (point.X - OffsetX) / longueurCase;
            int caseY = (point.Y - OffsetY) / hauteurCase;
            
            return new Tuple<int, int>(caseX, caseY);
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