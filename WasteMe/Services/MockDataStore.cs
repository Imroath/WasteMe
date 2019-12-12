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
                    Image="?.jpg",
                    WasteType="plastic",
                    WasteImage="plastic.png"
                },
                                new Item { Id = Guid.NewGuid().ToString(),
                    Name = "Milk",
                    Barcode="5904215131458",
                    Image="?.jpg",
                    WasteType="paper",
                    WasteImage="paper.png"
                },
                                new Item { Id = Guid.NewGuid().ToString(),
                    Name = "Cane sugar",
                    Barcode="5904215135395",
                    Image="?.jpg",
                    WasteType="paper",
                    WasteImage="paper.png"
                },
                                new Item { Id = Guid.NewGuid().ToString(),
                    Name = "Permanent marker",
                    Barcode="4038375025287",
                    Image="?.jpg",
                    WasteType="plastic",
                    WasteImage="plastic.png"
                },
                                new Item { Id = Guid.NewGuid().ToString(),
                    Name = "Chewing gum",
                    Barcode="4009900484695",
                    Image="?.jpg",
                    WasteType="plastic",
                    WasteImage="plastic.png"
                },
                                new Item { Id = Guid.NewGuid().ToString(),
                    Name = "Coca-Cola",
                    Barcode="5449000000996",
                    Image="cola.jpg",
                    WasteType="plastic",
                    WasteImage="plastic.png"
                },
                                new Item { Id = Guid.NewGuid().ToString(),
                    Name = "Tea",
                    Barcode="3585804000090",
                    Image="?.jpg",
                    WasteType="metal",
                    WasteImage="metal.png"
                },
                                new Item { Id = Guid.NewGuid().ToString(),
                    Name = "Post-it",
                    Barcode="4046719100675",
                    Image="?.jpg",
                    WasteType="paper",
                    WasteImage="paper.png"
                },
                                                new Item { Id = Guid.NewGuid().ToString(),
                    Name = "GP Batteries",
                    Barcode="4891199000034",
                    Image="?.jpg",
                    WasteType="paper",
                    WasteImage="paper.png"
                }
            };
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
