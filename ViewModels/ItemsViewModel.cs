
using System.Collections.Generic;

namespace CoreMigrationsWebApi.ViewModels
{
    public class ItemsViewModel
    {
        public List<ItemViewModel> Items { get; set; }
        public int Count { get; set; }
    }
}