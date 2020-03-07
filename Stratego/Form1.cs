using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Stratego.Personnages;

//todo cases vertes et rouges
//todo cacher les pièces à chaque tour
//todo chaque joueur ne peut faire qu'un déplacement par tour
//todo menu (aide, sauvegarder partie, reprendre partie...)
//todo zone tuto premièe prise en main
//todo détection fin de partie
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
        private bool placementPieces; // si on place les pièces avant le début du jeu
        private int idDragged; // élément sélectionné

        private Point positionOrigine; // position de départ de la pièce déplacée
        public Form1()
        {
            InitializeComponent();

            positionPieces = new List<Rectangle>();
            
            jeu = new JeuRegles("ListePieces.xml");
            
            map = new Map(jeu.ListeCasesInterdites());
            
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
            if(positionOrigine.X == -1)
            {
                MessageBox.Show(@"Case invalide !");
                return;
            }

            bool equipe = Personnage.Bleu;
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
            Personnage caseCible = map.GetPiece(positionOrigine);
            if (caseCible != null)
            {
                MessageBox.Show(@"Case occupée !");
                return;
            }

            if (Personnage.GetNombrePieces() >= 40)
                equipe = Personnage.Rouge;

            // crée la pièce
            if (GenereUnePiece(nomPiece, positionOrigine, equipe) == null)
                return;

            menuItem.Text = --nombrePieceRestante + @" - " + nomPiece; // actualise le texte de l'item

            // si toutes les pièces sont placées
            DesactiveMenuContextuel();

            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) // relâchement clic souris
        {
            if (!drag || placementPieces) return; // si la pièce n'est pas sélectionnée ce n'est pas la peine de continuer
            
            Point position = map.TrouveCase(e.Location);
            
            Personnage attaquant = map.TrouvePersoParId(idDragged);
            Personnage defenseur = map.GetPiece(position, Map.Pixel);

            // si les 2 pièces sont de la même équipe
            if (defenseur != null && attaquant.Equipe == defenseur.Equipe)
                RedessinePiece(idDragged, Map.CoordToPx(positionOrigine), false);
            // si le déplacement est valide pour la pièce
            else if(position.X != -1 && map.ConditionsDeplacement(idDragged, positionOrigine, map.PxToCoord(position)))
            {
                (int collision, int piece1, int piece2) = map.DeplacePiece(positionOrigine, map.PxToCoord(position));

                JeuRegles.GenereHistoriqueDialogue(richTextBox1, attaquant, defenseur, collision); // affiche l'action
                
                RedessinePiece(idDragged, position, false); // redessine la pièce à sa position finale

                // efface les pièces qui doivent l'être
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
            positionOrigine = map.TrouveCase(e.Location, Map.Coord); // trouve la case en coord où on a cliqué
            
            if (placementPieces)
                return;
            
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
            
            pictureBox1.Invalidate();
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
            bool equipe = Personnage.Bleu;
            Point caseCourante = new Point(0, Map.CasesY - 1); // position de la pièce à placer
            List<Point> listeCases = new List<Point>(40);
            Random positionAleatoire = new Random();
            int positionChoisie;
            if (buttonRemplir.Text.Contains("rouges"))
            {
                caseCourante.Y = 3;
                equipe = Personnage.Rouge;
            }

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

                    GenereUnePiece(piece.Key, listeCases[positionChoisie], equipe);
                
                    listeCases.RemoveAt(positionChoisie);
                }
            }

            if (buttonRemplir.Text.Contains("rouges"))
            {
                buttonRemplir.Enabled = false;
                DesactiveMenuContextuel();
            }
            else
                buttonRemplir.Text = buttonRemplir.Text.Replace("bleus", "rouges");
            
            pictureBox1.Invalidate();
        }

        private Personnage GenereUnePiece(string nomPiece, Point position, bool equipe)
        {
            Personnage personnage = jeu.GenereUnePiece(nomPiece, position, equipe);

            if (personnage == null)
            {
                MessageBox.Show(@"Création de la pièce " + nomPiece + @" impossible !");
                return null;
            }
            
            positionPieces.Add(new Rectangle(Map.CoordToPx(personnage.Position), personnage.Piece.Dimension)); // position de l'image
            map.SetPositionPiece(personnage.Position, personnage); // indique à la map ce qu'elle contient

            return personnage;
        }

        private void DesactiveMenuContextuel()
        {
            if (Personnage.GetNombrePieces() == 80)
            {
                placementPieces = false;
                pictureBox1.ContextMenu.Dispose();
            }
        }
    }
}