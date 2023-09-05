using Application1.DTO_s;
using Application1.Models;
using Application1.Repository;
using Application1.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Application1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaNumberController : ControllerBase
    {
        private readonly IVillaNumberRepository _villaNumber;
        private readonly IMapper _mapper;
        private readonly IVillaRepository _villa;
        public VillaNumberController(IMapper mapper, IVillaNumberRepository villaNumber, IVillaRepository villa)
        {
            _mapper = mapper;
            _villaNumber = villaNumber;
            _villa = villa;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVillaNumbers()
        {
            var response = new ApiResponseData<IEnumerable<VillaNumberDTO>>();
            try
            {
                var data = await _villaNumber.GetAllAsync();
                if (data.Count != 0 && data!=null)
                {
                    response.Data = _mapper.Map<List<VillaNumberDTO>>(data);
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.Found;
                    return Ok(response);
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.IsSuccess = false;
                    response.ErrorMessage = new List<string>() { "Data Not Found" };
                    return Ok(response);
                }


            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.Message };
                return Ok(response);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetVillaNumber(int id)
        {
            var response = new ApiResponseData<VillaNumberDTO>();

            try
            {
                var data = await _villaNumber.GetAsync(c => c.VillaId == id);
                if ( data != null)
                {
                    response.StatusCode = HttpStatusCode.Found;
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<VillaNumberDTO>(data);
                    return Ok(response);
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.IsSuccess = false;
                    response.ErrorMessage = new List<string>() { "Data NotFound" };
                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.Message };
                return Ok(response);
            }

        }
        [HttpPut]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberDTO villaNumber)
        {
            var response = new ApiResponseData<string>();

            try
            {
                if (villaNumber == null)
                {
                    response.StatusCode = HttpStatusCode.NotModified;
                    response.IsSuccess = false;
                    response.ErrorMessage = new List<string>() { "You Should put proper data" };
                    return Ok(response);
                }
                var villa = await _villa.GetAsync(c => c.Id == villaNumber.VillaId);
                if (villa == null)
                {
                    ModelState.AddModelError("CustomError", "VilaId Not Vaild");
                    return NotFound(ModelState);
                }

                if ((villaNumber != null) && (villaNumber.VillaId != null && villaNumber.VillaId != 0))
                {
                    var model = _mapper.Map<VillaNumber>(villaNumber);
                    await _villaNumber.Update(model);
                    response.StatusCode = HttpStatusCode.OK;
                    response.IsSuccess = true;
                    response.Data = "Updated Sucessfully";
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
                response.ErrorMessage = new List<string>() { ex.Message };
                return Ok(response);
            }

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteVillaNumber(int villaId)
        {
            var response = new ApiResponseData<string>();
            try
            {
                var record = await _villaNumber.GetAsync(c => c.VillaId == villaId);
                if (record == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.IsSuccess = false;
                    response.ErrorMessage = new List<string>() { "Data Not Found" };
                    return Ok(response);
                }
                await _villaNumber.RemoveAsync(record);
                response.StatusCode = HttpStatusCode.NotFound;
                response.IsSuccess = false;
                response.Data = "Deleted Successfully";
                return Ok(response);
            }

            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.Message };
                return Ok(response);
            }

        }
        [HttpPost]
        public async Task<ActionResult<ApiResponseData<string>>> CreateVillaNumber([FromBody] VillaNumberCreateDTO villaDTO)
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
                var villa = await _villa.GetAsync(c => c.Id == villaDTO.VillaId);
                if (villa ==null)
                {
                    ModelState.AddModelError("CustomError", "VillaId Not Vaild!");
                    return BadRequest(ModelState);
                }
                var Villa = _mapper.Map<VillaNumber>(villaDTO);
                await _villaNumber.CreateAsync(Villa);
                await _villaNumber.SaveAsync();
                response.StatusCode = HttpStatusCode.Created;
                response.IsSuccess = true;
                response.Data = "Created Sucessfully";
                return Ok(response);
            }

            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.Message };
                return Ok(response);
            }
        }
    }
}
