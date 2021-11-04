using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.userInfo;

namespace WebApi.Controllers
{
    [Route("api/publicInfo")]
    [ApiController]
    public class PublicInfoController : ControllerBase
    {
        private readonly DataContext dataContext;
        public PublicInfoController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        private static Expression<Func<publicInfo, publicInfoDTO>> MappingOfEntityToDTO()
        {
            return x => new publicInfoDTO
            {
                Id = x.Id,
                fullName = x.fullName,
                Email = x.Email,
                Phonenumber = x.Phonenumber,
                service=x.service,
                Description=x.Description

            };
        }

        [HttpGet]
        public IEnumerable<publicInfoDTO> ReturnAll()
        {
            return dataContext.Set<publicInfo>().Select(MappingOfEntityToDTO()).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<publicInfoDTO> GetById(int id)
        {
            var data = dataContext.Set<publicInfo>().Where(x => x.Id == id).Select(MappingOfEntityToDTO()).FirstOrDefault();
            if (data == null)
            {
                return NotFound();
            }
            return data;
        }

        [HttpPost]
        public ActionResult<publicInfoDTO> Create(publicInfoDTO target)
        {
            var data = dataContext.Set<publicInfo>().Add(new publicInfo
            {
                fullName = target.fullName,
                Email = target.Email,
                Phonenumber = target.Phonenumber,
                service = target.service,
                Description = target.Description
            });

            dataContext.SaveChanges();
            //Return 200
            return Ok(new publicInfoDTO
            {
                fullName = target.fullName,
                Email = target.Email,
                Phonenumber = target.Phonenumber,
                service = target.service,
                Description = target.Description
            });
        }

        [HttpPut("{id}")]
        public ActionResult<publicInfoDTO> Update(int id, publicInfoDTO target)
        {
            var data = dataContext.Set<publicInfo>().FirstOrDefault(x => x.Id == id);
            data.fullName = target.fullName;
            data.Email = target.Email;
            data.Phonenumber = target.Phonenumber;
            data.service = target.service;
            data.Description = target.Description;

            dataContext.SaveChanges();
            //Return 200
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<publicInfoDTO> Delete(int id)
        {
            var data = dataContext.Set<publicInfo>().FirstOrDefault(x => x.Id == id);
            dataContext.Set<publicInfo>().Remove(data);

            dataContext.SaveChanges();
            //Return 200
            return Ok();
        }
    }
}
    

    
