using System.Collections.Generic;

namespace Model
{
    public class Parameter
    {
        public List<string> Dataset {get; set;}
        public List<string> Refine {get; set;}
        public string Timezone {get; set;}
        public int Rows {get; set;}
    }
}