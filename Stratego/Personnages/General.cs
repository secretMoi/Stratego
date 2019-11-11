using System.Drawing;

namespace Stratego.Personnages
{
    public class General : Personnage

    {
    public General(int id, Point point) : base(id, point)
    {
        puissance = 9;
        type = "marechal";

        piece = new Pieces(type);
    }
    }
}