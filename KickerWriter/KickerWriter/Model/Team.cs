namespace KickerWriter.Model
{
    public class Team
    {
        public Team(string name)
        {
            this.Name = name;
        }

        public string City { get; set; }
        public string EmblemPath { get; set; }
        public string Name { get; set; }
    }
}
