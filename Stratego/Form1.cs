using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Stratego.Personnages;

//todo cases vertes et rouges
//todo dialogue historique combat
//todo utiliser la bande du bas pour générer les pièces
namespace Stratego
{
    public partial class Form1 : Form
    {
        private readonly Map map;

        private Graphics tv;
        private Bitmap fond;
        private readonly List<Rectangle> positionPieces;
        private Rectangle aireJeu;
        //private readonly List<Personnage> piecesJoueur;

        private JeuRegles jeu; // todo simplifier form en mettant la logique du jeu dans cette classe

        // Déplacement pièce
        private bool drag; // si on a activé le drag&drop
        private int idDragged; // élément sélectionné

        private Point positionOrigine;
        public Form1()
        {
            InitializeComponent();
            
            map = new Map();
            
            positionPieces = new List<Rectangle>();
            //piecesJoueur = new List<Personnage>();
            
            jeu = new JeuRegles();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fond = new Bitmap(map.AireJeu);
            aireJeu = new Rectangle(0,0, 612, 800);

            jeu.ListeClasse(); // génère la liste des classes du dossier personnage
            
            // charge fichier xml des différentes pièces
            jeu.OuvreXMLClasses(@"C:\Users\winmo\RiderProjects\Stratego\Stratego\ListePieces.xml");
            
            jeu.GenerePieces(map, positionPieces);

            tv = CreateGraphics();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) // relâchement clic souris
        {
            if (drag)
            {
                Point position = map.TrouveCase(e.Location);
                label1.Text = position.ToString();
                Personnage attaquant = map.TrouvePersoParID(idDragged);

                if (position.X == -1 || attaquant == null) return;
                // si le déplacement est valide pour la pièce
                if (attaquant.Deplacement >= map.Distance(positionOrigine, map.PxToCoord(position))
                    && positionOrigine != map.PxToCoord(position) // si on ne replace pas la pièce au même endroit
                    && map.DeplacementLineaire(positionOrigine, map.PxToCoord(position)) // si la pièce ne se déplace pas en diagonal
                    && map.SansObstacle(positionOrigine, map.PxToCoord(position))) 
                {
                    (int collision, int piece1, int piece2) = map.DeplacePiece(positionOrigine, map.PxToCoord(position));

                    if (collision == Personnage.Vide) // si la case de destination est vide
                        RedessinePiece(idDragged, position, false);
                    else if(collision == Personnage.Attaquant)
                        RedessinePiece(idDragged, position, false);

                    EffacePiece(piece1);
                    EffacePiece(piece2);

                }
                else // sinon on la replace à sa position d'origine
                    RedessinePiece(idDragged, map.CoordToPx(positionOrigine), false);

                idDragged = -1;
                drag = false; // désactive le drag&drop
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if(map.grille[j, i] != null)
                        Debug.Write(map.grille[j, i].ToString().Replace("Stratego.Personnages.", ""));
                    else
                    {
                        Debug.Write("0");
                    }
                }
                
                Debug.WriteLine(" ");
            }
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
            Personnage personnage = map.TrouvePersoParID(id);

            if (!map.PositionValide(point) || personnage == null) return;
            if (centrePiece) // si on doit centrer l'image au centre du curseur
            {
                point.X -= personnage.Piece.Longueur / 2;
                point.Y -= personnage.Piece.Hauteur / 2;
            }
                
            pictureBox1.Invalidate(); // supprime l'image

            // calcule ses nouvelles coordonnées
            positionPieces[id].Point = point;
                    
            tv.DrawImage(personnage.Piece.Image, positionPieces[id].Rect); // affiche l'image avec ses nouvelles coordonnées
        }

        private void EffacePiece(int id)
        {
            if (id >= 0)
            {
                positionPieces[id] = null;
            
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(fond, aireJeu.Rect); // repeint la grille

            Personnage personnage;

            // redessine les pièces
            for (int id = 0; id < positionPieces.Count; id++)
            {
                personnage = map.TrouvePersoParID(id);
                
                if(personnage != null) // ne dessine que les pièces valides
                    e.Graphics.DrawImage(personnage.Piece.Image, positionPieces[id].Rect);
            }
            
        }
    }
}