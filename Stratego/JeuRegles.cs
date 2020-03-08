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
        private bool tourActuel = Personnage.Bleu;
        private readonly Bitmap imagePieceAdverse;
        private readonly Bitmap imagePieceAlliee;

        public JeuRegles(string chemin)
        {
            imagePieceAdverse = new Bitmap(@"images/pieceAdverse.jpg");
            imagePieceAlliee = new Bitmap(@"images/pieceAlliee.jpg");
            listePieces = new Dictionary<string, int>();
            XmlTextReader listePiecesXml = new XmlTextReader(chemin);
            
            string nomPiece = null;
            int nombrePieces = 0; // nombre de fois qu'une pièce peut être placée

            while (listePiecesXml.Read()) // parcours le fichier XML
            {
                if (listePiecesXml.NodeType == XmlNodeType.Element && listePiecesXml.Name == "name") // récupère le nom
                    nomPiece = listePiecesXml.ReadElementString();
                if (listePiecesXml.NodeType == XmlNodeType.Element && listePiecesXml.Name == "nombre") // récupère le nb de pièces
                    nombrePieces = Convert.ToInt32(listePiecesXml.ReadElementString());
                
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

        public void ChangeTour()
        {
            tourActuel = !tourActuel;
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

        public Personnage GenereUnePiece(string nomPiece, Point position, bool equipe)
        {
            string @namespace = "Stratego.Personnages";
            string @class = nomPiece;
            Personnage personnage = null;

            Type typeClasse = Type.GetType($"{@namespace}.{@class}"); // trouve la classe
            if(typeClasse != null)
                personnage = Activator.CreateInstance(typeClasse) as Personnage; // instancie un objet

            if (personnage == null) return null;
            
            personnage.Hydrate(Personnage.AugmenteNombrePieces(), Map.CasesX, position, equipe); // hydrate l'objet

            return personnage;
        }

        public List<Point> ListeCasesInterdites()
        {
            List<Point> casesInterdites = new List<Point>
            {
                // lac gauche
                new Point(2, 4),
                new Point(3, 4),
                new Point(2, 5),
                new Point(3, 5),
                
                // lac droit
                new Point(6, 4),
                new Point(7, 4),
                new Point(6, 5),
                new Point(7, 5)
            };

            return casesInterdites;
        }

        public Bitmap ImagePiece(Personnage personnage)
        {
            if (personnage.Equipe == tourActuel)
                return personnage.Piece.Image;
            else if (tourActuel == Personnage.Bleu)
                return imagePieceAdverse;
            else
                return imagePieceAlliee;
        }

        public Dictionary<string, int> ListePieces => listePieces;

        public bool TourActuel => tourActuel;
    }
}