using System.IO;

namespace Carrosse
{
    public class Son
    {
        private System.Media.SoundPlayer player;
        private string repertoireSons = @"Ressources\Sons\";
        private string extension;
        public Son(string cheminFichier)
        {
            extension = ".wav";
            player = new System.Media.SoundPlayer(repertoireSons + cheminFichier + extension);
        }

        public void Joue()
        {
            player.Play();
        }
    }
}