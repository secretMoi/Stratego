using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Stratego.Personnages;

//todo cases vertes et rouges
//todo menu (aide, sauvegarder partie, reprendre partie, options...)
//todo zone tuto premièe prise en main
//todo détection fin de partie
//todo compléter aléatoire au lieu d'écraser les pièces
//todo ne placer ses pièces avec le menu que dans la zone indiquée
//todo fenetre menu (son, Anti-alias, emplacement sauvegarde, activé/désactivé historique combat...)
namespace Stratego
{
    public partial class Form1 : Form
    {
        private readonly Map map;
        
        private readonly Bitmap fond;
        private readonly Rectangle aireJeu;

        private readonly JeuRegles jeu;
        
        private bool placementPieces; // si on place les pièces avant le début du jeu

        private Point positionOrigine; // position de départ de la pièce déplacée
        public Form1()
        {
            InitializeComponent();
            
            jeu = new JeuRegles("ListePieces.xml");
            map = new Map(jeu.ListeCasesInterdites());
            jeu.Map = map;
            
            fond = new Bitmap(map.AireJeu);
            aireJeu = new Rectangle(0,0, 612, 800);

            placementPieces = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.ContextMenu = new ContextMenu();
            
            GenereMenu();
        }

        public void MenuPictureBox(MenuItem menuItem)
        {
            if(positionOrigine.X == -1)
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
            Personnage caseCible = map.GetPiece(positionOrigine);
            if (caseCible != null)
            {
                MessageBox.Show(@"Case occupée !");
                return;
            }

            // crée la pièce
            if (jeu.GenereUnePiece(nomPiece, positionOrigine) == null)
                return;

            menuItem.Text = --nombrePieceRestante + @" - " + nomPiece; // actualise le texte de l'item

            // si toutes les pièces sont placées
            DesactiveMenuContextuel();

            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) // relâchement clic souris
        {
            if (placementPieces) return; // si la pièce n'est pas sélectionnée ce n'est pas la peine de continuer
            
            jeu.LachePiece(e.Location, positionOrigine, richTextBox1);

            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(placementPieces) return;
            
            jeu.BougePiece(e.Location);
            
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) // enfoncement clic souris
        {
            jeu.PrisePiece(ref positionOrigine, e.Location, placementPieces);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(fond, aireJeu.Rect); // repeint la grille

            jeu.DessinePieces(e.Graphics);
        }

        private void buttonRemplir_Click(object sender, EventArgs e)
        {
            Point caseCourante = new Point(0, Map.CasesY - 1); // position de la pièce à placer
            List<Point> listeCases = new List<Point>(40); // liste les coordonnées des cases disponibles
            Random positionAleatoire = new Random();
            int positionChoisie; // id du point de la liste sélectionné aléatoirement
            if (buttonRemplir.Text.Contains("rouges"))
                caseCourante.Y = 3;

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

            // génère une pièce pour chaque Point de la liste
            foreach(KeyValuePair<string, int> piece in jeu.ListePieces)
            {
                for (int repetitionPiece = 0; repetitionPiece < piece.Value; repetitionPiece++)
                {
                    positionChoisie = positionAleatoire.Next(listeCases.Count);

                    jeu.GenereUnePiece(piece.Key, listeCases[positionChoisie]);
                
                    listeCases.RemoveAt(positionChoisie);
                }
            }

            // si le bouton s'applique aux rouges
            if (buttonRemplir.Text.Contains("rouges"))
            {
                buttonRemplir.Enabled = false;
                DesactiveMenuContextuel();
            }
            else // sinon aux bleus
                buttonRemplir.Text = buttonRemplir.Text.Replace("bleus", "rouges");
            
            pictureBox1.Invalidate();
        }

        private void GenereMenu()
        {
            foreach(KeyValuePair<string, int> piece in jeu.ListePieces)
                pictureBox1.ContextMenu.MenuItems.Add(piece.Value + " - " + piece.Key, Menu_OnClick);
        }

        private void Menu_OnClick(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            MenuPictureBox(menuItem);
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