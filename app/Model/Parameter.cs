using System.Collections.Generic;

namespace Model
{
    public class Parameter
    {
        public List<string> dataset {get; set;}
        public List<string> refine {get; set;}
        public string timezone {get; set;}
        public int rows {get; set;}
        public string format {get; set;}
    }
}