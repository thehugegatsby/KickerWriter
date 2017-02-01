namespace KickerWriter.Data
{
    using KickerWriter.Data.DataObjects;
    using System;
    using System.Collections.Generic;

    public class GlobalData
    {
        public GlobalData()
        {
            this.HumanPlayers = new HumanPlayers();
        }

        public static List<Tuple<string, string>> TopicStringReplaceList = new List<Tuple<string, string>>()
        {
            Tuple.Create("Saisonziel Vorstand", "Ziel_Vorstand"),
            Tuple.Create("Saisonziel Manager", "Ziel_Manager")
        };

        public static List<string> SeasonFileNameList = new List<string>()
        {
            "Direkte_Duelle",
            "Saisonverlauf",
            "Statistik_International",
            "Sonstiges",
            "Transfers",
            "Vorbericht"
        };

        public HumanPlayers HumanPlayers { get; set; }
    }
}