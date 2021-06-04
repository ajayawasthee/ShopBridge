using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridgeWebApp.Models
{
    public class InventoryModel
    {
        public List<Inventory> Inventories { get; set; }

        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
}
