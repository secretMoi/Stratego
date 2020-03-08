namespace Stratego
{
    public abstract class Reseau
    {
        protected bool clientOuServeur;

        public bool Client = false;
        public bool Serveur = true;

        protected int port;

        public Reseau()
        {
            clientOuServeur = Client;

            port = 5035;
        }

        public void SetRole(bool role)
        {
            clientOuServeur = !clientOuServeur;
        }

        public abstract void Ferme();
    }
}