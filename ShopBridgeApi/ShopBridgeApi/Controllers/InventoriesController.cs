using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopBridgeWebApp.Data;
using ShopBridgeWebApp.Models;
using ShopBridgeWebApp.Service;

namespace ShopBridgeWebApp.Controllers
{
    [Route("api/Inventories")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private ILogger _logger;
        private IInventory _service;

        public InventoriesController( ILogger<InventoriesController> logger, IInventory service)
        {   
            _logger = logger;
            _service = service;
        }

        // GET: api/Inventories
        [HttpGet("/api/Inventories/GetInventory/{id}")]
        public ActionResult<Inventory> GetInventory(int id)
        {
            return _service.GetInventory(id);
        }

        [HttpGet("/api/Inventories/GetData")]
        public ActionResult<List<Inventory>> Inventories()
        {
            return _service.Inventories();
        }

        // DELETE: api/Inventories/5
        [HttpPost("/api/Inventories/delete")]
        public ActionResult<Inventory> DeleteInventory([FromBody]int id)
        {
            return _service.DeleteInventory(id);
        }

        [HttpPost("/api/Inventories/Edit")]
        public ActionResult<Inventory> Edit([FromBody] Inventory inventory)
        {
            return _service.EditInventory(inventory);
        }

        [HttpPost("/api/Inventories/Create")]
        public ActionResult<List<Inventory>> Create([FromBody] Inventory inventory)
        {
            return _service.CreateInventory(inventory);
        }


    }
}
