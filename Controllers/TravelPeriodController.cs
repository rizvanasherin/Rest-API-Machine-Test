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
    public class TravelPeriodController : ControllerBase
    {
        private readonly ITravelPeriodRepository _travelPeriodRepository; //abstraction

        //constructor injection
        public TravelPeriodController(ITravelPeriodRepository travelPeriodRepository) //encapsulation
        {
            _travelPeriodRepository = travelPeriodRepository;
        }

        #region Get All TravelPeriod
        [HttpGet]
        //  [Authorize]
        //  [Authorize(AuthenticationSchemes ="Bearer")] //use this if the above one is not working
        public async Task<ActionResult<IEnumerable<TravelPeriod>>> GetTravelPeriodAll()
        {
            return await _travelPeriodRepository.GetAllTravelPeriods();
        }

        #endregion
        #region Add TravelPeriod
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddTravelPeriod([FromBody] TravelPeriod travelPeriod)
        {
            //check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    var TravelPeriodId = await _travelPeriodRepository.AddTravelPeriod(travelPeriod);
                    if (TravelPeriodId > 0)
                    {
                        return Ok(TravelPeriodId);
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

        #region Update TravelPeriod

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateTravelPeriod([FromBody] TravelPeriod travelPeriod)
        {
            //check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    await _travelPeriodRepository.UpdateTravelPeriod(travelPeriod);
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

        #region find TravelPeriod by id

        [HttpGet("{id}")]
        public async Task<ActionResult<TravelPeriod>> GetTravelPeriodById(int? id)
        {
            try
            {
                var TravelPeriod = await _travelPeriodRepository.GetTravelPeriodById(id);
                if (TravelPeriod == null)
                {
                    return NotFound();
                }
                return TravelPeriod;  //return ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region delete TravelPeriod
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravelPeriodById(int? id)
        {
            int result = 0;
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                result = await _travelPeriodRepository.DeleteTravelPeriod(id);
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
