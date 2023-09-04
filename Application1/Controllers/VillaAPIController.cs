using Application1.Data;
using Application1.DTO_s;
using Application1.Models;
using Application1.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController :ControllerBase
    {
        private readonly IVillaRepository _iVilla;
        private readonly IMapper _mapper;
        public VillaAPIController(IVillaRepository iVilla, IMapper mapper)
        {
            _iVilla = iVilla;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            var villas = await _iVilla.GetAllAsync();
            if (villas != null) return Ok(_mapper.Map<List<VillaDTO>>(villas)); 
            return BadRequest();
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id == 0) return BadRequest();
            var data = await _iVilla.GetVillaAsync(c=>c.Id==id);
            if(data != null)return Ok(_mapper.Map<VillaDTO>(data));
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<VillaDTO>> CreateVilla ([FromBody]VillaCreateDTO villaDTO)
        {
            if (villaDTO == null)
            {
                return BadRequest();
            }
            var Villa = _mapper.Map<Villa>(villaDTO);
            await _iVilla.CreateAsync(Villa);
            await _iVilla.SaveAsync();
            return Ok(villaDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVilla([FromBody]int id )
        {
            await _iVilla.RemoveAsync(id);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateVilla([FromBody] VillaUpdateDTO villaDTO)
        {
            if ((villaDTO != null) && (villaDTO.Id != null))
            {
                var model = _mapper.Map<Villa>(villaDTO);
                await _iVilla.UpdateAsync(model);
                return NoContent();
            }
            return BadRequest();
        }

    }
}
