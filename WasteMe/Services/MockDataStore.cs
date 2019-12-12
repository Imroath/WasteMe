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
                    Name = "Permanent Market", 
                    Barcode="4004675006400",
                    Image="permanentmarker.jpg",
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
                    Name = "Post-It",
                    Barcode="4046719100675",
                    Image="postit.jpg",
                    WasteType="plastic",
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