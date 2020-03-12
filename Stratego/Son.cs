using System.IO;
using WMPLib;

namespace Stratego
{
    public class Son
    {
	    protected readonly WindowsMediaPlayer player;
	    protected string extension;
	    protected string sonEnCours;

        public enum Extension
        {
	        MP3, WAV
        }

        public Son(string cheminFichier, Extension typeExtension = Extension.WAV) : this(cheminFichier, typeExtension, @"Ressources\Sons\")
        {
            
        }

        public Son(string cheminFichier, Extension typeExtension, string repertoire)
        {
	        AssocieExtension(typeExtension);

	        if (File.Exists(repertoire + cheminFichier + extension))
	        {
		        player = new WindowsMediaPlayer()
		        {
			        URL = repertoire + cheminFichier + extension
		        };
		        sonEnCours = cheminFichier;
	        }
		        
        }

        protected void AssocieExtension(Extension typeExtension)
        {
	        switch (typeExtension)
	        {
                case Extension.WAV:
	                extension = ".wav";
                    break;
                case Extension.MP3:
	                extension = ".mp3";
	                break;
                default:
	                extension = "";
                    break;
            }
        }

        public void Joue()
        {
            player.controls.play();
        }

        public void Stop()
        {
            player.controls.stop();
        }

        public string SonEnCours => sonEnCours;
    }
}