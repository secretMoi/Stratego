using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml;
using Stratego.Personnages;
using Stratego.UserControls;

namespace Stratego
{
    [Serializable]
    public class JeuRegles : ISerializable
    {
        private Map map; // contient la map
        private int idDragged; // id de l'élément sélectionné par la souris
        private bool drag; // si on est en train de déplacer un élément
        private bool partieEnCours;
        private readonly List<Rectangle> positionPieces; // liste des positions des pièces
        private readonly Dictionary<String, int> listePieces; // liste dse pièces du fichier XML
        private bool tourActuel = Personnage.Bleu; // indique quelle équipe joue actuellement
        private readonly Bitmap imagePieceAdverse;
        private readonly Bitmap imagePieceAlliee;
        
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Map", map, typeof(Map));
            info.AddValue("PartieEnCours", partieEnCours, typeof(bool));
            info.AddValue("PositionPieces", positionPieces, typeof(List<Rectangle>));
            info.AddValue("ListePieces", listePieces, typeof(Dictionary<String, int>));
            info.AddValue("TourActuel", tourActuel, typeof(bool));
            info.AddValue("ImagePieceAdverse", imagePieceAdverse, typeof(Bitmap));
            info.AddValue("ImagePieceAlliee", imagePieceAlliee, typeof(Bitmap));
        }
        
        // deserialise
        public JeuRegles(SerializationInfo info, StreamingContext context)
        {
            map = (Map) info.GetValue("Map", typeof(Map));
            partieEnCours = (bool) info.GetValue("PartieEnCours", typeof(bool));
            positionPieces = (List<Rectangle>) info.GetValue("PositionPieces", typeof(List<Rectangle>));
            listePieces = (Dictionary<String, int>) info.GetValue("ListePieces", typeof(Dictionary<String, int>));
            tourActuel = (bool) info.GetValue("TourActuel", typeof(bool));
            imagePieceAdverse = (Bitmap) info.GetValue("ImagePieceAdverse", typeof(Bitmap));
            imagePieceAlliee = (Bitmap) info.GetValue("ImagePieceAlliee", typeof(Bitmap));
        }

