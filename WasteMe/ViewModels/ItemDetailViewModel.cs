using System;

using WasteMe.Models;

namespace WasteMe.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Name;
            Item = item;
        }
        public ItemDetailViewModel(String barcode)
        {
            Item = DataStore.GetItemAsyncBarcode(barcode).ConfigureAwait(false).GetAwaiter().GetResult();
            Title = Item?.Name;
        }
    }
}
