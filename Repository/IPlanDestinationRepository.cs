using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgencyApp.Models;

namespace TravelAgencyApp.Repository
{
    public interface IPlanDestinationRepository
    {
        //Get all details
        Task<List<PlananddestinationDetails>> GetAllPlananddestinationDetails();

        //Add PlananddestinationDetails
        Task<int> AddPlananddestinationDetails(PlananddestinationDetails plananddestinationDetails);

        //UpdatePlananddestinationDetails
        Task UpdatePlananddestinationDetails(PlananddestinationDetails plananddestinationDetails);

        //Delete PlananddestinationDetails
        Task<int> DeletePlananddestinationDetails(int? id);

    }
}