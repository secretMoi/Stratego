using System.Drawing;

namespace Stratego.Personnages
{
    public abstract class Personnage
    {
        public const int Vide = 0;
        public const int Attaquant = 1;
        public const int Defenseur = 2;
        public const int Egalite = 3;

        public const bool Bleu  = true;
        public const bool Rouge = false;

        protected Pieces piece;

        protected int deplacement; // nombre de cases que peut parcourir le personnage
        protected Point position; // sa position courante dans la map
        protected int id;

        protected int puissance;
        protected string type;
        protected bool estVivant;
        // todo implémenter équipe pour ne pas tuer ses pièces
        protected bool equipe;

        public Personnage(int id, Point point)
        {
            estVivant = true;
            deplacement = 1;

            this.id = id;
            position = point;

            equipe = Bleu;
        }
        
        public Personnage()
        {
            estVivant = true;
            deplacement = 1;

            equipe = Bleu;
        }
        
        public virtual void Hydrate(int id, int deplacement, Point point)
        {
            this.id = id;
            position = point;
        }

        public virtual int Collision(Personnage attaquant, Personnage defenseur)
        {
            int resultat = Vide;
            if (attaquant != null && defenseur != null)
            {
                if (attaquant.Puissance > defenseur.Puissance) // si l'attaquant est plus puissant
                    resultat = Attaquant;
                else if (attaquant.Puissance < defenseur.Puissance) // si le défenseur est plus puissant
                    resultat =  Defenseur;
                else if (attaquant.Puissance == defenseur.Puissance) // si même puissance
                    resultat = Egalite;
            }

            return resultat; // sinon la case est vide
        }
        
        public Point Position
        {
            get => position;
            set => position = value;
        }

        public int Id => id;

        public bool Equipe => equipe;

        public int Puissance => puissance;

        public int Deplacement => deplacement;

        public Pieces Piece => piece;

        public void Meurt()
        {
            estVivant = false;
            
            position = new Point(-1, -1);
        }

        public Color Couleur()
        {
            if(Equipe == Bleu)
                return Color.Navy;

            return Color.Crimson;
        }
    }
}