using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kitchen;

namespace KitchenAPI.Controllers
{
    [Route("api/[controller]")]
    public class KitchenController : Controller
    {
        private KitchenWorker _kitchenWorker;

        public KitchenController()
        {
            _kitchenWorker = new KitchenWorker();
        }

        
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Recipe Get(string recipeName)
        {
            return _kitchenWorker.GetRecipe(recipeName);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
