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
    public class PlanDestinationDetailsController : ControllerBase
    {
        private readonly IPlanDestinationRepository _planDestinationRepository; //abstraction

        //constructor injection
        public PlanDestinationDetailsController(IPlanDestinationRepository planDestinationRepository) //encapsulation
        {
            _planDestinationRepository = planDestinationRepository;
        }

        #region Get All Employees
        [HttpGet]
        //  [Authorize]
        //  [Authorize(AuthenticationSchemes ="Bearer")] //use this if the above one is not working
        public async Task<ActionResult<IEnumerable<PlananddestinationDetails>>> GetPlananddestinationDetailsAll()
        {
            return await _planDestinationRepository.GetAllPlananddestinationDetails();
        }

        #endregion
        #region Add PlananddestinationDetails
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPlananddestinationDetails([FromBody] PlananddestinationDetails plananddestinationDetails)
        {
            //check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    var PlananddestinationDetailsId = await _planDestinationRepository.AddPlananddestinationDetails(plananddestinationDetails);
                    if (PlananddestinationDetailsId > 0)
                    {
                        return Ok(PlananddestinationDetailsId);
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

        #region Update PlananddestinationDetails
        [HttpPut]
       [Authorize]
        public async Task<IActionResult> UpdatePlananddestinationDetails([FromBody] PlananddestinationDetails plananddestinationDetails)
        {
            //check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    await _planDestinationRepository.UpdatePlananddestinationDetails(plananddestinationDetails);
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

        #region delete PlananddestinationDetails
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlananddestinationDetailsById(int? id)
        {
            int result = 0;
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                result = await _planDestinationRepository.DeletePlananddestinationDetails(id);
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