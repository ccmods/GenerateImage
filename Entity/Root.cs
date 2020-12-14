using System;
using System.Collections.Generic;

namespace Entity {
    public class Item    {
        public string item { get; set; } 
    }

    public class Batchrsp    {
        public string ver { get; set; } 
        public List<Item> items { get; set; } 
        public List<int> itemorder { get; set; } 
        public DateTime refreshtime { get; set; } 
    }

    public class Root    {
        public Batchrsp batchrsp { get; set; } 
    }

}
