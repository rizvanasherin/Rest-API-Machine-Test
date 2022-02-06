using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyApp.Models;
using TravelAgencyApp.ViewModel;

namespace TravelAgencyApp.Repository
{
    public interface IDestinationRepository
    {
        //Get all Destinations       
        Task<List<Destinations>> GetAllDestinations();

        //Add a Destinations
        Task<int> AddDestinations(Destinations destinations);

        //Update Destinations
        Task UpdateDestinations(Destinations destinations);

        //Delete Destinations
        Task<int> DeleteDestinations(int? id);

        //get Destinations by id
        Task<ActionResult<Destinations>> GetDestinationsById(int? id);

        //get destination with maximum number of plans
        string Getdest();
    }
}
