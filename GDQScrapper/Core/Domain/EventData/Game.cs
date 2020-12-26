namespace GDQScrapper.Core.Domain.EventData
{
    public class Game
    {
        private string game;

        public Game(string game)
        {
            this.game = game;
        }

        public override string ToString()
        {
            return game;
        }
    }
}
