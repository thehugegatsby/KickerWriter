namespace KickerWriter.Data.DataObjects
{
    using System.Collections.Generic;

    public class HumanPlayer
    {
        public HumanPlayer()
        {
        }

        public string Name { get; set; }

        public List<int> DirekteDuellePointList { get; set; }
    }
}