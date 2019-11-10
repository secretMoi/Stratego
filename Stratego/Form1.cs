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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fond = new Bitmap(map.AireJeu);
            aireJeu = new Rectangle(0,0, 612, 800);
            
            pieces.Add(new Bitmap(@"C:\Users\winmo\RiderProjects\Stratego\Stratego\images\marechal.jpg"));
            positionPieces.Add(new Rectangle(map.posPieceX(9), map.posPieceY(9), Personnage.DimensionPieceX, Personnage.DimensionPieceY));
            
            tv = CreateGraphics();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) // relâchement clic souris
        {
            drag = false; // désactive le drag&drop
            //todo remettre la pièce dans sa position initiale si position invalide grâce à position origine
            Point position = map.TrouveCase(e.Location);
            
            if (position.X != -1)
                RedessinePiece(0, position, false);
                
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag) // si le drag&drop est activé
                RedessinePiece(0, e.Location);

            label1.Text = map.Distance(new Point(Map.OffsetX, Map.OffsetY), e.Location).ToString();
            //label2.Text = map.TrouveCase(e.Location, Map.Coord).ToString();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) // enfoncement clic souris
        {
            drag = true; // active le drag&drop

            RedessinePiece(0, e.Location);
        }

        private void RedessinePiece(int id, Point point, bool centrePiece = true)
        {
            if (map.PositionValide(point)) // si la position est dans la grille
            {
                if (centrePiece) // si on doit centrer l'image au centre du curseur
                {
                    point.X -= Personnage.DimensionPieceX / 2;
                    point.Y -= Personnage.DimensionPieceY / 2;
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
            e.Graphics.DrawImage(pieces[0], positionPieces[0].Rect);
        }
    }
}