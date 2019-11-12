using System;
using System.Collections.Generic;
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

        private JeuRegles jeu;
        private Reseau reseau;

        // Déplacement pièce
        private bool drag; // si on a activé le drag&drop
        private int idDragged; // élément sélectionné

        private Point positionOrigine;
        public Form1()
        {
            InitializeComponent();

            reseau = new Client();
            map = new Map();
            
            positionPieces = new List<Rectangle>();
            
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
            if(reseau is Client)
                (reseau as Client).Emettre("coucou");
            if (!drag) return; // si la pièce n'est pas sélectionnée ce n'est pas la peine de continuer
            
            Point position = map.TrouveCase(e.Location);
            if (position.X == -1) return;
            
            Personnage attaquant = map.TrouvePersoParId(idDragged);
            Personnage defenseur = map.GetPiece(map.PxToCoord(position));
                
            // si le déplacement est valide pour la pièce
            if(map.ConditionsDeplacement(idDragged, positionOrigine, map.PxToCoord(position))){
                (int collision, int piece1, int piece2) = map.DeplacePiece(positionOrigine, map.PxToCoord(position));

                jeu.GenereHistoriqueDialogue(richTextBox1, attaquant, defenseur, collision);
                    
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
            Personnage personnage = map.TrouvePersoParId(id);

            if (!map.PositionValide(point) || personnage == null) return;
            
            if (centrePiece) // si on doit centrer l'image au centre du curseur
                personnage.Piece.CentrePiece(ref point);
                
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
                personnage = map.TrouvePersoParId(id);
                
                if(personnage != null) // ne dessine que les pièces valides
                    e.Graphics.DrawImage(personnage.Piece.Image, positionPieces[id].Rect);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reseau.Ferme();
            reseau = new Client();
            
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reseau.Ferme();
            reseau = new Serveur();
            
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            reseau.Ferme(); // lance la déconnexion du réseau à la fermeture de la fenêtre
        }
    }
}