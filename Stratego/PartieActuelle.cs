using System;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Stratego
{
    [Serializable]
    public class PartieActuelle : ISerializable
    {
        private Rectangle aireJeu;
        private JeuRegles jeu;
        private MenuContextuel menuContextuel;

        //private Point positionOrigine; // position de départ de la pièce déplacée
        
        // serialise
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("AireJeu", aireJeu, typeof(Rectangle));
            info.AddValue("Jeu", jeu, typeof(JeuRegles));
            info.AddValue("MenuContextuel", menuContextuel, typeof(MenuContextuel));
        }
        
        // deserialise
        public PartieActuelle(SerializationInfo info, StreamingContext context)
        {
            aireJeu = (Rectangle) info.GetValue("AireJeu", typeof(Rectangle));
            jeu = (JeuRegles) info.GetValue("Jeu", typeof(JeuRegles));
            menuContextuel = (MenuContextuel) info.GetValue("MenuContextuel", typeof(MenuContextuel));
        }

        public PartieActuelle(PictureBox pictureBox)
        {
            jeu = new JeuRegles("ListePieces.xml");
            
            aireJeu = new Rectangle(0,0, 612, 800);
            
            menuContextuel = new MenuContextuel(pictureBox);
            menuContextuel.GenereMenu(jeu);
        }

        public Rectangle AireJeu
        {
            get => aireJeu;
            set => aireJeu = value;
        }

        public JeuRegles Jeu
        {
            get => jeu;
            set => jeu = value;
        }

        public MenuContextuel MenuContextuel
        {
            get => menuContextuel;
            set => menuContextuel = value;
        }

        /*public Point PositionOrigine
        {
            get => positionOrigine;
            set => positionOrigine = value;
        }*/
    }
}