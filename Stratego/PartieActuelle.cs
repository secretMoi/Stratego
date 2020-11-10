using System;
using System.Runtime.Serialization;
using System.Windows.Forms;
using Stratego.Reseau.Models;

namespace Stratego
{
    [Serializable]
    public class PartieActuelle : ISerializable, IModelReseau
    {
        private Rectangle aireJeu;
        private JeuRegles jeu;
        private MenuContextuel menuContextuel;
        private Options options;
        
        // serialise
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("AireJeu", aireJeu, typeof(Rectangle));
            info.AddValue("Jeu", jeu, typeof(JeuRegles));
            info.AddValue("MenuContextuel", menuContextuel, typeof(MenuContextuel));
            info.AddValue("Options", options, typeof(Options));
        }
        
        // deserialise
        public PartieActuelle(SerializationInfo info, StreamingContext context)
        {
            aireJeu = (Rectangle) info.GetValue("AireJeu", typeof(Rectangle));
            jeu = (JeuRegles) info.GetValue("Jeu", typeof(JeuRegles));
            menuContextuel = (MenuContextuel) info.GetValue("MenuContextuel", typeof(MenuContextuel));
            options = (Options) info.GetValue("Options", typeof(Options));
        }

        public PartieActuelle(PictureBox pictureBox)
        {
	        options = Options.Instance;
            jeu = new JeuRegles(options.GetOption("EmplacementPiece"));
            
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

        public Options Option => options;
    }
}