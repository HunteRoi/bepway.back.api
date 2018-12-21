using System.Collections.Generic;

namespace Model
{
    public class Dataset
    {
        public int nhits { get; set; }
        public Parameter parameter { get ; set; }
        public List<Record> records { get; set ;}
        public object face_groups { get; set; }

    }
}