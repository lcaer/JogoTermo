using System.Windows.Forms;
using TermoLib;

namespace TermoApp
{
    public partial class PlacarApp : Form
    {
        public Placar placar;
        public Termo termo;
        int maisVitorias = 0;
        public PlacarApp()
        {
            InitializeComponent();
            placar = new Placar();
            termo = new Termo();
            
            
            //MessageBox.Show("games = " + placar.games.ToString());
            //MessageBox.Show("btn games = " + btnGames.Text);
        }
        private Button RetornaBotao(string name)
        {
            return (Button)Controls.Find(name, true)[0];
        }

        public void AtualizaPlacar(int pos)
        {
            String buttonPlacarname;
            Button buttonPlacar;
            MessageBox.Show("games = " + placar.games.ToString());
            btnGames.Text = placar.games.ToString();
            btnVictories.Text = placar.victories.ToString();
            btnStreak.Text = placar.streak.ToString();

            if(placar.places[pos] >= maisVitorias)
            {
                maisVitorias = placar.places[pos];
                buttonPlacarname = $"btnP{pos + 1}";
                buttonPlacar = RetornaBotao(buttonPlacarname);
                buttonPlacar.Width = 290;
                buttonPlacar.ImageKey = "(none)";
                buttonPlacar.Text = placar.places[pos].ToString();
                for(int i = 0; i < 7; i++)
                {
                    if (i != pos & placar.places[i] != 0)
                    {
                        buttonPlacarname = $"btnP{i+1}";
                        buttonPlacar = RetornaBotao(buttonPlacarname);
                        buttonPlacar.Width = ((290 / maisVitorias) * placar.places[i]);
                    }
                }
            }
            else if (placar.places[pos] < maisVitorias)
            {
                buttonPlacarname = $"btnP{pos + 1}";
                buttonPlacar = RetornaBotao(buttonPlacarname);
                buttonPlacar.Width = ((290 / maisVitorias) * placar.places[pos]);
                buttonPlacar.ImageKey = "(none)";
                buttonPlacar.Text = placar.places[pos].ToString();
            }
        }
        public void Loss()
        {
            placar.places[6]++;
            placar.games++;
            placar.streak = 0;
            AtualizaPlacar(6);
        }

        public void Victory(int pos)
        {
            placar.places[pos-1]++;
            placar.games++;
            placar.victories++;
            placar.streak++;
            AtualizaPlacar(pos-1);
        }
    }
}
