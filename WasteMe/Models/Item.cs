using System;

namespace WasteMe.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Image { get; set; }
        public string WasteType { get; set; } 
        public string WasteImage { get; set; }
    }
}