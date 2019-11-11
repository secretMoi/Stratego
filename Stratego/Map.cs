using System;
using System.Drawing;
using Stratego.Personnages;

namespace Stratego
{
    public class Map
    {
        // chemin du background map
        public readonly string AireJeu = @"C:\Users\winmo\RiderProjects\Stratego\Stratego\images\fonds.png";

        // nombre de cases
        public const int casesX = 10;
        public const int casesY = 10;
        private Personnage[,] grille;
        
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
            grille = new Personnage[casesX, casesY];
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

        public void SetPositionPiece(Point point, Personnage idElement)
        {
            grille[point.X, point.Y] = idElement;
        }

        public (int, int, int) DeplacePiece(Point source, Point destination)
        {
            Personnage attaquant = grille[source.X, source.Y];
            
            if (attaquant.Deplacement >= 1) // si la pièce peut se déplacer
            {
                if (SansObstacle(source, destination)) // si il n'y a pas d'obstacle sur son chemin
                {
                    int collision = attaquant.Collision(attaquant, grille[destination.X, destination.Y]);
                    
                    if (collision == Personnage.Vide)
                    {
                        SetPositionPiece(destination, attaquant); // définit nouvelle position
                        SetPositionPiece(source, null); // supprime l'ancienne position

                        attaquant.Position = destination;

                        return (Personnage.Vide, -1, -1);
                    }
                    
                    else if (collision == Personnage.Defenseur) // si le défenseur gagne
                    {
                        SetPositionPiece(source, null); // supprime la position de l'attaquant

                        attaquant.Meurt();

                        return (Personnage.Defenseur, attaquant.Id, -1);
                    }
                    
                    else if (collision == Personnage.Attaquant) // si l'attaquant gagne
                    {
                        Personnage defenseur = grille[destination.X, destination.Y];
                        defenseur.Meurt();
                        
                        SetPositionPiece(destination, attaquant); // l'attaquant prend la place du défenseur
                        SetPositionPiece(source, null); // supprime l'ancienne position

                        attaquant.Position = destination;

                        return (Personnage.Attaquant, defenseur.Id, -1);
                    }
                    
                    else if (collision == Personnage.Egalite)
                    {
                        Personnage defenseur = grille[destination.X, destination.Y];
                        
                        attaquant.Meurt();
                        defenseur.Meurt();
                        
                        SetPositionPiece(source, null); // supprime les 2 pièces de la grille
                        SetPositionPiece(destination, null);
                        
                        return (Personnage.Egalite, defenseur.Id, attaquant.Id);
                    }
                }
            }

            return (-1, -1, -1);
        }

        public bool SansObstacle(Point source, Point destination)
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
                        if (grille[x, source.Y] != null)
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
                        if (grille[source.X, y] != null)
                            return false;
                    }
                }
            }

            return true;
        }

        public Personnage GetPiece(Point point)
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
                point = PxToCoord(point);
            }
            
            return point;
        }

        public bool PositionValide(Point point, bool typeCoord = Pixel) // vérifie que la position en px est bien dans la grille de jeu
        {
            if (typeCoord == Pixel)
            {
                if (
                    point.X >= OffsetX && point.X < OffsetX + longueurCase * casesX &&
                    point.Y >= OffsetY && point.Y < OffsetY + hauteurCase  * casesY
                )
                    return true;
            }
            else
            {
                if(
                    point.X >= 0 && point.X <= casesX &&
                    point.Y >= 0 && point.Y <= casesY
                    )
                    return true;
            }
            
            return false;
        }

        // recoit des coordonnées en pixels, et retourne la distance en nombre de cases
        public int Distance(Point source, Point destination)
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

        public Point CoordToPx(Point point)
        {
            point.X = OffsetX + point.X * longueurCase;
            point.Y = OffsetY + point.Y * hauteurCase;

            return point;
        }
        
        public Point PxToCoord(Point point)
        {
            point.X = (point.X - OffsetX) / longueurCase;
            point.Y = (point.Y - OffsetY) / hauteurCase;

            return point;
        }

        public bool DeplacementLineaire(Point origine, Point destination)
        {
            return origine.X == destination.X || origine.Y == destination.Y;
        }
    }
}