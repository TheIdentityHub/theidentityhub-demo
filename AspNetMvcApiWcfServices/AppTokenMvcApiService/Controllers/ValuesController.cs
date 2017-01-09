using System.Collections.Generic;
using System.Web.Http;
using TheIdentityHub;

namespace AppTokenMvcApiService.Controllers
{
    public class ValuesController : ApiController
    {
        // DELETE api/values/5
        [Authorize]
        public void Delete(int id)
        {
        }

        // GET api/values
        [Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "Is Application:" + this.User.IsApp().ToString(), "CliendId:" + this.User.Name(), "Scopes:" + string.Join(" ", this.User.Scopes()) };
        }

        // GET api/values/5
        [Authorize]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [Authorize]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [Authorize]
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}