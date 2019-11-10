using System.Collections.Generic;
using System.Drawing;

namespace Stratego.Personnages
{
    public class Personnage //todo séparer personnage en 2 classes, rajouter une classe pièce ::: pièce sera contenue dans personnage car plsrs persos peuvent avoir la même pièce
    {
        protected readonly Map map; // contient un objet map pour intéragir
        protected Pieces piece;

        protected int deplacement; // nombre de cases que peut parcourir le personnage
        protected Point position; // sa position courante dans la map

        public Personnage(Point point, string type) // todo supprimer besoin de map, se sera la classe appelante qui gérera ça
        {
            deplacement = 1;

            position = point;
            
            piece = new Pieces(type);
        }

        public Point Position
        {
            get => position;
            set => position = value;
        }

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

        public Pieces Piece => piece;

        public virtual bool DeplacementValide(Point point)
        {
            if (deplacement <= map.Distance(position, point))
                return true;
            
            return false;
        }
    }
}