        public JeuRegles(string chemin)
        {
            map = new Map(ListeCasesInterdites());

            partieEnCours = true;
            positionPieces = new List<Rectangle>();
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

                if (ClasseExiste(nomPiece))
                    listePieces.Add(nomPiece, nombrePieces);

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

        private void ChangeTour()
        {
            tourActuel = !tourActuel;
        }

        private static void GenereHistoriqueDialogue(RichTextBox richTextBox, Personnage attaquant, Personnage defenseur, int resultat)
        {
            if(attaquant == null || defenseur == null) return;
            
            AjoutTexte(richTextBox, attaquant.ToString(), attaquant.Couleur());
            AjoutTexte(richTextBox, " attaque ", richTextBox.ForeColor);
            AjoutTexte(richTextBox, defenseur.ToString(), defenseur.Couleur());
            AjoutTexte(richTextBox, Environment.NewLine, richTextBox.ForeColor);

            switch (resultat)
            {
                case Personnage.Attaquant:
                    AjoutTexte(richTextBox, defenseur.ToString(), defenseur.Couleur());
                    AjoutTexte(richTextBox, " est mort", richTextBox.ForeColor);
                    break;
                case Personnage.Defenseur:
                    AjoutTexte(richTextBox, attaquant.ToString(), attaquant.Couleur());
                    AjoutTexte(richTextBox, " est mort", richTextBox.ForeColor);
                    break;
                default:
                    AjoutTexte(richTextBox, attaquant.ToString(), attaquant.Couleur());
                    AjoutTexte(richTextBox, " et ", richTextBox.ForeColor);
                    AjoutTexte(richTextBox, defenseur.ToString(), defenseur.Couleur());
                    AjoutTexte(richTextBox, " sont morts", richTextBox.ForeColor);
                    break;
            }
            
            AjoutTexte(richTextBox, Environment.NewLine + Environment.NewLine, richTextBox.ForeColor);
        }
        
        public Personnage GenereUnePiece(string nomPiece, Point position)
        {
            Personnage personnage = InstancieUnePiece(nomPiece, position, tourActuel);

            if (personnage == null)
            {
	            DialogBox.Show(@"Création de la pièce " + nomPiece + @" impossible !");
                return null;
            }

            // vérifie qu'on essaye pas de placer la pièce dans un endroit invalide
            if (personnage.Equipe == Personnage.Bleu)
            {
                if (position.Y < Map.CasesY - 4)
                {
	                DialogBox.Show(@"Création impossible de la pièce à " + position);
                    personnage.Dispose();
                    return null;
                }
            }
            else
            {
                if (position.Y > 3)
                {
	                DialogBox.Show(@"Création impossible de la pièce à " + position);
                    personnage.Dispose();
                    return null;
                }
            }
            
            positionPieces.Add(new Rectangle(Map.CoordToPx(personnage.Position), personnage.Piece.Dimension)); // position de l'image
            map.SetPositionPiece(personnage.Position, personnage); // indique à la map ce qu'elle contient

            if (Personnage.GetNombrePieces() % 40 == 0)
                ChangeTour();

            return personnage;
        }

        private Personnage InstancieUnePiece(string nomPiece, Point position, bool equipe)
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

        private List<Point> ListeCasesInterdites()
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

        private Bitmap ImagePiece(Personnage personnage)
        {
            if (personnage.Equipe == tourActuel || !partieEnCours)
                return personnage.Piece.Image;
            else if (tourActuel == Personnage.Bleu)
                return imagePieceAdverse;
            else
                return imagePieceAlliee;
        }

        public void PrisePiece(ref Point positionOrigine, Point positionClic, bool placementPieces)
        {
            positionOrigine = map.TrouveCase(positionClic, Map.Coord); // trouve la case en coord où on a cliqué
            
            if (placementPieces || !partieEnCours)
                return;
            
            Personnage persoSelectionne = map.GetPiece(positionOrigine);
            if(persoSelectionne == null) return;
            
            // vérifie que la pièce soit déplacable (sinon bombe/drapeau) et que ce soit au tour de la pièce de bouger 
            if (persoSelectionne.Deplacement > 0 && persoSelectionne.Equipe == tourActuel)
            {
                drag = true; // active le drag&drop
                idDragged = persoSelectionne.Id;

                RedessinePiece(idDragged, positionClic);
            }
        }

        public void LachePiece(Point position, Point positionOrigine, RichTextBox richTextBox)
        {
            if(!drag || !partieEnCours) return;
            
            position = map.TrouveCase(position);
            
            Personnage attaquant = map.TrouvePersoParId(idDragged);
            Personnage defenseur = map.GetPiece(position, Map.Pixel);

            // si les 2 pièces sont de la même équipe
            if (defenseur != null && attaquant.Equipe == defenseur.Equipe)
                RedessinePiece(idDragged, Map.CoordToPx(positionOrigine), false);
            // si le déplacement est valide pour la pièce
            else if(position.X != -1 && map.ConditionsDeplacement(idDragged, positionOrigine, map.PxToCoord(position)))
            {
                (int collision, int piece1, int piece2) = map.DeplacePiece(positionOrigine, map.PxToCoord(position));

                GenereHistoriqueDialogue(richTextBox, attaquant, defenseur, collision); // affiche l'action
                
                RedessinePiece(idDragged, position, false); // redessine la pièce à sa position finale

                // efface les pièces qui doivent l'être
                EffacePiece(piece1);
                EffacePiece(piece2);

                ChangeTour();
                DetectionFinPartie(defenseur);
            }
            else // sinon on la replace à sa position d'origine
                RedessinePiece(idDragged, Map.CoordToPx(positionOrigine), false);
            
            drag = false; // désactive le drag&drop
        }
        
        private void DetectionFinPartie(Personnage defenseur)
        {
            if(defenseur == null) return;
            
            if (defenseur.ToString() == "Drapeau")
            {
                partieEnCours = false;
                
                string equipe = "bleue";

                if (defenseur.Equipe == Personnage.Bleu)
                    equipe = "rouge";

                DialogBox.Show(
                    @"Partie terminée !" +
                    Environment.NewLine + // retour ligne
                    @"L'équipe " + equipe + @" remporte la victoire !"
                );
            }
        }
        
        public void BougePiece(Point positionDestination)
        {
            if (drag && partieEnCours) // si le drag&drop est activé
                RedessinePiece(idDragged, positionDestination);
        }
        
        private void EffacePiece(int id)
        {
            if (id < 0) return;
            
            positionPieces[id] = null;
        }
        
        private void RedessinePiece(int id, Point point, bool centrePiece = true)
        {
            Personnage personnage = map.TrouvePersoParId(id);

            if (!map.PositionValide(point) || personnage == null) return;
            
            if (centrePiece) // si on doit centrer l'image au centre du curseur
                personnage.Piece.CentrePiece(ref point);
            
            // calcule ses nouvelles coordonnées
            positionPieces[id].Point = point;
        }

        public void DessinePieces(Graphics graphics)
        {
            Personnage personnage;

            // redessine les pièces
            for (int id = 0; id < positionPieces.Count; id++)
            {
                personnage = map.TrouvePersoParId(id);
                
                if(personnage != null) // ne dessine que les pièces valides
                    graphics.DrawImage(ImagePiece(personnage), positionPieces[id].Rect);
            }
        }

        public Dictionary<string, int> ListePieces => listePieces;

        public Map Map
        {
            get => map;
            set => map = value;
        }
    }
}