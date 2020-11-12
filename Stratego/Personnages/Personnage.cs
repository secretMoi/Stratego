using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace Stratego.Personnages
{
    [Serializable]
    public abstract class Personnage : IDisposable, ISerializable
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
        
        protected bool equipe;

        private static int nombrePiece;
        
        bool disposed;

        public Personnage(int id, Point point)
        {
            deplacement = 1;

            this.id = id;
            position = point;

            equipe = Bleu;
        }
        
        public Personnage()
        {
            deplacement = 1;

            equipe = Bleu;
        }
        
        public virtual void Hydrate(int id, int deplacement, Point point, bool equipe)
        {
            this.id = id;
            position = point;
            this.equipe = equipe;
            
            if (equipe == Bleu)
                type = "bleu_" + type;
            else
                type = "rouge_" + type;
            piece = new Pieces(type);
        }

        public static int AugmenteNombrePieces()
        {
            nombrePiece++;

            return nombrePiece - 1;
        }

        public static int GetNombrePieces()
        {
            return nombrePiece;
        }
        
        public virtual int Collision(Personnage attaquant, Personnage defenseur)
        {
            int resultat = Vide;
            if (attaquant == null || defenseur == null) return resultat; // sinon la case est vide

            if (attaquant.Puissance > defenseur.Puissance) // si l'attaquant est plus puissant
                resultat = Attaquant;
            else if (attaquant.Puissance < defenseur.Puissance) // si le défenseur est plus puissant
                resultat =  Defenseur;
            else if (attaquant.Puissance == defenseur.Puissance) // si même puissance
                resultat = Egalite;

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
            position = new Point(-1, -1);
        }

        public Color Couleur()
        {
            /*if(Equipe == Bleu)
                return Color.Navy;

            return Color.Crimson;*/

            return Theme.CouleurTexte;
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    nombrePiece--;
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Piece", piece, typeof(Pieces));
            info.AddValue("Deplacement", deplacement, typeof(int));
            info.AddValue("Position", position, typeof(Point));
            info.AddValue("Id", id, typeof(int));
            info.AddValue("Puissance", puissance, typeof(int));
            info.AddValue("NombrePiece", nombrePiece, typeof(int));
            info.AddValue("Type", type, typeof(string));
            info.AddValue("Equipe", equipe, typeof(bool));
        }
        
        // deserialise
        public Personnage(SerializationInfo info, StreamingContext context)
        {
            piece = (Pieces) info.GetValue("Piece", typeof(Pieces));
            deplacement = (int) info.GetValue("Deplacement", typeof(int));
            position = (Point) info.GetValue("Position", typeof(Point));
            id = (int) info.GetValue("Id", typeof(int));
            puissance = (int) info.GetValue("Puissance", typeof(int));
            nombrePiece = (int) info.GetValue("NombrePiece", typeof(int));
            type = (string) info.GetValue("Type", typeof(string));
            equipe = (bool) info.GetValue("Equipe", typeof(bool));
        }
    }
}