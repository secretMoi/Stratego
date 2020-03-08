using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

//todo cases vertes et rouges
//todo zone tuto premièe prise en main
//todo fenetre menu (son, Anti-alias, emplacement sauvegarde, activé/désactivé historique combat...)
namespace Stratego.Fenetres
{
    public partial class Form1 : Form
    {
        private readonly Rectangle aireJeu;
        private JeuRegles jeu;
        private MenuContextuel menuContextuel;

        private Point positionOrigine; // position de départ de la pièce déplacée
        public Form1()
        {
            InitializeComponent();
            
            jeu = new JeuRegles("ListePieces.xml");
            
            aireJeu = new Rectangle(0,0, 612, 800);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            menuContextuel = new MenuContextuel(pictureBox1);
            menuContextuel.GenereMenu(jeu);
        }
        
        private void evenement_Click(object sender, EventArgs e)
        {
            string nom = ((ToolStripMenuItem) sender).Name; // récupère le nom du controle appelant
            string[] chaine = nom.Split('_'); // scinde le nom pour avoir les 2 parties
            
            string @namespace = GetType().Namespace;
            string @class = chaine[1];

            // équivalent var typeClasse = Type.GetType(String.Format("{0}.{1}", @namespace, @class));
            var typeClasse = Type.GetType($"{@namespace}.{@class}"); // trouve la classe
            if (typeClasse == null) return; // quitte si la classe est introuvable
            Form fenetre = Activator.CreateInstance(typeClasse) as Form; // instancie un objet

            fenetre?.Show(); // Affiche la fenêtre
        }
        
        private void Partie(object sender, EventArgs e)
        {
            string nom = ((ToolStripMenuItem) sender).Name; // récupère le nom du controle appelant
            string resultat;
            FileStream fichierSauvegarde;
            try
            {
                // Use a BinaryFormatter or SoapFormatter.
                IFormatter formatter = new BinaryFormatter();

                if (nom.Contains("Reprendre"))
                {
                    fichierSauvegarde = new FileStream(@"save.xml", FileMode.Open);
                    JeuRegles ancienJeu = (JeuRegles)formatter.Deserialize(fichierSauvegarde);

                    jeu = ancienJeu;
                    
                    resultat = @"Partie restaurée";
                    
                    pictureBox1.Invalidate();
                }
                else if(nom.Contains("Sauvegarder"))
                {
                    fichierSauvegarde = new FileStream(@"save.xml", FileMode.Create);
                    formatter.Serialize(fichierSauvegarde, jeu);
                    
                    resultat = @"Partie sauvegardée";
                }
                else
                    return;

                fichierSauvegarde.Close();

                MessageBox.Show(resultat);
            }
            catch (ApplicationException caught)
            {
                MessageBox.Show(caught.Source);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) // relâchement clic souris
        {
            if (menuContextuel.PlacementPieces) return; // si la pièce n'est pas sélectionnée ce n'est pas la peine de continuer
            
            jeu.LachePiece(e.Location, positionOrigine, richTextBox1);

            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(menuContextuel.PlacementPieces) return;
            
            jeu.BougePiece(e.Location);
            
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) // enfoncement clic souris
        {
            jeu.PrisePiece(ref positionOrigine, e.Location, menuContextuel.PlacementPieces);
            menuContextuel.PositionOrigine = positionOrigine;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(jeu.Map.Fond, aireJeu.Rect); // repeint la grille

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
                if (jeu.Map.GetPiece(caseCourante) == null)
                    listeCases.Add(caseCourante);

                caseCourante.X++;

                if (caseCourante.X == 10)
                {
                    caseCourante.X = 0;
                    caseCourante.Y--;
                }
            }
            
            // génère une pièce pour chaque Point de la liste
            foreach(KeyValuePair<string, int> piece in menuContextuel.PiecesRestantes())
            {
                for (int repetitionPiece = 0; repetitionPiece < piece.Value; repetitionPiece++)
                {
                    positionChoisie = positionAleatoire.Next(listeCases.Count);

                    jeu.GenereUnePiece(piece.Key, listeCases[positionChoisie]);
                
                    listeCases.RemoveAt(positionChoisie);
                }
            }
            
            menuContextuel.GenereMenu();

            // si le bouton s'applique aux rouges
            if (buttonRemplir.Text.Contains("rouges"))
            {
                buttonRemplir.Enabled = false;
                menuContextuel.DesactiveMenuContextuel();
            }
            else // sinon aux bleus
                buttonRemplir.Text = buttonRemplir.Text.Replace("bleus", "rouges");
            
            pictureBox1.Invalidate();
        }

        private void Quitter_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // todo réactiver
            /*e.Cancel = MessageBox.Show(this,
                @"Souhaitez-vous quitter ?" + Environment.NewLine + @"Toute partie non sauvegardée sera perdue...",
                @"Quitter",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            ) != DialogResult.Yes;*/
        }
    }
}