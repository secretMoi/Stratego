using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Timers;
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
        
        private static System.Timers.Timer loopTimer;

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
            positionPieces.Add(new Rectangle(Map.OffsetX, Map.OffsetY, personnage.DimensionPieceX, personnage.DimensionPieceY));
            
            tv = CreateGraphics();
            
            // timer qui se déclenche lorsque l'on clique dans la tv et sert à déplacer une figure
            /*loopTimer = new System.Timers.Timer();
            loopTimer.Interval = 15; //interval in milliseconds
            loopTimer.Enabled = false; // désactive par défaut pour limiter les ressources
            loopTimer.Elapsed += loopTimerEvent; // à effectuer entre les 2 clics souris
            loopTimer.AutoReset = true; // le ré enclenche à la fin
            MouseDown += mouseDownEvent; // active le timer lors du clic
            MouseUp += mouseUpEvent; // le désactive*/
        }

        private void loopTimerEvent(Object source, ElapsedEventArgs e)
        {
            int resX, resY;
            resX = Cursor.Position.X - DesktopLocation.X - decalageStatiqueSourisX;
            resY = Cursor.Position.Y - DesktopLocation.Y - decalageStatiqueSourisY;
            // position absolue du curseur -
            // position de la fenetre -
            // position de la tv -
            // les bordures -
            // une marge pour la taille du curseur souris
            /*figureCourante.Positionne(Cursor.Position.X - DesktopLocation.X - decalageStatiqueSourisX,
                Cursor.Position.Y - DesktopLocation.Y - decalageStatiqueSourisY);*/

            Invalidate(positionPieces[0].Rect);

            positionPieces[0].X = resX;
            positionPieces[0].Y = resY;
            
            //tv.DrawImage(fond, aireJeu.Rect);
            //tv.DrawImage(pieces[0], positionPieces[0].Rect);

            // démarre un nouveau thread servant à ne pas effacer les autres figures
            /*Thread tRafraichir = new Thread(Rafraichir);
            tRafraichir.Start();*/
        }
        private void mouseDownEvent(object sender, MouseEventArgs e)
        {
            loopTimer.Enabled = true;
        }
        private void mouseUpEvent(object sender, MouseEventArgs e)
        {
            loopTimer.Enabled = false;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
            
            tv.DrawImage(pieces[0], positionPieces[0].Rect);
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                tv.DrawImage(pieces[0], positionPieces[0].Rect);
                positionPieces[0].X = e.X;
                positionPieces[0].Y = e.Y;
                
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            
            positionPieces[0].X = e.X;
            positionPieces[0].Y = e.Y;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(fond, aireJeu.Rect);
            e.Graphics.DrawImage(pieces[0], positionPieces[0].Rect);
        }
    }
    
    
}