using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Serveur
{
    class Program
    {
        private static Thread Ecoute;
        private static bool serveurActif = false;
        
        static void Main(string[] args)
        {
            Ecoute = new Thread(Ecouter);
            Ecoute.Start();
        }
        
        private static void Ecouter()
        {
            UdpClient serveur = new UdpClient(5035);

            while (true)
            {
                IPEndPoint client = null;

                byte[] data = serveur.Receive(ref client);
                string message = Encoding.Default.GetString(data);

                Console.WriteLine(message);
            }
        }
    }
}