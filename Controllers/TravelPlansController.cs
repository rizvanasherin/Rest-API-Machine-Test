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
    public class TravelPlansController : ControllerBase
    {
        private readonly ITravelPlanRepository _travelPlanRepository; //abstraction

        //constructor injection
        public TravelPlansController(ITravelPlanRepository travelPlanRepository) //encapsulation
        {
            _travelPlanRepository = travelPlanRepository;
        }

        #region Get All TravelPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelPlans>>> GetTravelPlansAll()
        {
            return await _travelPlanRepository.GetAllTravelPlans();
        }

        #endregion
        #region Add TravelPlan
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddTravelPlans([FromBody] TravelPlans travelPlans)
        {
            //check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    var TravelPlanId = await _travelPlanRepository.AddTravelPlans(travelPlans);
                    if (TravelPlanId > 0)
                    {
                        return Ok(TravelPlanId);
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

        #region Update TravelPlan

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateTravelPlan([FromBody] TravelPlans travelPlans )
        {
            //check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    await _travelPlanRepository.UpdateTravelPlans(travelPlans);
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

        #region find TravelPlan by id

        [HttpGet("{id}")]
        public async Task<ActionResult<TravelPlans>> GetTravelPlansById(int? id)
        {
            try
            {
                var TravelPlan = await _travelPlanRepository.GetTravelPlansById(id);
                if (TravelPlan == null)
                {
                    return NotFound();
                }
                return TravelPlan;  //return ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region delete TravelPlan
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteTravelPlans(int? id)
        {
            int result = 0;
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                result = await _travelPlanRepository.DeleteTravelPlans(id);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok(); 
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        //method to get details of all plans that have period more than 7 days

        //Endpoint : - https://localhost:44302/api/travelplans/getplans
        [HttpGet]
        [Route("GetPlans")]
        public async Task<IActionResult> GetAllPlans()
        {
            try
            {
                var plan = await _travelPlanRepository.GetPlans();
                if (plan == null)
                {
                    return NotFound();
                }
                return Ok(plan);
            }
            catch (Exception)
            {
                return BadRequest();

            }
        }

    }
}
