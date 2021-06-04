using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShopBridgeWebApp.Data;
using ShopBridgeWebApp.Models;

namespace ShopBridgeWebApp.Controllers
{
    public class InventoriesController : Controller
    {
        private readonly ShopBridgeWebAppContext _context;
        private string apiBaseUrl = "";
        public InventoriesController(ShopBridgeWebAppContext context, IConfiguration configuration)
        {
            _context = context;
            apiBaseUrl = configuration["API_URL"];
        }


        
        public async Task<IActionResult> Index()
        {
            List<Inventory> inventories = null;
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent("", Encoding.UTF8, "application/json");
                string endpoint = apiBaseUrl + "api/Inventories/GetData";

                using (var Response = await client.GetAsync(endpoint))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string jsonObj = Response.Content.ReadAsStringAsync().Result;
                        inventories = JsonConvert.DeserializeObject<List<Inventory>>(jsonObj);


                        return View(GetInventories(1,inventories.ToList()));

                    }
                    
                    return View(GetInventories(1, inventories.ToList()));

                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(int currentPageIndex)
        {
            List<Inventory> inventories = null;
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent("", Encoding.UTF8, "application/json");
                string endpoint = apiBaseUrl + "api/Inventories/GetData";

                using (var Response = await client.GetAsync(endpoint))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string jsonObj = Response.Content.ReadAsStringAsync().Result;
                        inventories = JsonConvert.DeserializeObject<List<Inventory>>(jsonObj);


                        return View(GetInventories(currentPageIndex, inventories.ToList()));

                    }

                    return View(GetInventories(currentPageIndex, inventories.ToList()));

                }
            }
            
        }
        private InventoryModel GetInventories(int currentPage,List<Inventory> inventories)
        {
            int maxRows = 4;
            InventoryModel inventoryModel = new InventoryModel();

            inventoryModel.Inventories = (from inv in inventories
                                          select inv)
                                  .OrderBy(inv => inv.ID)
                                  .Skip((currentPage - 1) * maxRows)
                                  .Take(maxRows).ToList();

            double pageCount = (double)((decimal)inventories.Count() / Convert.ToDecimal(maxRows));
            inventoryModel.PageCount = (int)Math.Ceiling(pageCount);

            inventoryModel.CurrentPageIndex = currentPage;

            return inventoryModel;
        }

        // GET: Inventories
        //    public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Inventory.ToListAsync());
        //}

        // GET: Inventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Inventory inventory = null;
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent("", Encoding.UTF8, "application/json");
                string endpoint = apiBaseUrl + "api/Inventories/GetInventory/"+id;

                using (var Response = await client.GetAsync(endpoint))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string jsonObj = Response.Content.ReadAsStringAsync().Result;
                        inventory = JsonConvert.DeserializeObject<Inventory>(jsonObj);

                        return View(inventory);

                    }
                    return View(inventory);

                }
            }

        }

        // GET: Inventories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,Price")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(inventory), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl + "api/Inventories/Create";

                    using (var Response = await client.PostAsync(endpoint, content))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string jsonObj = Response.Content.ReadAsStringAsync().Result;
                            //inventory = JsonConvert.DeserializeObject<Inventory>(jsonObj);

                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(inventory);
        }

        // GET: Inventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Inventory inventory = null;
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent("", Encoding.UTF8, "application/json");
                string endpoint = apiBaseUrl + "api/Inventories/GetInventory/" + id;

                using (var Response = await client.GetAsync(endpoint))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string jsonObj = Response.Content.ReadAsStringAsync().Result;
                        inventory = JsonConvert.DeserializeObject<Inventory>(jsonObj);

                        return View(inventory);

                    }
                    return View(inventory);

                }
            }


            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,Price")] Inventory inventory)
        {
            if (id != inventory.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    using (HttpClient client = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(inventory), Encoding.UTF8, "application/json");
                        string endpoint = apiBaseUrl + "api/Inventories/Edit";

                        using (var Response = await client.PostAsync(endpoint, content))
                        {
                            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                string jsonObj = Response.Content.ReadAsStringAsync().Result;
                                //inventory = JsonConvert.DeserializeObject<Inventory>(jsonObj);

                            }
                        }
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

         
            Inventory inventory = null;
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent("", Encoding.UTF8, "application/json");
                string endpoint = apiBaseUrl + "api/Inventories/GetInventory/" + id;

                using (var Response = await client.GetAsync(endpoint))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string jsonObj = Response.Content.ReadAsStringAsync().Result;
                        inventory = JsonConvert.DeserializeObject<Inventory>(jsonObj);

                        return View(inventory);

                    }
                }
            }

            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
         
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(id.ToString(), Encoding.UTF8, "application/json");
                string endpoint = apiBaseUrl + "api/Inventories/delete" ;

                using (var Response = await client.PostAsync(endpoint,content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string jsonObj = Response.Content.ReadAsStringAsync().Result;
                        //inventory = JsonConvert.DeserializeObject<Inventory>(jsonObj);

                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventory.Any(e => e.ID == id);
        }
    }
}
