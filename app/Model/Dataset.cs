using System.Collections.Generic;

namespace Model
{
    public class Dataset
    {
        public int Nhits { get; set; }
        public Parameter Parameter { get ; set; }
        public List<Record> Records { get; set ;}

    }
}