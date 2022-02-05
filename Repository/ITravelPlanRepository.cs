using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyApp.Models;
using TravelAgencyApp.ViewModel;

namespace TravelAgencyApp.Repository
{
   public  interface ITravelPlanRepository
    {
        //Get all TravelPlans
        Task<List<TravelPlans>> GetAllTravelPlans();

        //Add TravelPlans
        Task<int> AddTravelPlans(TravelPlans travelPlans);

        //Update TravelPlans
        Task UpdateTravelPlans(TravelPlans travelPlans);

        //Delete TravelPlans
        Task<int> DeleteTravelPlans(int? id);

        //get an TravelPlans by id
        Task<ActionResult<TravelPlans>> GetTravelPlansById(int? id);

        //get plans having period more than 7 days
        Task<List<TravelPeriodViewModel>> GetPlans();
    }
}
