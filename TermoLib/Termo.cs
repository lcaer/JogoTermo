namespace TermoLib
{
    public class Letra 
    {
        public Letra (char caracter, char cor)
        {
            Caracter = caracter;
            Cor = cor;
        }

        public char Caracter;
        public char Cor;
    }

    public class Termo
    {
        public List<String> palavras;
        public string palavraSorteada;
        public List<List<Letra>> tabuleiro;
        public Dictionary<char, char> teclado;
        public int palavraAtual;
        public bool JogoFinalizado;

        public Termo() {
            CarregaPalavras("Palavras.txt");
            SorteiaPalavra();
            palavraAtual = 1;
            JogoFinalizado = false;
            tabuleiro = new List<List<Letra>>();
            teclado = new Dictionary<char, char>(); 
            for(int i = 65; i <= 90; i++)//65 - 90 Tabela ASCII (letras maíusculas)
            {
                /*
                 * [C] cinza - não digitado
                 * [V] verde - digitado (letra certa no lugar certo)
                 * [A] amarelo - digitado (letra certa no lugar errado)
                 * [P] preto - digitado (letra errada no lugar errado)
                 */
                teclado.Add((char)i, 'C');
            }
        }


        public void CarregaPalavras(string fileName)
        {
            palavras = File.ReadAllLines(fileName).ToList();
        }

        public void SorteiaPalavra()
        {
            Random rd = new Random();
            var index = rd.Next(0,palavras.Count()-1);
            palavraSorteada = palavras[index];
        }

        public void ChecaPalavra(string palavra)
        {
            if (palavra == palavraSorteada)
            {
                JogoFinalizado = true;
            }

            bool vp = validaPalavra(palavra);

            if(vp == true)
            {
                var palavraTabuleiro = new List<Letra>();
                char cor;
                for (int i = 0; i < palavra.Length; i++)
                {
                    if (palavra[i] == palavraSorteada[i])
                    {
                        cor = 'V';
                    }
                    else if (palavraSorteada.Contains(palavra[i]))
                    {
                        cor = 'A';
                    }
                    else
                    {
                        cor = 'P';
                    }
                    palavraTabuleiro.Add(new Letra(palavra[i], cor));
                    teclado[palavra[i]] = cor;
                }
                tabuleiro.Add(palavraTabuleiro);
                palavraAtual++;
            }
        }

        public bool validaPalavra(string palavra)
        {
            bool vp = false;
            foreach (string p in palavras)
            {
                if (p == palavra)
                {
                    vp = true;
                }
            }
            return vp;
        }
        
    }
}