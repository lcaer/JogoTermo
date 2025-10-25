using System.Reflection.Metadata.Ecma335;

namespace TermoLib
{
    public class Placar
    {
        public Termo termo;
        public int games; //contagem total de jogos
        public int victories; // contagem total de vitórias
        public int loses; // contagem total de derrotas
        public int streak; //contagem de vitórias seguidas
        public int[] places; //vitórias (ou derrotas) em cada uma das linhas
        public int[] prevplaces = {0, 0, 0, 0, 0, 0, 0}; //numero anterior de vitórias (ou derrotas) em cada uma das linhas

        public Placar()
        {
            termo = new Termo();
            games = 0; //contagem total de jogos
            victories = 0; // contagem total de vitórias
            loses = 0; // contagem total de derrotas
            streak = 0; //contagem de vitórias seguidas
            places = new int[7]; //vitórias (ou derrotas) em cada uma das linhas
            prevplaces = new int[7];
            contagemJogos();
        }

        public void contagemJogos()
        {
            if (termo.JogoFinalizado)
            {
                games++;
            }
        }

    }
}
