﻿using System;
using System.Drawing;

namespace Stratego
{
    public class Map
    {
        // chemin du background map
        public readonly string AireJeu = @"C:\Users\winmo\RiderProjects\Stratego\Stratego\images\fonds.png";

        // nombre de cases
        private const int casesX = 10;
        private const int casesY = 10;
        private Object[,] grille;
        
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
            grille = new Object[casesX, casesY];
            
                
        }

        // permet de positionner une pièce grâce à ses coord et renvoie les px correspondants
        public int posPieceX(int colonne)
        {
            return OffsetX + longueurCase * colonne;
        }
        public int posPieceY(int ligne)
        {
            return OffsetY + hauteurCase * ligne;
        }

        public bool SetPositionPiece(Point point, int idElement)
        {
            // todo vérifier position ok
            // todo à modifier vu que l'on pourra tuer les persos
            if (grille[point.X, point.Y] == null) // si il n'y a rien dans cette case
            {
                grille[point.X, point.Y] = idElement;

                return true;
            }

            return false;
        }

        public Object GetPositionPiece(Point point)
        {
            return grille[point.X, point.Y];
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

                point.X = (point.X / longueurCase) * longueurCase;
                point.Y = (point.Y / hauteurCase) * hauteurCase;
            
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

        public bool PositionValide(Point point) // vérifie que la position en px est bien dans la grille de jeu
        {
            if (
                point.X >= OffsetX && point.X < OffsetX + longueurCase * casesX &&
                point.Y >= OffsetY && point.Y < OffsetY + hauteurCase  * casesY
            )
                return true;

            return false;
        }

        // recoit des coordonnées en pixels, et retourne la distance en nombre de cases
        public int Distance(Point source, Point destination)
        {
            // conversion des px en coordonnées
            source = TrouveCase(source, Coord);
            destination = TrouveCase(destination, Coord);

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