using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Stratego.Personnages;

namespace Stratego
{
    public class JeuRegles
    {
        private readonly List<string> listeClasses;
        private XmlTextReader listePieces;
        public JeuRegles()
        {
            listeClasses = new List<string>();
        }

        // Récupère la liste des classes contenues dans le programmes
        public void ListeClasse()
        {
            // liste toutes les classes existantes
            DirectoryInfo d = new DirectoryInfo(@"C:\Users\winmo\RiderProjects\Stratego\Stratego\Personnages");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.cs", SearchOption.TopDirectoryOnly); //Getting Text files
            
            foreach (FileInfo fichier in Files)
            {
                listeClasses.Add(fichier.ToString().Remove(fichier.ToString().Length - 3));
            }
        }

        // Vérifie qu'une classe est contenue dans la liste de classes
        public bool ClasseExiste(string classe)
        {
            return listeClasses.Contains(classe);
        }

        // Ouvre le fichier XML listant les différentes pièces et leur nombre
        public void OuvreXMLClasses(string chemin)
        {
            listePieces = new XmlTextReader(chemin);
        }

        public void GenerePieces(Map map, List<Rectangle> positionPieces)
        {
            string nomPiece = null;
            int nombrePieces; // nombre de fois qu'une pièce peut être placée
            
            int id = 0;
            Point position = new Point(0, Map.casesY - 1); // position de la pièce à placer
            
            while (listePieces.Read()) // parcours le fichier XML
            {
                nombrePieces = 0; // remet à 0 le nombre de pièces à chaque tour de boucle
                
                if (listePieces.NodeType == XmlNodeType.Element && listePieces.Name == "name") // récupère le nom
                    nomPiece = listePieces.ReadElementString();
                if (listePieces.NodeType == XmlNodeType.Element && listePieces.Name == "nombre") // récupère le nb de pièces
                    nombrePieces = Convert.ToInt32(listePieces.ReadElementString());

                //todo apprendre réflection pour simplifier et rendre dynamique l'ajout de pièce
                /*Assembly currentAssembly = Assembly.GetExecutingAssembly();
                Type myType = currentAssembly.GetType(nomPiece);
                MethodInfo TypePiece = myType.GetMethod("TypePiece");
                
                Personnage instance = Activator.CreateInstance(myType) as Personnage;
                TypePiece.Invoke(instance, null);*/

                for (int i = 0; i < nombrePieces; i++) // génère les nb de pièces indiquées par le fichier XML
                {
                    if (ClasseExiste(nomPiece))
                        MessageBox.Show("Pièce erronnée : " + nomPiece);

                    if (position.X == 10) // passe à la ligne suivante lorsqu'on arrive à la dernière colonne X
                    {
                        position.X = 0;
                        position.Y--;
                    }

                    Personnage personnage = null;
                    
                    switch (nomPiece)
                    {
                        case "Marechal":
                            personnage = new Marechal(id, position); // crée le personnage
                            break;
                        case "General":
                            personnage = new General(id, position); // crée le personnage
                            break;
                        case "Colonel":
                            personnage = new Colonel(id, position); // crée le personnage
                            break;
                        case "Major":
                            personnage = new Major(id, position); // crée le personnage
                            break;
                        case "Capitaine":
                            personnage = new Capitaine(id, position); // crée le personnage
                            break;
                        case "Lieutenant":
                            personnage = new Lieutenant(id, position); // crée le personnage
                            break;
                        case "Sergent":
                            personnage = new Sergent(id, position); // crée le personnage
                            break;
                        case "Demineur":
                            personnage = new Demineur(id, position); // crée le personnage
                            break;
                        case "Eclaireur":
                            personnage = new Eclaireur(id, position, Map.casesX); // crée le personnage
                            break;
                        case "Espion":
                            personnage = new Espion(id, position); // crée le personnage
                            break;
                        case "Drapeau":
                            personnage = new Drapeau(id, position); // crée le personnage
                            break;
                        case "Bombe":
                            personnage = new Bombe(id, position); // crée le personnage
                            break;
                    }
                    
                    positionPieces.Add(new Rectangle(map.CoordToPx(personnage.Position), personnage.Piece.Dimension)); // position de l'image
                    map.SetPositionPiece(personnage.Position, personnage); // indique à la map ce qu'elle contient
            
                    id++;
                    position.X++;
                }
            }
        }
    }
}