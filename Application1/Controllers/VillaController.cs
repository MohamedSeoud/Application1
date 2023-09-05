using Application1.Data;
using Application1.DTO_s;
using Application1.Models;
using Application1.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController :ControllerBase
    {
        private readonly IVillaRepository _iVilla;
        private readonly IMapper _mapper;
        public VillaController(IVillaRepository iVilla, IMapper mapper)
        {
            _iVilla = iVilla;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseData<IEnumerable<VillaDTO>>>> GetVillas()
        {
            var response = new ApiResponseData<IEnumerable<VillaDTO>>();
            try
            {
                var data = await _iVilla.GetAllAsync();
                if (data.Count != 0 && data != null)
                {
                    response.Data = _mapper.Map<List<VillaDTO>>(data);
                    response.StatusCode = HttpStatusCode.OK;
                    response.IsSuccess = true;
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.IsSuccess = false;
                    response.ErrorMessage = new List<string>() { "Data Not Found" };
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.IsSuccess = false;
                response.ErrorMessage= new List<string>(){ex.Message};
                return Ok(response);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponseData<VillaDTO>>> GetVilla(int id)
        {
            var response = new ApiResponseData<VillaDTO>();
            try 
            { 
                var data = await _iVilla.GetAsync(c=>c.Id==id);
                if ( data != null)
                {
                    response.Data = _mapper.Map<VillaDTO>(data);
                    response.StatusCode = HttpStatusCode.OK;
                    response.IsSuccess = true;
                    return Ok(response);
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.IsSuccess = false;
                    response.ErrorMessage=new List<string>() { "Data Not Found" };
                    return Ok(response);
                }

            }

            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.IsSuccess = false;
                response.ErrorMessage= new List<string>(){ex.Message};
                return Ok(response);
            }
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponseData<string>>> CreateVilla ([FromBody]VillaCreateDTO villaDTO)
        {
            var response = new ApiResponseData<string>();
            try
            {
                if (villaDTO == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.IsSuccess = false;
                    response.ErrorMessage = new List<string>() { "Put proper data" };
                    return Ok(response);
                }
                var Villa = _mapper.Map<Villa>(villaDTO);
                await _iVilla.CreateAsync(Villa);
                await _iVilla.SaveAsync();
                response.StatusCode = HttpStatusCode.Created;
                response.IsSuccess = true;
                response.Data = "Created Sucessfully";
                return Ok(response);
            }

            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.IsSuccess = false;
                response.ErrorMessage= new List<string>(){ex.Message};
                return Ok(response);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteVilla([FromBody]int id )
        {
            var response = new ApiResponseData<string>();
            try
            {
                var record = await _iVilla.GetAsync(c => c.Id == id);
                if (record == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.IsSuccess = false;
                    response.ErrorMessage = new List<string>() { "Data Not Found" };
                    return Ok(response);
                }
                await _iVilla.RemoveAsync(record);
                response.StatusCode = HttpStatusCode.NotFound;
                response.IsSuccess = false;
                response.Data="Deleted Successfully";
                return Ok(response);
            }

            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.IsSuccess = false;
                response.ErrorMessage= new List<string>(){ex.Message};
                return Ok(response);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateVilla([FromBody] VillaUpdateDTO villaDTO)
        {
            var response = new ApiResponseData<string>();

            try
            {
                if (villaDTO == null)
                {
                    response.StatusCode = HttpStatusCode.NotModified;
                    response.IsSuccess = false;
                    response.ErrorMessage = new List<string>() { "You Should put proper data" };
                    return Ok(response);
                }

                if ((villaDTO != null) && (villaDTO.Id != null && villaDTO.Id != 0))
                {
                    var model = _mapper.Map<Villa>(villaDTO);
                    await _iVilla.UpdateAsync(model);
                    response.StatusCode = HttpStatusCode.OK;
                    response.IsSuccess = true;
                    response.Data="Updated Sucessfully";
                    return Ok(response);
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NotModified;
                    response.IsSuccess = false;
                    response.ErrorMessage = new List<string>() { "You should put a proper data" };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.NotModified;
                response.IsSuccess = false;
                response.ErrorMessage= new List<string>(){ex.Message};
                return Ok(response);
            }
        }

    }
}
