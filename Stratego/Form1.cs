using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Stratego.Personnages;

//todo cases vertes et rouges
namespace Stratego
{
    public partial class Form1 : Form
    {
        private readonly Map map;
        
        private Bitmap fond;
        private readonly List<Rectangle> positionPieces;
        private Rectangle aireJeu;

        private readonly JeuRegles jeu;

        // Déplacement pièce
        private bool drag; // si on a activé le drag&drop
        private int idDragged; // élément sélectionné

        private Point positionOrigine;
        public Form1()
        {
            InitializeComponent();
            
            map = new Map();
            
            positionPieces = new List<Rectangle>();
            
            jeu = new JeuRegles();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fond = new Bitmap(map.AireJeu);
            aireJeu = new Rectangle(0,0, 612, 800);
            
            // charge fichier xml des différentes pièces
            jeu.OuvreXmlClasses(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName) + @"\ListePieces.xml");
            
            jeu.GenerePieces(map, positionPieces);
            
            GenereMenu(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName) + @"\ListePieces.xml");
        }
        
        private bool ClasseExiste(string typeName) {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Any(type => type.Name == typeName);
        }

        private void GenereMenu(string chemin)
        {
            ContextMenu contextMenu = new ContextMenu();
            
            pictureBox1.ContextMenu = contextMenu;
            
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

                contextMenu.MenuItems.Add(nombrePieces + " - " + nomPiece);

                // reset les valeurs pour lire la prochaine pièce
                nomPiece = null;
                nombrePieces = 0; // remet à 0 le nombre de pièces à chaque tour de boucle
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) // relâchement clic souris
        {
            if (!drag) return; // si la pièce n'est pas sélectionnée ce n'est pas la peine de continuer
            
            Point position = map.TrouveCase(e.Location);
            
            Personnage attaquant = map.TrouvePersoParId(idDragged);
            Personnage defenseur = map.GetPiece(position, Map.Pixel);
                
            // si le déplacement est valide pour la pièce
            if(position.X != -1 && map.ConditionsDeplacement(idDragged, positionOrigine, map.PxToCoord(position))){
                (int collision, int piece1, int piece2) = map.DeplacePiece(positionOrigine, map.PxToCoord(position));

                JeuRegles.GenereHistoriqueDialogue(richTextBox1, attaquant, defenseur, collision);
                    
                if (collision == Personnage.Vide) // si la case de destination est vide
                    RedessinePiece(idDragged, position, false);
                else if(collision == Personnage.Attaquant)
                    RedessinePiece(idDragged, position, false);

                EffacePiece(piece1);
                EffacePiece(piece2);
            }
            else // sinon on la replace à sa position d'origine
                RedessinePiece(idDragged, Map.CoordToPx(positionOrigine), false);

            idDragged = -1;
            drag = false; // désactive le drag&drop
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag) // si le drag&drop est activé
                RedessinePiece(idDragged, e.Location);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) // enfoncement clic souris
        {
            positionOrigine = map.TrouveCase(e.Location, Map.Coord); // trouve la case en coord où on a cliqué

            Personnage persoSelectionne = map.GetPiece(positionOrigine);
            
            // vérifie qu'il y a bien une pièce dans la case et que la pièce soit déplaçable
            if (persoSelectionne != null && persoSelectionne.Deplacement > 0)
            {
                drag = true; // active le drag&drop
                idDragged = persoSelectionne.Id;

                RedessinePiece(idDragged, e.Location);
            }
        }

        private void RedessinePiece(int id, Point point, bool centrePiece = true)
        {
            Personnage personnage = map.TrouvePersoParId(id);

            if (!map.PositionValide(point) || personnage == null) return;
            
            if (centrePiece) // si on doit centrer l'image au centre du curseur
                personnage.Piece.CentrePiece(ref point);
            
            // calcule ses nouvelles coordonnées
            positionPieces[id].Point = point;
            
            pictureBox1.Invalidate(); // supprime l'image
        }

        private void EffacePiece(int id)
        {
            if (id < 0) return;
            
            positionPieces[id] = null;
            
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(fond, aireJeu.Rect); // repeint la grille

            Personnage personnage;

            // redessine les pièces
            for (int id = 0; id < positionPieces.Count; id++)
            {
                personnage = map.TrouvePersoParId(id);
                
                if(personnage != null) // ne dessine que les pièces valides
                    e.Graphics.DrawImage(personnage.Piece.Image, positionPieces[id].Rect);
            }
        }
    }
}