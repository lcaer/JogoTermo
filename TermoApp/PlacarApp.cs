using System.Windows.Forms;
using TermoLib;

namespace TermoApp
{
    public partial class PlacarApp : Form
    {
        public Termo termo;
        public PlacarApp()
        {
            InitializeComponent();
            termo = new Termo();
            AtualizarPlacar();
        }

        public void AtualizarPlacar()
        {
            termo.contaJogos();
            MessageBox.Show("Gm: " + termo.games);
        }
    }
}
