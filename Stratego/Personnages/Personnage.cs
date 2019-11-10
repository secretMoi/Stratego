using System.Collections.Generic;
using System.Drawing;

namespace Stratego.Personnages
{
    public class Personnage
    {
        protected readonly Map map; // contient un objet map pour intéragir
        protected Pieces piece;

        protected int deplacement; // nombre de cases que peut parcourir le personnage
        protected Point position; // sa position courante dans la map
        protected int id;

        protected int puissance;
        protected string type;

        public Personnage(int id, Point point)
        {
            deplacement = 1;

            this.id = id;
            position = point;
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

        public virtual bool DeplacementValide(Point point)
        {
            if (deplacement <= map.Distance(position, point))
                return true;
            
            return false;
        }
    }
}