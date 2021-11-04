using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Feedback;

namespace WebApi.Controllers
{
    [Route("api/feedBack")]
    [ApiController]
    public class feedBackController : ControllerBase
    {
        private readonly DataContext dataContext;
        public feedBackController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        private static Expression<Func<feedBack, feedBackDTO>> MappingOfEntityToDTO()
        {
            return x => new feedBackDTO
            {
                Id = x.Id,
                fullName = x.fullName,
                Email = x.Email,
                comments = x.comments

            };
        }

        [HttpGet]
        public IEnumerable<feedBackDTO> ReturnAll()
        {
            return dataContext.Set<feedBack>().Select(MappingOfEntityToDTO()).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<feedBackDTO> GetById(int id)
        {
            var data = dataContext.Set<feedBack>().Where(x => x.Id == id).Select(MappingOfEntityToDTO()).FirstOrDefault();
            if (data == null)
            {
                return NotFound();
            }
            return data;
        }

        [HttpPost]
        public ActionResult<feedBackDTO> Create(feedBackDTO target)
        {
            var data = dataContext.Set<feedBack>().Add(new feedBack
            {
                fullName = target.fullName,
                Email = target.Email,
                comments = target.comments
            });

            dataContext.SaveChanges();
            //Return 200
            return Ok(new feedBackDTO
            {
                fullName = target.fullName,
                Email = target.Email,
                comments = target.comments
            });
        }

        [HttpPut("{id}")]
        public ActionResult<feedBackDTO> Update(int id, feedBackDTO target)
        {
            var data = dataContext.Set<feedBack>().FirstOrDefault(x => x.Id == id);
            data.fullName = target.fullName;
            data.Email = target.Email;
            data.comments = target.comments;

            dataContext.SaveChanges();
            //Return 200
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<feedBackDTO> Delete(int id)
        {
            var data = dataContext.Set<feedBack>().FirstOrDefault(x => x.Id == id);
            dataContext.Set<feedBack>().Remove(data);

            dataContext.SaveChanges();
            //Return 200
            return Ok();
        }
    }
}
    
