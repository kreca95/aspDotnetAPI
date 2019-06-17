using authAPI.Models;
using authAPI.Models.DTOs;
using authAPI.Redis;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace authAPI.Controllers
{
    //[Authorize]
    [RoutePrefix("api/resource")]
    public class ResourceController : ApiController
    {
        private readonly Models.AppContext db = new Models.AppContext();

        [HttpGet]
        public IHttpActionResult GetResource()
        {
            var res = db.Resources.ToList();
            foreach (var item in res)
            {
                item.Tags = item.TagsCompressed.Split(',');
            }
            if (res != null)
            {
                return Ok(res);
            }

            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult GetResource(int id)
        {
            var res = db.Resources.FirstOrDefault(x => x.Id == id);

            if (res != null)
            {
                res.Tags = res.TagsCompressed.Split(',');
                return Ok(res);
            }
            return NotFound();
        }


        [HttpDelete]
        public IHttpActionResult DeleteResource(int id)
        {
            var res = db.Resources.FirstOrDefault(x => x.Id == id);

            if (res != null)
            {

                db.Resources.Remove(res);
                db.SaveChanges();
                return Ok(res);
            }

            return BadRequest();
        }

        [HttpPost]
        public IHttpActionResult CreateResource([FromBody] ResourceDTO resource)
        {
            if (resource != null)
            {


                var res = new Resource()
                {
                    Language = resource.Language,
                    TagsCompressed = String.Join(",", resource.Tags),
                    Value = resource.Value

                };

                db.Resources.Add(res);
                db.SaveChanges();
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("filter")]
        //[CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IHttpActionResult GetFilterResource([FromUri]string filter)
        {
            var filtRes = db.Resources.Where(x => x.TagsCompressed.Contains(filter));
            if (filtRes != null)
            {
                var cache = new RedisCacheProvider();
                cache.Set("filter", filtRes, TimeSpan.FromMinutes(5));
                return Ok(filtRes);

            }
            return NotFound();
        }
    }
}
