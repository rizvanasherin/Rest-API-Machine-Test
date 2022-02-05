using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyApp.Models;

namespace TravelAgencyApp.Repository
{
    public interface ITransportationRepository
    {
        //Get all 
        Task<List<Transportation>> GetAllTransportations();

        //Add Transportation
        Task<int> AddTransportation(Transportation transportation);

        //Update Transportation
        Task UpdateTransportation(Transportation transportation);

        //Delete Transportation
        Task<int> DeleteTransportation(int? id);

        //get Transportation by id
        Task<ActionResult<Transportation>> GetTransportationById(int? id);
    }
}
