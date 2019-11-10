﻿using System;
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
            
            piecesJoueur.Add(new Personnage(new Point(9, 9), "marechal")); // crée le personnage
            pieces.Add(new Bitmap(piecesJoueur[0].Piece.Chemin)); // chemin de l'image à afficher
            positionPieces.Add(new Rectangle(map.CoordToPx(piecesJoueur[0].Position), piecesJoueur[0].Piece.Dimension)); // position de l'image
            map.SetPositionPiece(piecesJoueur[0].Position, piecesJoueur[0]); // indique à la map ce qu'elle contient

            tv = CreateGraphics();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) // relâchement clic souris
        {
            drag = false; // désactive le drag&drop
            //todo remettre la pièce dans sa position initiale si position invalide grâce à position origine
            Point position = map.TrouveCase(e.Location);

            if (position.X != -1) // si la position est valide
            {
                Debug.WriteLine(piecesJoueur[0].Deplacement);
                Debug.WriteLine(positionOrigine);
                Debug.WriteLine(map.PxToCoord(position));
                Debug.WriteLine(map.Distance(positionOrigine,  map.PxToCoord(position)));
                if(piecesJoueur[0].Deplacement <= map.Distance(positionOrigine,  map.PxToCoord(position)))
                    RedessinePiece(0, position, false);
                else
                    RedessinePiece(0, positionOrigine, false);
            }

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag) // si le drag&drop est activé
                RedessinePiece(0, e.Location);

            label1.Text = map.Distance(new Point(Map.OffsetX, Map.OffsetY), e.Location).ToString();
            label2.Text = positionOrigine.ToString();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) // enfoncement clic souris
        {
            drag = true; // active le drag&drop
            // todo modifier positionOrigine
            positionOrigine = map.TrouveCase(e.Location, Map.Coord);

            RedessinePiece(0, e.Location);
        }

        private void RedessinePiece(int id, Point point, bool centrePiece = true)
        {
            if (map.PositionValide(point)) // si la position est dans la grille
            {
                if (centrePiece) // si on doit centrer l'image au centre du curseur
                {
                    point.X -= piecesJoueur[0].Piece.Longueur / 2;
                    point.Y -= piecesJoueur[0].Piece.Hauteur / 2;
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