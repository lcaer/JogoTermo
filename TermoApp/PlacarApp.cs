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
        }
       
    }
}
