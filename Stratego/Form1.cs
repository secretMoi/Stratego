using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Stratego.Personnages;

namespace Stratego
{
    public partial class Form1 : Form
    {
        private Map map;
        private Personnage personnage;
        
        private Graphics tv;
        private Bitmap fond;
        private List<Bitmap> pieces;
        private List<Rectangle> positionPieces;
        private Rectangle aireJeu;
        private List<Personnage> piecesJoueur;

        // Déplacement pièce
        private bool drag; // si on a activé le drag&drop
        private int idDragged; // élément sélectionné

        private Point positionOrigine;
        public Form1()
        {
            InitializeComponent();

            DoubleBuffered = true;
            
            map = new Map();
            
            pieces = new List<Bitmap>();
            positionPieces = new List<Rectangle>();
            
            piecesJoueur = new List<Personnage>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fond = new Bitmap(map.AireJeu);
            aireJeu = new Rectangle(0,0, 612, 800);

            for (int i = 0; i < 2; i++)
            {
                piecesJoueur.Add(new Personnage(i, new Point(i, i), "marechal")); // crée le personnage
                pieces.Add(new Bitmap(piecesJoueur[i].Piece.Chemin)); // chemin de l'image à afficher
                positionPieces.Add(new Rectangle(map.CoordToPx(piecesJoueur[i].Position), piecesJoueur[i].Piece.Dimension)); // position de l'image
                map.SetPositionPiece(piecesJoueur[i].Position, piecesJoueur[i]); // indique à la map ce qu'elle contient
            }

            tv = CreateGraphics();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) // relâchement clic souris
        {
            if (drag)
            {
                Point position = map.TrouveCase(e.Location);

                if (position.X != -1) // si la position est valide
                {
                    // si le déplacement est valide pour la pièce
                    if (piecesJoueur[idDragged].Deplacement >= map.Distance(positionOrigine, map.PxToCoord(position)))
                    {
                        RedessinePiece(idDragged, position, false);
                        
                        map.DeplacePiece(positionOrigine, map.PxToCoord(position), piecesJoueur[idDragged]);
                        piecesJoueur[idDragged].Position = map.PxToCoord(position);
                    }

                    else // sinon on la replace à sa position d'origine
                        RedessinePiece(idDragged, map.CoordToPx(positionOrigine), false);

                    idDragged = -1;
                    drag = false; // désactive le drag&drop
                }
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
            if (map.PositionValide(point)) // si la position est dans la grille
            {
                if (centrePiece) // si on doit centrer l'image au centre du curseur
                {
                    point.X -= piecesJoueur[id].Piece.Longueur / 2;
                    point.Y -= piecesJoueur[id].Piece.Hauteur / 2;
                }
                
                pictureBox1.Invalidate(); // supprime l'image

                // calcule ses nouvelles coordonnées
                positionPieces[id].Point = point;
                    
                tv.DrawImage(pieces[id], positionPieces[id].Rect); // affiche l'image avec ses nouvelles coordonnées
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(fond, aireJeu.Rect);

            for (int i = 0; i < pieces.Count; i++)
            {
                e.Graphics.DrawImage(pieces[i], positionPieces[i].Rect);
            }
            
        }
    }
}