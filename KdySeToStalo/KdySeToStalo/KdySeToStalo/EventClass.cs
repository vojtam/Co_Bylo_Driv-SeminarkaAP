using System;
using System.Collections.Generic;
using System.Text;

namespace KdySeToStalo
{
    public class Události
    {
        public string Název;
        public int Datum;
        public int Id;
        public string Wiki;
        public Události(string název, int datum, int id, string wiki)
        {
            Název = název;
            Datum = datum;
            Id = id;
            Wiki = wiki;

        }
    }
}
