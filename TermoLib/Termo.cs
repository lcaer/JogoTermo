using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

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
        public int cont;
        public bool JogoFinalizado;

        public Termo() {
            CarregaPalavras("Palavras.txt");
            SorteiaPalavra();
            palavraAtual = 1;
            cont = 0;
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
            if (palavra == palavraSorteada || cont == 25)
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
                    if (palavraSorteada.Distinct().Count() > palavraSorteada.Length)//tem letras repitidas
                    {
                        var letraRepetida = palavraSorteada.GroupBy(c => c)
                           .Where(g => g.Count() > 1)
                           .FirstOrDefault();

                        
                    }

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
        public void ChecaPalavraV2(string palavra)
        {
            if (palavra == palavraSorteada)
            {
                JogoFinalizado = true;
                var palavraVencedora = new List<Letra>();
                for (int i = 0; i < palavra.Length; i++)
                {
                    palavraVencedora.Add(new Letra(palavra[i], 'V'));
                    teclado[palavra[i]] = 'V';
                }
                tabuleiro.Add(palavraVencedora);
                palavraAtual++;
                return;
            }

            if (cont == 30)
            {
                JogoFinalizado = true;
            }

            bool vp = validaPalavra(palavra);

            if (vp == true)
            {
                var cores = new char[palavra.Length];
                var palavraSorteadaTemp = new StringBuilder(palavraSorteada);

                for (int i = 0; i < palavra.Length; i++)
                {
                    if (palavra[i] == palavraSorteada[i])
                    {
                        cores[i] = 'V';
                        palavraSorteadaTemp[i] = '_';
                    }
                }

                for (int i = 0; i < palavra.Length; i++)
                {
                    if (cores[i] == 'V')
                    {
                        continue;
                    }

                    int indexNaSorteada = palavraSorteadaTemp.ToString().IndexOf(palavra[i]);

                    if (indexNaSorteada != -1)
                    {
                        cores[i] = 'A';
                        palavraSorteadaTemp[indexNaSorteada] = '_';
                    }
                    else
                    {
                        cores[i] = 'P';
                    }
                }
                var palavraTabuleiro = new List<Letra>();
                for (int i = 0; i < palavra.Length; i++)
                {
                    palavraTabuleiro.Add(new Letra(palavra[i], cores[i]));

                    char letra = palavra[i];
                    char corNova = cores[i];

                    if (!teclado.ContainsKey(letra) || corNova == 'V')
                    {
                        teclado[letra] = corNova;
                    }
                    else if (corNova == 'A' && teclado[letra] != 'V')
                    {
                        teclado[letra] = corNova;
                    }
                    else if (!teclado.ContainsKey(letra))
                    {
                        teclado[letra] = corNova;
                    }
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