using ShopBridgeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopBridgeWebApp.Data;

namespace ShopBridgeWebApp.Service
{
    public class InventoryService : IInventory
    {
        ShopBridgeWebAppContext context=null;
        public InventoryService(ShopBridgeWebAppContext ctx)
        {
            context = ctx;
        }
        public Inventory DeleteInventory(int id)
        {
            Inventory inve= context.Inventory.Where(a => a.ID == id).FirstOrDefault();
            context.Inventory.Remove(inve);
            context.SaveChanges();
            return inve;
        }

        public Inventory GetInventory(int id)
        {
            return context.Inventory.Where(a => a.ID == id).FirstOrDefault();
        }

        public List<Inventory> Inventories()
        {
            return context.Inventory.ToList();
        }

        public Inventory EditInventory(Inventory inve) {
            context.Update(inve);
            context.SaveChangesAsync();
            return inve;
        }

        public List<Inventory> CreateInventory(Inventory inve)
        {
            context.Inventory.Add(inve);
            context.SaveChanges();
            return context.Inventory.ToList();
        }
    }
}
