using APITut.Data;
using APITut.Models;
using APITut.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITut.Controllers
{
    [Route("api/VillaApi")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        [HttpGet]
        public ActionResult< IEnumerable<VillaDto>> GetVillas()
        {
            return Ok(VillaStore.villaList);
        }

        [HttpGet("{id:int}", Name="GetVilla")]
        [ProducesResponseType(typeof(VillaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.Where(x => x.Id == id).FirstOrDefault();

            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost]
        public ActionResult<VillaDto> AddVilla([FromBody]VillaDto villa)
        {
            if (villa == null)
            {
                return BadRequest();
            }
            if (villa.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                            }
            if(VillaStore.villaList.FirstOrDefault(u => u.Name == villa.Name) != null)
            {
                ModelState.AddModelError("CustomError", villa.Name + "already exists");
                return BadRequest(ModelState);
            }
            villa.Id = VillaStore.villaList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villa);
            return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);        
        }
    }
}
