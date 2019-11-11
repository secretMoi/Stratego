using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Stratego.Personnages
{
    public class Personnage
    {
        public const int Vide = 0;
        public const int Attaquant = 1;
        public const int Defenseur = 2;
        public const int Egalite = 3;
        
        protected Pieces piece;

        protected int deplacement; // nombre de cases que peut parcourir le personnage
        protected Point position; // sa position courante dans la map
        protected int id;

        protected int puissance;
        protected string type;
        protected bool estVivant;

        public Personnage(int id, Point point)
        {
            estVivant = true;
            deplacement = 1;

            this.id = id;
            position = point;
        }

        public int Collision(Personnage attaquant, Personnage defenseur)
        {
            if (attaquant != null && defenseur != null)
            {
                if (attaquant.Puissance > defenseur.Puissance) // si l'attaquant est plus puissant
                    return Attaquant;
                if (attaquant.Puissance < defenseur.Puissance) // si le défenseur est plus puissant
                    return Defenseur;
                if (attaquant.Puissance == defenseur.Puissance) // si même puissance
                    return Egalite;
            }

            return Vide; // sinon la case est vide
        }
        
        public Point Position
        {
            get => position;
            set => position = value;
        }

        public int Id => id;

        public int Puissance => puissance;

        public int PositionX
        {
            get => position.X;
            set => position.X = value;
        }
        public int PositionY
        {
            get => position.Y;
            set => position.Y = value;
        }

        public int Deplacement => deplacement;

        public Pieces Piece => piece;

        public void Meurt()
        {
            estVivant = false;
            
            position = new Point(-1, -1);
        }
    }
}