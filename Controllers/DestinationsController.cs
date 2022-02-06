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
    public class DestinationsController : ControllerBase
    {
        private readonly IDestinationRepository _destinationRepository; //abstraction

        //constructor injection
        public DestinationsController(IDestinationRepository destinationRepository) //encapsulation
        {
            _destinationRepository = destinationRepository;
        }

        #region Get All Destinations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Destinations>>> GetDestinationsAll()
        {
            return await _destinationRepository.GetAllDestinations();
        }



        #region get the destination with most number of plans
        [HttpGet]
        [Route("Getdestination")]
        public string Getdest()
        {
            var plan = _destinationRepository.Getdest();
            return plan;
        }
        #endregion


        #endregion
        #region Add Destinations
        [HttpPost]
        [Authorize]
        //  [Authorize(AuthenticationSchemes ="Bearer")] //use this if the above one is not working
        public async Task<IActionResult> AddDestinations([FromBody] Destinations destinations)
        {
            //check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    var DestinationsId = await _destinationRepository.AddDestinations(destinations);
                    if (DestinationsId > 0)
                    {
                        return Ok(DestinationsId);
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

        #region Update Destinations

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateDestinations([FromBody] Destinations destinations)
        {
            //check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    await _destinationRepository.UpdateDestinations(destinations);
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

        #region find an Destinations by id

        [HttpGet("{id}")]
        public async Task<ActionResult<Destinations>> GetDestinationsById(int? id)
        {
            try
            {
                var Destinations = await _destinationRepository.GetDestinationsById(id);
                if (Destinations == null)
                {
                    return NotFound();
                }
                return Destinations;  //return ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region delete Destinations
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDestinationsById(int? id)
        {
            int result = 0;
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                result = await _destinationRepository.DeleteDestinations(id);
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
