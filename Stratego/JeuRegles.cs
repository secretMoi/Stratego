using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Stratego.Personnages;

namespace Stratego
{
    public class JeuRegles
    {
        private Form1 fenetrePrincipale;
        private readonly Dictionary<String, int> listePieces;

        public JeuRegles(string chemin)
        {
            this.listePieces = new Dictionary<string, int>();
            XmlTextReader listePieces = new XmlTextReader(chemin);
            
            string nomPiece = null;
            int nombrePieces = 0; // nombre de fois qu'une pièce peut être placée

            while (listePieces.Read()) // parcours le fichier XML
            {
                if (listePieces.NodeType == XmlNodeType.Element && listePieces.Name == "name") // récupère le nom
                    nomPiece = listePieces.ReadElementString();
                if (listePieces.NodeType == XmlNodeType.Element && listePieces.Name == "nombre") // récupère le nb de pièces
                    nombrePieces = Convert.ToInt32(listePieces.ReadElementString());
                
                if(nomPiece == null || nombrePieces == 0) continue; // tant qu'on a pas le nom et le nb de pièces on continue de parcourir

                if (!ClasseExiste(nomPiece))
                    MessageBox.Show(@"Pièce erronnée : " + nomPiece);

                this.listePieces.Add(nomPiece, nombrePieces);

                // reset les valeurs pour lire la prochaine pièce
                nomPiece = null;
                nombrePieces = 0; // remet à 0 le nombre de pièces à chaque tour de boucle
            }
        }
        
        // Vérifie qu'une classe existe
        private bool ClasseExiste(string typeName) {
            /*foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                foreach (Type type in assembly.GetTypes()) {
                    if (type.Name == typeName)
                        return true;
                }
            }*/

            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Any(type => type.Name == typeName);
        }

        private static void AjoutTexte(RichTextBox richTextBox, string texte, Color color)
        {
            richTextBox.SelectionStart = richTextBox.TextLength;
            richTextBox.SelectionLength = 0;

            richTextBox.SelectionColor = color;
            richTextBox.AppendText(texte);
            richTextBox.SelectionColor = richTextBox.ForeColor;
        }
        
        public void GenereMenu(ContextMenu contextMenu, Form1 fenetrePrincipale)
        {
            this.fenetrePrincipale = fenetrePrincipale;

            foreach(KeyValuePair<string, int> piece in listePieces)
                contextMenu.MenuItems.Add(piece.Value + " - " + piece.Key, Menu_OnClick);
        }

        private void Menu_OnClick(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            fenetrePrincipale.MenuPictureBox(menuItem);
        }

        public static void GenereHistoriqueDialogue(RichTextBox richTextBox, Personnage attaquant, Personnage defenseur, int resultat)
        {
            if(attaquant == null || defenseur == null) return;
            
            AjoutTexte(richTextBox, attaquant.ToString(), attaquant.Couleur());
            AjoutTexte(richTextBox, " attaque ", Color.Black);
            AjoutTexte(richTextBox, defenseur.ToString(), defenseur.Couleur());
            AjoutTexte(richTextBox, Environment.NewLine, Color.Black);

            switch (resultat)
            {
                case Personnage.Attaquant:
                    AjoutTexte(richTextBox, defenseur.ToString(), defenseur.Couleur());
                    AjoutTexte(richTextBox, " est mort", Color.Black);
                    break;
                case Personnage.Defenseur:
                    AjoutTexte(richTextBox, attaquant.ToString(), attaquant.Couleur());
                    AjoutTexte(richTextBox, " est mort", Color.Black);
                    break;
                default:
                    AjoutTexte(richTextBox, attaquant.ToString(), attaquant.Couleur());
                    AjoutTexte(richTextBox, " et ", Color.Black);
                    AjoutTexte(richTextBox, defenseur.ToString(), defenseur.Couleur());
                    AjoutTexte(richTextBox, " sont morts", Color.Black);
                    break;
            }
            
            AjoutTexte(richTextBox, Environment.NewLine + Environment.NewLine, Color.Black);
        }

        public Personnage GenereUnePiece(string nomPiece, Point position)
        {
            string @namespace = "Stratego.Personnages";
            string @class = nomPiece;
            Personnage personnage = null;

            Type typeClasse = Type.GetType($"{@namespace}.{@class}"); // trouve la classe
            if(typeClasse != null)
                personnage = Activator.CreateInstance(typeClasse) as Personnage; // instancie un objet

            if (personnage == null) return null;
            
            personnage.Hydrate(Personnage.AugmenteNombrePieces(), Map.CasesX, position); // hydrate l'objet

            return personnage;
        }

        public void GenerePieces(Map map, List<Rectangle> positionPieces)
        {
            Point position = new Point(0, Map.CasesY - 1); // position de la pièce à placer

            Personnage personnage;

            foreach (KeyValuePair<string, int> piece in listePieces)
            {
                for (int i = 0; i < piece.Value; i++) // génère les nb de pièces indiquées par le fichier XML
                {
                    if (position.X == 10) // passe à la ligne suivante lorsqu'on arrive à la dernière colonne X
                    {
                        position.X = 0;
                        position.Y--;
                    }
                    
                    personnage = GenereUnePiece(piece.Key, position);

                    if (personnage != null)
                    {
                        positionPieces.Add(new Rectangle(Map.CoordToPx(personnage.Position), personnage.Piece.Dimension)); // position de l'image
                        map.SetPositionPiece(personnage.Position, personnage); // indique à la map ce qu'elle contient
                    
                        position.X++;
                    }
                }
            }
        }

        public Dictionary<string, int> ListePieces => listePieces;
    }
}