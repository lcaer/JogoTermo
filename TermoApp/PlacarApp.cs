using System.Windows.Forms;
using TermoLib;

namespace TermoApp
{
    public partial class PlacarApp : Form
    {
        public Placar placar;
        public Termo termo;
        public PlacarApp()
        {
            InitializeComponent();
            placar = new Placar();
            termo = new Termo();
            //MessageBox.Show("games = " + placar.games.ToString());
            //MessageBox.Show("btn games = " + btnGames.Text);
        }

        public void AtualizaPlacar()
        {
            MessageBox.Show("games = " + placar.games.ToString());
            btnGames.Text = placar.games.ToString();
            btnVictories.Text = placar.victories.ToString();
            btnStreak.Text = placar.streak.ToString();
            btnP1.Text = placar.places[0].ToString();
            btnP2.Text = placar.places[1].ToString();
            btnP3.Text = placar.places[2].ToString();
            btnP4.Text = placar.places[3].ToString();
            btnP5.Text = placar.places[4].ToString();
            btnP6.Text = placar.places[5].ToString();
            btnPL.Text = placar.places[6].ToString();
        }

        public void Vitoria()
        {
            //termo.JogoFinalizado =true;
            if (termo.JogoFinalizado)
            {
                placar.games++;
                placar.victories++;
                placar.streak++;
            }
            AtualizaPlacar();
        }
    }
}
