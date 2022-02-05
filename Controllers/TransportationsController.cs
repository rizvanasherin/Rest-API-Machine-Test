using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyApp.Models;
using TravelAgencyApp.Repository;

namespace TravelAgencyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportationsController : ControllerBase
    {
        private readonly ITransportationRepository _transportationRepository; //abstraction

        //constructor injection
        public TransportationsController(ITransportationRepository transportationRepository) //encapsulation
        {
            _transportationRepository = transportationRepository;
        }

        #region Get All Transportation
        [HttpGet]
        //  [Authorize]
        //  [Authorize(AuthenticationSchemes ="Bearer")] //use this if the above one is not working
        public async Task<ActionResult<IEnumerable<Transportation>>> GetTransportationsAll()
        {
            return await _transportationRepository.GetAllTransportations();
        }

        #endregion
        #region Add Transportation
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddTransportation([FromBody] Transportation transportation)
        {
            //check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    var TransportationId = await _transportationRepository.AddTransportation(transportation);
                    if (TransportationId > 0)
                    {
                        return Ok(TransportationId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        #endregion

        #region Update Transportation

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateTransportation([FromBody] Transportation transportation)
        {
            //check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    await _transportationRepository.UpdateTransportation(transportation);
                    return Ok();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        #endregion

        #region find Transportation by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Transportation>> GetTransportationById(int? id)
        {
            try
            {
                var Transportation = await _transportationRepository.GetTransportationById(id);
                if (Transportation == null)
                {
                    return NotFound();
                }
                return Transportation;  //return ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region delete Transportation
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransportationById(int? id)
        {
            int result = 0;
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                result = await _transportationRepository.DeleteTransportation(id);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();  //return ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
