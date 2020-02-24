using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using Stratego.Personnages;

namespace Stratego
{
    public class JeuRegles
    {
        private XmlTextReader listePieces;

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

        // Ouvre le fichier XML listant les différentes pièces et leur nombre
        public void OuvreXmlClasses(string chemin)
        {
            listePieces = new XmlTextReader(chemin);
        }

        private static void AjoutTexte(RichTextBox richTextBox, string texte, Color color)
        {
            richTextBox.SelectionStart = richTextBox.TextLength;
            richTextBox.SelectionLength = 0;

            richTextBox.SelectionColor = color;
            richTextBox.AppendText(texte);
            richTextBox.SelectionColor = richTextBox.ForeColor;
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

        public void GenerePieces(Map map, List<Rectangle> positionPieces)
        {
            string nomPiece = null;
            int nombrePieces = 0; // nombre de fois qu'une pièce peut être placée
            
            int id = 0;
            Point position = new Point(0, Map.CasesY - 1); // position de la pièce à placer

            Personnage personnage;
            
            while (listePieces.Read()) // parcours le fichier XML
            {
                if (listePieces.NodeType == XmlNodeType.Element && listePieces.Name == "name") // récupère le nom
                    nomPiece = listePieces.ReadElementString();
                if (listePieces.NodeType == XmlNodeType.Element && listePieces.Name == "nombre") // récupère le nb de pièces
                    nombrePieces = Convert.ToInt32(listePieces.ReadElementString());
                
                if(nomPiece == null || nombrePieces == 0) continue; // tant qu'on a pas le nom et le nb de pièces on continue de parcourir

                if (!ClasseExiste(nomPiece))
                    MessageBox.Show(@"Pièce erronnée : " + nomPiece);

                for (int i = 0; i < nombrePieces; i++) // génère les nb de pièces indiquées par le fichier XML
                {
                    if (position.X == 10) // passe à la ligne suivante lorsqu'on arrive à la dernière colonne X
                    {
                        position.X = 0;
                        position.Y--;
                    }

                    personnage = null;
                    
                    string @namespace = "Stratego.Personnages";
                    string @class = nomPiece;

                    var typeClasse = Type.GetType($"{@namespace}.{@class}"); // trouve la classe
                    if(typeClasse != null)
                        personnage = Activator.CreateInstance(typeClasse) as Personnage; // instancie un objet

                    if (personnage == null) continue;
                    personnage.Hydrate(id, position, Map.CasesX); // hydrate l'objet
                    positionPieces.Add(new Rectangle(Map.CoordToPx(personnage.Position), personnage.Piece.Dimension)); // position de l'image
                    map.SetPositionPiece(personnage.Position, personnage); // indique à la map ce qu'elle contient
            
                    id++;
                    position.X++;
                }
                
                // reset les valeurs pour lire la prochaine pièce
                nomPiece = null;
                nombrePieces = 0; // remet à 0 le nombre de pièces à chaque tour de boucle
            }
        }
    }
}