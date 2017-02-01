namespace KickerWriter.Model
{
    public class Season
    {
        public Season()
        {
        }

        public Season(int number)
        {
            this.Number = number;
        }
        public Vorbericht Vorbericht { get; set; }

        public int Number { get; set; }

        public int Year { get; set; }
    }
}