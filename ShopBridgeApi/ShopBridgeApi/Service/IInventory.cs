using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopBridgeWebApp.Models;

namespace ShopBridgeWebApp.Service
{
    public interface IInventory
    {
        public List<Inventory> Inventories();
        public Inventory GetInventory(int id);

        public Inventory EditInventory(Inventory inve);
        public Inventory DeleteInventory(int id);
        public List<Inventory> CreateInventory(Inventory inve);
    }
}
