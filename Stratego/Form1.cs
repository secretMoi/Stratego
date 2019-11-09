using System;
using System.Collections.Generic;
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
        
        private int decalageStatiqueSourisX;
        private int decalageStatiqueSourisY;

        private bool drag;
        public Form1()
        {
            InitializeComponent();

            DoubleBuffered = true;
            
            map = new Map();
            personnage = new Personnage();
            
            pieces = new List<Bitmap>();
            positionPieces = new List<Rectangle>();
            
            decalageStatiqueSourisX = 2 * SystemInformation.BorderSize.Width + 7;
            decalageStatiqueSourisY = SystemInformation.CaptionHeight + 7;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fond = new Bitmap(map.AireJeu);
            aireJeu = new Rectangle(0,0, 612, 800);
            
            pieces.Add(new Bitmap(@"C:\Users\winmo\RiderProjects\Stratego\Stratego\images\marechal.jpg"));
            positionPieces.Add(new Rectangle(map.posPieceX(9), map.posPieceY(9), personnage.DimensionPieceX, personnage.DimensionPieceY));
            
            tv = CreateGraphics();
            
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false; // désactive le drag&drop
            
            MessageBox.Show(map.TrouveCase(e.Location).ToString());
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag) // si le drag&drop est activé
                RedessinePiece(0, e.Location);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true; // active le drag&drop

            RedessinePiece(0, e.Location);
        }

        private void RedessinePiece(int id, Point point)
        {
            int sourisX = point.X - personnage.DimensionPieceX / 2;
            int sourisY = point.Y - personnage.DimensionPieceY / 2;
            
            if (map.PositionValide(sourisX, sourisY )) // si la position est dans la grille
            {
                pictureBox1.Invalidate(positionPieces[id].Rect); // supprime l'image'

                // calcule ses nouvelles coordonnées
                positionPieces[id].X = sourisX;
                positionPieces[id].Y = sourisY;
                    
                tv.DrawImage(pieces[id], positionPieces[id].Rect); // affiche l'image avec ses nouvelles coordonnées'
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(fond, aireJeu.Rect);
            e.Graphics.DrawImage(pieces[0], positionPieces[0].Rect);
        }
    }
}