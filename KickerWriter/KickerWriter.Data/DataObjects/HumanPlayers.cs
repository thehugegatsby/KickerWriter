namespace KickerWriter.Data.DataObjects
{
    using System.Collections.Generic;

    public class HumanPlayers
    {
        public HumanPlayers()
        {
            this.Players = new List<HumanPlayer>()
            {
                new HumanPlayer() { Name = "David" },
                new HumanPlayer() { Name = "Basti" },
                new HumanPlayer() { Name = "Schmaiby" },
                new HumanPlayer() { Name = "Jens" },
            };
        }

        public List<HumanPlayer> Players { get; set; }
    }
}