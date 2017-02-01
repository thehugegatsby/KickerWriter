namespace KickerWriter.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class StringTupleLists
    {
        public StringTupleLists()
        {
            this.List = new List<Tuple<string, string>>();
        }

        public List<Tuple<string, string>> List { get; set; }

        public void AddList(StringTupleLists newList)
        {
            this.List.AddRange(newList.List);
        }

        public void AddStringPair(string string1, string string2)
        {
            this.List.Add(Tuple.Create(string1, string2));
        }
    }
}