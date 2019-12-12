using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WasteMe.Models;

namespace WasteMe.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(),
                    Name = "Snickers",
                    Barcode="5900951027307",
                    Image="Snickers75g.jpg",
                    WasteType="metal plastic",
                    WasteImage="Metal_plastic.png"
                },
                new Item { Id = Guid.NewGuid().ToString(),
                    Name = "Cane sugar",
                    Barcode="5904215135395",
                    Image="CukierTrzcinowyDemerara400g.jpg",
                    WasteType="paper",
                    WasteImage="Paper.png"
                },
                new Item { Id = Guid.NewGuid().ToString(),
                    Name = "Hirse",
                    Barcode="4038375025287",
                    Image="NatumiHirseNatural1000ml.jpg",
                    WasteType="metal plastic",
                    WasteImage="Metal_plastic.png"
                },
                new Item { Id = Guid.NewGuid().ToString(),
                    Name = "Chewing gum",
                    Barcode="4009900484695",
                    Image="OrbitSpearmintx42.png",
                    WasteType="metal plastic",
                    WasteImage="Metal_plastic.png"
                },
                new Item { Id = Guid.NewGuid().ToString(),
                    Name = "Coca-Cola",
                    Barcode="5449000000996",
                    Image="CocaCola330ml.jpg",
                    WasteType="metal plastic",
                    WasteImage="Metal_plastic.png"
                },
                new Item { Id = Guid.NewGuid().ToString(),
                    Name = "Tea",
                    Barcode="3585804000090",
                    Image="KusmiTeaPrinceVladimir125g.jpg",
                    WasteType="metal plastic",
                    WasteImage="Metal_plastic.png"
                },
                new Item { Id = Guid.NewGuid().ToString(),
                    Name = "Post-it",
                    Barcode="4046719100675",
                    Image="PostIt100RecycledPack.jpg",
                    WasteType="paper",
                    WasteImage="Paper.png"
                },
                new Item { Id = Guid.NewGuid().ToString(),
                    Name = "Delicje",
                    Barcode="7622300436438",
                    Image="DelicjeJagodowe147g.png",
                    WasteType="metal plastic",
                    WasteImage="Metal_plastic.png"
                },
                new Item { Id = Guid.NewGuid().ToString(),
                    Name = "Rekawice",
                    Barcode="5903936010769",
                    Image="AnnaZaradnaRekawiceGumowe.png",
                    WasteType="metal plastic",
                    WasteImage="Metal_plastic.png"
                },
                new Item { Id = Guid.NewGuid().ToString(),
                    Name = "Glade",
                    Barcode="5000204625271",
                    Image="GladeSandalwood300ml.jpg",
                    WasteType="metal plastic",
                    WasteImage="Metal_plastic.png"
                },
				new Item { Id = Guid.NewGuid().ToString(),
                    Name = "Beer",
                    Barcode="5903870005241",
                    Image="NamyslowPils500ml.png",
                    WasteType="glass",
                    WasteImage="Glass.png"
                },    
                new Item { Id = Guid.NewGuid().ToString(),
                    Name = "Green tea",
                    Barcode="5904215133933",
                    Image="HerbataZielonaAuchan100g.jpg",
                    WasteType="paper",
                    WasteImage="Paper.png"
                },    
                new Item { Id = Guid.NewGuid().ToString(),
                    Name = "Frosch",
                    Barcode="4009175148117",
                    Image="FroschCytrynowy500ml.png",
                    WasteType="metal plastic",
                    WasteImage="Metal_plastic.png"
                }                             };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }
        public async Task<Item> GetItemAsyncBarcode(string barcode)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Barcode == barcode));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
