using System.Windows.Forms;
using TermoLib;

namespace TermoApp
{
    public partial class PlacarApp : Form
    {
        public Placar placar;
        public PlacarApp()
        {
            InitializeComponent();
            placar = new Placar();
            btnGames.Text = placar.games.ToString();
            MessageBox.Show("games = " + placar.games.ToString());
            MessageBox.Show("btn games = " + btnGames.Text);
        }
       
    }
}
