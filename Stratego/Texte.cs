using System.Collections.Generic;
using System.Drawing;

namespace Stratego
{
    public class Texte
    {
        private static Font Font = new Font("Arial", 16);
        private static SolidBrush Brosse = new SolidBrush(Color.Black);
        
        private static List<Texte> liste = new List<Texte>();
        
        private static Font font;
        private static SolidBrush brosse;
        
        public Texte()
        {
            if(font == null) font = Font;
            if(brosse == null) brosse = Brosse;
            
            liste.Add(this);
        }

        public void SetFont(string police, int taille)
        {
            font = new Font(police, taille);
        }
        
        public void SetBrosse(Color couleur)
        {
            brosse = new SolidBrush(couleur);
        }
        
        public void SetDefautFont(string police, int taille)
        {
            Font = new Font(police, taille);
        }
        
        public void SetDefautBrosse(Color couleur)
        {
            Brosse = new SolidBrush(couleur);
        }
        
        public static void SetAllFont(string police, int taille)
        {
            foreach (Texte texte in liste)
                texte.SetFont(police, taille);
        }
        
        public static void SetAllBrosse(Color couleur)
        {
            foreach (Texte texte in liste)
                texte.SetBrosse(couleur);
        }
    }
}