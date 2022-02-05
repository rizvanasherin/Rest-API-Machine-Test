using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencyApp.Models;

namespace TravelAgencyApp.Repository
{
    public class PlanDestinationRepository : IPlanDestinationRepository
    {
        //data fields
        private readonly Travel_AgencyContext _context;

        //default constructor
        //constructor based dependency injection
        public PlanDestinationRepository(Travel_AgencyContext context)
        {
            _context = context;
        }

        //Implement the interface
        #region Get All PlananddestinationDetails
        public async Task<List<PlananddestinationDetails>> GetAllPlananddestinationDetails()
        {
            if (_context != null)
            {
                return await _context.PlananddestinationDetails.ToListAsync();
            }
            return null;
        }

        #endregion

        #region Add PlananddestinationDetails
        public async Task<int> AddPlananddestinationDetails(PlananddestinationDetails plananddestinationDetails)
        {
            if (_context != null)
            {
                await _context.PlananddestinationDetails.AddAsync(plananddestinationDetails);
                await _context.SaveChangesAsync(); //commit the transaction
                return plananddestinationDetails.PId;
            }
            return 0;
        }
        #endregion

        #region update PlananddestinationDetails
        public async Task UpdatePlananddestinationDetails(PlananddestinationDetails plananddestinationDetails)
        {
            if (_context != null)
            {
                _context.Entry(plananddestinationDetails).State = EntityState.Modified;
                _context.PlananddestinationDetails.Update(plananddestinationDetails);
                await _context.SaveChangesAsync(); //commit the transaction

            }
        }
        #endregion
        #region get PlananddestinationDetails by id
        public async Task<ActionResult<PlananddestinationDetails>> GetPlananddestinationDetailsById(int? id)
        {
            if (_context != null)
            {
                var plananddestinationDetails = await _context.PlananddestinationDetails.FindAsync(id);
                return plananddestinationDetails;
            }
            return null;
        }
        #endregion

        #region delete PlananddestinationDetails
        public async Task<int> DeletePlananddestinationDetails(int? id)
        {
            int result = 0;
            if (_context != null)
            {                                           //linq and lambda expression
                var plananddestinationDetails = await _context.PlananddestinationDetails.FirstOrDefaultAsync(emp => emp.PId == id);
                //check condition
                if (plananddestinationDetails != null)
                {
                    //delete
                    _context.PlananddestinationDetails.Remove(plananddestinationDetails);

                    //commit
                    result = await _context.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }
        #endregion

    }
}