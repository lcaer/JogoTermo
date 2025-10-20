using TermoLib;

namespace TermoApp
{
    public partial class TermoApp : Form
    {
        public Termo termo;

        int coluna = 1;
        int show = 0;

        public TermoApp()
        {
            InitializeComponent();
            termo = new Termo();
            DisableButton();
            MessageBox.Show(termo.palavraSorteada);
            btn11.BackColor = Color.FromArgb(252, 215, 194);
            this.KeyPreview = true;
            this.AcceptButton = btnEnter;
            this.AcceptButton = btnRight;
            this.AcceptButton = btnLeft;
            this.ActiveControl = null;
        }

        public void DisableButton()
        {
            int col, linha;
            for (col = 1; col <= 5; col++)
            {
                for (linha = termo.palavraAtual + 1; linha <= 6; linha++)
                {
                    var nomeBotaoTab = $"btn{linha}{col}";
                    var botaoTab = RetornaBotao(nomeBotaoTab);
                    botaoTab.Enabled = false;
                }
            }
        }
        public void EnableButton()
        {
            int col;
            for (col = 1; col <= 5; col++)
            {
                var nomeBotaoTab = $"btn{termo.palavraAtual}{col}";
                var botaoTab = RetornaBotao(nomeBotaoTab);
                botaoTab.Enabled = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnEnter.PerformClick();
                return true;
            }
            else if (keyData == Keys.Right)
            {
                btnRight.PerformClick();
                return true;
            }
            else if (keyData == Keys.Left)
            {
                btnLeft.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnTeclado_Click(object sender, EventArgs e)
        {
            if (coluna > 5)
                return;

            var button = (Button)sender;
            var linha = termo.palavraAtual;
            var nomeButton = $"btn{linha}{coluna}";

            var buttonTabuleiro = RetornaBotao(nomeButton);
            buttonTabuleiro.Text = button.Text;

            coluna++;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            var palavra = string.Empty;

            for (int i = 1; i <= 5; i++)
            {
                var nomeBotao = $"btn{termo.palavraAtual}{i}";
                var botao = RetornaBotao(nomeBotao);
                palavra += botao.Text;
            }
            if (palavra.Length != 5)
            {
                MessageBox.Show("A palavra precisa ter 5 letras", "Jogo termo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            termo.ChecaPalavra(palavra);
            if (termo.validaPalavra(palavra) == false)
            {
                MessageBox.Show("Palavra Inválida!", "Jogo termo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            AtualizaTabuleiro();
            coluna = 1;
            EnableButton();
            if (termo.JogoFinalizado)
            {
                MessageBox.Show("Parabéns, palavra correta!", "Jogo termo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private Button RetornaBotao(string name)
        {
            return (Button)Controls.Find(name, true)[0];
        }

        private Button baseAtualizar(int n, int op, int col)
        {
            var nomeBotaoTab = $"btn{termo.palavraAtual - 1}{col}";
            var botaoTab = RetornaBotao(nomeBotaoTab);
            var nomeBotaoKey = "";
            if (n == 1)
            {
                var letra = termo.tabuleiro[termo.palavraAtual - 2][col - 1];
                nomeBotaoKey = $"btn{letra.Caracter}";
            }
            if (n == 2)
            {
                var letra2 = botaoTab.Text;
                nomeBotaoKey = $"btn{letra2}";
            }

            var botaoKey = RetornaBotao(nomeBotaoKey);

            if (op == 1)
            {
                return botaoTab;

            }
            else if (op == 2)
            {
                return botaoKey;
            }

            return null;
        }

        private void AtualizaTabuleiro()
        {
            for (int col = 1; col <= 5; col++)
            {
                var letra = termo.tabuleiro[termo.palavraAtual - 2][col - 1];
                var botaoTab = baseAtualizar(1, 1, col);
                var botaoKey = baseAtualizar(1, 2, col);
                //MessageBox.Show("Letra = " + letra.Caracter + "\nCor = " + letra.Cor);
                if (letra.Cor == 'A')
                {

                    botaoTab.BackColor = Color.FromArgb(255, 252, 218);
                    botaoKey.BackColor = Color.FromArgb(255, 252, 218);
                }
                else if (letra.Cor == 'V')
                {
                    botaoTab.BackColor = Color.FromArgb(216, 239, 216);
                    botaoKey.BackColor = Color.FromArgb(216,239, 216);
                }
                else if (letra.Cor == 'P')
                {
                    botaoTab.BackColor = Color.LightGray;
                    botaoKey.BackColor = Color.LightGray;
                }
                //MessageBox.Show("Tab = " + botaoTab.BackColor + "\nKey = " + botaoKey.BackColor);
            }
        }

        private void gbPaintBorderEraser(object sender, PaintEventArgs e)
        {
            GroupBox groupBox = (GroupBox)sender;
            using (SolidBrush brush = new SolidBrush(groupBox.BackColor))
            {
                e.Graphics.FillRectangle(brush, 0, 0, groupBox.Width, groupBox.Height);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (coluna == 1)
                return;
            var col = coluna - 1;

            var button = (Button)sender;
            var linha = termo.palavraAtual;
            var nomeButton = $"btn{linha}{col}";

            var buttonTabuleiro = RetornaBotao(nomeButton);
            buttonTabuleiro.Text = "";

            coluna--;
            DestaqueBotao("bwd");
            if (coluna == 5)
                buttonTabuleiro.BackColor = Color.FromArgb(252, 215, 194); ;
        }

        private void TermoApp_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            {
                if (coluna > 5)
                    return;

                var linha = termo.palavraAtual;
                var nomeButton = $"btn{linha}{coluna}";

                var buttonTabuleiro = RetornaBotao(nomeButton);
                buttonTabuleiro.Text = e.KeyCode.ToString();

                coluna++;
                if (coluna <= 5)
                    DestaqueBotao("fwd");
                else if (coluna > 5)
                    buttonTabuleiro.BackColor = Color.MistyRose;

            }
            else if (e.KeyCode == Keys.Back)
            {
                btnDelete.PerformClick();
            }
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            int col, lin;
            for (lin = termo.palavraAtual; termo.palavraAtual != 1; termo.palavraAtual--)
            {
                for (col = 1; col <= 5; col++)
                {
                    var botaoTab = baseAtualizar(2, 1, col);
                    var botaoKey = baseAtualizar(2, 2, col);

                    botaoTab.Text = "";
                    botaoTab.BackColor = Color.MistyRose;
                    botaoKey.BackColor = Color.MistyRose;
                    termo.SorteiaPalavra();
                    termo.JogoFinalizado = false;
                    //if ()
                    //{

                    //}

                }
            }
            coluna = 1;
        }

        private void btnPlacar_Click(object sender, EventArgs e)
        {
            PlacarApp placar = new PlacarApp();
            if (show == 0)
            {
                placar.Show();
                show = 1;
            }
        }

        private void DestaqueBotao(string direcao)
        {
            // Implementar destaque visual para o botão selecionado
            String botaoNome;
            Button botao;
            int col;

            if (coluna > 5)
                return;

            botaoNome = $"btn{termo.palavraAtual}{coluna}";
            botao = RetornaBotao(botaoNome);
            botao.BackColor = Color.FromArgb(252, 215, 194);

            if (direcao == "fwd")
            {
                col = coluna - 1;
            }
            else if (direcao == "bwd" & coluna < 5)
            {
                col = coluna + 1;
            }
            else
            {
                col = coluna;
            }

            botaoNome = $"btn{termo.palavraAtual}{col}";
            botao = RetornaBotao(botaoNome);
            botao.BackColor = Color.MistyRose;
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (coluna < 6)
                coluna++;
            DestaqueBotao("fwd");
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (coluna > 1)
                coluna--;
            DestaqueBotao("bwd");
        }

        private void btnTabuleiro_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            for(int col = 1; col <=5; col++)
            {
                var nomeBotao = $"btn{termo.palavraAtual}{col}";
                if (nomeBotao == button.Name)
                {
                    button.BackColor = Color.FromArgb(252, 215, 194);
                    coluna =col;
                }
            }
        }
    }
}
