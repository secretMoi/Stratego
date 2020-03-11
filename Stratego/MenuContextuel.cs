using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;
using Stratego.Personnages;
using Stratego.UserControls;

namespace Stratego
{
    [Serializable]
    public class MenuContextuel : ISerializable
    {
        private readonly ContextMenu contextMenu;
        private static PictureBox pictureBox;
        private JeuRegles jeu;
        private Point positionOrigine;
        
        private bool placementPieces; // si on place les pièces avant le début du jeu
        
        // serialise
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Jeu", jeu, typeof(JeuRegles));
            info.AddValue("PositionOrigine", positionOrigine, typeof(Point));
            info.AddValue("PlacementPieces", placementPieces, typeof(bool));
        }
        
        // deserialise
        public MenuContextuel(SerializationInfo info, StreamingContext context)
        {
            /*jeu = (JeuRegles) info.GetValue("Jeu", typeof(JeuRegles));
            positionOrigine = (Point) info.GetValue("PositionOrigine", typeof(Point));
            placementPieces = (bool) info.GetValue("PlacementPieces", typeof(bool));*/

            pictureBox.ContextMenu?.Dispose();
        }

        public MenuContextuel(PictureBox pictureBox)
        {
	        MenuContextuel.pictureBox = pictureBox;
	        MenuContextuel.pictureBox.ContextMenu = new ContextMenu();
            contextMenu = pictureBox.ContextMenu;
            
            PlacementPieces = true;
        }
        
        public void GenereMenu(JeuRegles jeu = null)
        {
            if(jeu != null)
                this.jeu = jeu;
            
            contextMenu.MenuItems.Clear();
            
            foreach(KeyValuePair<string, int> piece in this.jeu.ListePieces)
                contextMenu.MenuItems.Add(piece.Value + " - " + piece.Key, Menu_OnClick);
        }
        
        private void Menu_OnClick(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            
            // si on clic en dehors de la map ou dans un lac
            if(positionOrigine.X == -1)
            {
	            DialogBox.Show(@"Case invalide !");
                return;
            }
            
            string[] chaineItem = menuItem?.Text.Split('-'); // récupère la chaine de l'item sélectionné
            int nombrePieceRestante = Convert.ToInt32(chaineItem?[0].Trim()); // récupère le nombre de pièces pouvant encore être placées
            string nomPiece = chaineItem?[1].Trim(); // récupère le nom de la pièce

            // si on ne peut plus poser de pièces
            if (nombrePieceRestante < 1)
            {
	            DialogBox.Show(@"Pièce épuisée");
                return;
            }
            
            // si la case cible est déjà occupée
            Personnage caseCible = jeu.Map.GetPiece(positionOrigine);
            if (caseCible != null)
            {
	            DialogBox.Show(@"Case occupée !");
                return;
            }

            // crée la pièce
            if (jeu.GenereUnePiece(nomPiece, positionOrigine) == null)
                return;
            
            if(Personnage.GetNombrePieces() == 40)
                GenereMenu();

            if (menuItem != null)
                menuItem.Text = --nombrePieceRestante + @" - " + nomPiece; // actualise le texte de l'item

            // si toutes les pièces sont placées
            DesactiveMenuContextuel();

            pictureBox.Invalidate();
        }
        
        public void DesactiveMenuContextuel()
        {
            if (Personnage.GetNombrePieces() >= 80)
            {
                placementPieces = false;
                contextMenu.Dispose();
            }
        }

        public Dictionary<string, int> PiecesRestantes()
        {
            Dictionary<string, int> piecesRestantes = new Dictionary<string, int>();

            foreach (MenuItem item in contextMenu.MenuItems)
            {
                string[] chaineItem = item.Text.Split('-'); // récupère la chaine de l'item sélectionné
                int nombrePieceRestante = Convert.ToInt32(chaineItem[0].Trim()); // récupère le nombre de pièces pouvant encore être placées
                string nomPiece = chaineItem[1].Trim(); // récupère le nom de la pièce
                
                piecesRestantes.Add(nomPiece, nombrePieceRestante);
            }

            return piecesRestantes;
        }

        public Point PositionOrigine
        {
            set => positionOrigine = value;
        }

        public bool PlacementPieces
        {
            get => placementPieces;
            set => placementPieces = value;
        }
    }
}