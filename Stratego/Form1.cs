using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using Stratego.Personnages;

namespace Stratego
{
    public partial class Form1 : Form
    {
        private readonly Map map;
        private Personnage personnage;
        
        private Graphics tv;
        private Bitmap fond;
        private readonly List<Rectangle> positionPieces;
        private Rectangle aireJeu;
        private readonly List<Personnage> piecesJoueur;

        // Déplacement pièce
        private bool drag; // si on a activé le drag&drop
        private int idDragged; // élément sélectionné

        private Point positionOrigine;
        public Form1()
        {
            InitializeComponent();
            
            map = new Map();
            
            positionPieces = new List<Rectangle>();
            piecesJoueur = new List<Personnage>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fond = new Bitmap(map.AireJeu);
            aireJeu = new Rectangle(0,0, 612, 800);

            // liste toutes les classes existantes
            DirectoryInfo d = new DirectoryInfo(@"C:\Users\winmo\RiderProjects\Stratego\Stratego\Personnages");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.cs", SearchOption.TopDirectoryOnly); //Getting Text files

            List<string> fichiersClasses =new List<string>();
            foreach (FileInfo fichier in Files)
            {
                fichiersClasses.Add(fichier.ToString().Remove(Files[0].ToString().Length - 3));
            }

            // charge fichier xml des différentes pièces
            XmlTextReader listePieces = new XmlTextReader(@"C:\Users\winmo\RiderProjects\Stratego\Stratego\ListePieces.xml");
            int id = 0;
            Point position = new Point(0,9);
            while (listePieces.Read())
            {
                if (listePieces.NodeType == XmlNodeType.Element && listePieces.Name == "name")
                {
                    //todo apprendre réflection pour simplifier et rendre dynamique l'ajout de pièce
                    /*Assembly currentAssembly = Assembly.GetExecutingAssembly();
                    Type myType = currentAssembly.GetType(nomPiece);
                    MethodInfo TypePiece = myType.GetMethod("TypePiece");
                    
                    Personnage instance = Activator.CreateInstance(myType) as Personnage;
                    TypePiece.Invoke(instance, null);*/

                    string nomPiece = listePieces.ReadElementString();

                    /*if (!fichiersClasses.Contains(nomPiece))
                        MessageBox.Show("Pièce erronnée");*/

                    if (position.X == 10)
                    {
                        position.X = 0;
                        position.Y--;
                    }

                    switch (nomPiece)
                    {
                        case "Marechal":
                            piecesJoueur.Add(new Marechal(id, position)); // crée le personnage
                            break;
                        case "General":
                            piecesJoueur.Add(new General(id, position)); // crée le personnage
                            break;
                        case "Colonel":
                            piecesJoueur.Add(new Colonel(id, position)); // crée le personnage
                            break;
                        case "Major":
                            piecesJoueur.Add(new Major(id, position)); // crée le personnage
                            break;
                        case "Capitaine":
                            piecesJoueur.Add(new Capitaine(id, position)); // crée le personnage
                            break;
                        case "Lieutenant":
                            piecesJoueur.Add(new Lieutenant(id, position)); // crée le personnage
                            break;
                        case "Sergent":
                            piecesJoueur.Add(new Sergent(id, position)); // crée le personnage
                            break;
                        case "Demineur":
                            piecesJoueur.Add(new Demineur(id, position)); // crée le personnage
                            break;
                        case "Eclaireur":
                            piecesJoueur.Add(new Eclaireur(id, position, Map.casesX)); // crée le personnage
                            break;
                        case "Espion":
                            piecesJoueur.Add(new Espion(id, position)); // crée le personnage
                            break;
                        case "Drapeau":
                            piecesJoueur.Add(new Drapeau(id, position)); // crée le personnage
                            break;
                        case "Bombe":
                            piecesJoueur.Add(new Bombe(id, position)); // crée le personnage
                            break;
                    }
                    
                
                    positionPieces.Add(new Rectangle(map.CoordToPx(piecesJoueur[id].Position), piecesJoueur[id].Piece.Dimension)); // position de l'image
                    map.SetPositionPiece(piecesJoueur[id].Position, piecesJoueur[id]); // indique à la map ce qu'elle contient
                
                    id++;
                    position.X++;
                }
            }

            tv = CreateGraphics();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) // relâchement clic souris
        {
            //todo fixer le déplacement éclaireur
            if (drag)
            {
                Point position = map.TrouveCase(e.Location);

                if (position.X != -1) // si la position est valide
                {
                    // si le déplacement est valide pour la pièce
                    if (piecesJoueur[idDragged].Deplacement >= map.Distance(positionOrigine, map.PxToCoord(position)))
                    {
                        (int collision, int piece1, int piece2) = map.DeplacePiece(positionOrigine, map.PxToCoord(position));
                        
                        if (collision == Personnage.Vide) // si la case de destination est vide
                            RedessinePiece(idDragged, position, false);
                        else
                        {
                            EffacePiece(piece1);
                            EffacePiece(piece2);
                        }
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
            if (map.PositionValide(point) && piecesJoueur[id] != null) // si la position est dans la grille
            {
                if (centrePiece) // si on doit centrer l'image au centre du curseur
                {
                    point.X -= piecesJoueur[id].Piece.Longueur / 2;
                    point.Y -= piecesJoueur[id].Piece.Hauteur / 2;
                }
                
                pictureBox1.Invalidate(); // supprime l'image

                // calcule ses nouvelles coordonnées
                positionPieces[id].Point = point;
                    
                tv.DrawImage(piecesJoueur[id].Piece.Image, positionPieces[id].Rect); // affiche l'image avec ses nouvelles coordonnées
            }
        }

        private void EffacePiece(int id)
        {
            if (id >= 0)
            {
                positionPieces[id] = null;
                piecesJoueur[id] = null;
            
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(fond, aireJeu.Rect);

            for (int id = 0; id < piecesJoueur.Count; id++)
            {
                if(piecesJoueur[id] != null)
                    e.Graphics.DrawImage(piecesJoueur[id].Piece.Image, positionPieces[id].Rect);
            }
            
        }
    }
}