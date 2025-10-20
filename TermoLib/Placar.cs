namespace TermoLib
{
    public class Placar
    {
        public Termo termo;
        public int games = 0; //contagem total de jogos
        public int victories = 0; // contagem total de vitórias
        public int loses = 0; // contagem total de derrotas
        public int streak = 0; //contagem de vitórias seguidas
                           //public int maxStreak; // maior contagem de vitórias seguidas
        public int[] places =  new int[7]; //vitórias (ou derrotas) em cada uma das linhas

        public Placar()
        {
            termo = new Termo();
            games = contagemJogos(games);
        }

        public int contagemJogos(int jogos)
        {
            return jogos++;
        }

    }
}
