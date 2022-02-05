using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyApp.Models;

namespace TravelAgencyApp.Repository
{
    public interface ITravelPeriodRepository
    {
        //Get all TravelPeriod
        Task<List<TravelPeriod>> GetAllTravelPeriods();

        //Add TravelPeriod
        Task<int> AddTravelPeriod(TravelPeriod travelPeriod);

        //Update TravelPeriod
        Task UpdateTravelPeriod(TravelPeriod travelPeriod);

        //Delete TravelPeriod
        Task<int> DeleteTravelPeriod(int? id);

        //get TravelPeriod by id
        Task<ActionResult<TravelPeriod>> GetTravelPeriodById(int? id);



    }
}
