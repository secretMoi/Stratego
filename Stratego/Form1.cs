using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using Stratego.Personnages;

//todo cases vertes et rouges

namespace Stratego
{
    public partial class Form1 : Form
    {
        private readonly Map map;
        
        private readonly Bitmap fond;
        private readonly List<Rectangle> positionPieces;
        private readonly Rectangle aireJeu;

        private readonly JeuRegles jeu;

        // Déplacement pièce
        private bool drag; // si on a activé le drag&drop
        private Point dernierClic;
        private bool placementPieces; // si on place les pièces avant le début du jeu
        private int idDragged; // élément sélectionné

        private Point positionOrigine;
        public Form1()
        {
            InitializeComponent();
            
            map = new Map();
            
            positionPieces = new List<Rectangle>();
            
            jeu = new JeuRegles("ListePieces.xml");
            
            fond = new Bitmap(map.AireJeu);
            aireJeu = new Rectangle(0,0, 612, 800);

            placementPieces = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.ContextMenu = new ContextMenu();
            
            jeu.GenereMenu(pictureBox1.ContextMenu, this);
        }

        public void MenuPictureBox(MenuItem menuItem)
        {
            if(dernierClic.X == -1)
            {
                MessageBox.Show(@"Case invalide !");
                return;
            }
            string[] chaineItem = menuItem.Text.Split('-'); // récupère la chaine de l'item sélectionné
            int nombrePieceRestante = Convert.ToInt32(chaineItem[0].Trim()); // récupère le nombre de pièces pouvant encore être placées
            string nomPiece = chaineItem[1].Trim(); // récupère le nom de la pièce

            // si on ne peut plus poser de pièces
            if (nombrePieceRestante < 1)
            {
                MessageBox.Show(@"Pièce épuisée");
                return;
            }
            
            // si la case cible est déjà occupée
            Personnage caseCible = map.GetPiece(dernierClic);
            if (caseCible != null)
            {
                MessageBox.Show(@"Case occupée !");
                return;
            }

            // crée la pièce
            if (GenereUnePiece(nomPiece, dernierClic) == null)
                return;

            menuItem.Text = --nombrePieceRestante + @" - " + nomPiece; // actualise le texte de l'item

            // si toutes les pièces sont placées
            if (Personnage.GetNombrePieces() == 80)
            {
                placementPieces = false;
                pictureBox1.ContextMenu.Dispose();
            }
            
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) // relâchement clic souris
        {
            if(placementPieces) return;
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
            if(placementPieces) return;
            
            if (drag) // si le drag&drop est activé
                RedessinePiece(idDragged, e.Location);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) // enfoncement clic souris
        {
            if (placementPieces)
            {
                dernierClic = map.TrouveCase(e.Location, Map.Coord);
                return;
            }
            
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

        private void buttonRemplir_Click(object sender, EventArgs e)
        {
            Point caseDepart = new Point(0, Map.CasesY - 1); // position de la pièce à placer
            Point caseCourante = caseDepart;
            List<Point> listeCases = new List<Point>(40);
            Random positionAleatoire = new Random();
            int positionChoisie;
            //if(buttonRemplir.Text.Contains("rouge"))

            // création de la liste
            for (int i = 0; i < 40; i++)
            {
                listeCases.Add(caseCourante);
                caseCourante.X++;

                if (caseCourante.X == 10)
                {
                    caseCourante.X = 0;
                    caseCourante.Y--;
                }
            }

            foreach(KeyValuePair<string, int> piece in jeu.ListePieces)
            {
                for (int repetitionPiece = 0; repetitionPiece < piece.Value; repetitionPiece++)
                {
                    positionChoisie = positionAleatoire.Next(listeCases.Count);

                    GenereUnePiece(piece.Key, listeCases[positionChoisie]);
                
                    listeCases.RemoveAt(positionChoisie);
                }
            }
            
            //jeu.GenerePieces(map, positionPieces);

            placementPieces = false;
            
            pictureBox1.Invalidate();
        }

        private Personnage GenereUnePiece(string nomPiece, Point position)
        {
            Personnage personnage = jeu.GenereUnePiece(nomPiece, position);

            if (personnage == null)
            {
                MessageBox.Show("Création de la pièce " + nomPiece + " impossible !");
                return null;
            }
            
            positionPieces.Add(new Rectangle(Map.CoordToPx(personnage.Position), personnage.Piece.Dimension)); // position de l'image
            map.SetPositionPiece(personnage.Position, personnage); // indique à la map ce qu'elle contient

            return personnage;
        }
    }
}