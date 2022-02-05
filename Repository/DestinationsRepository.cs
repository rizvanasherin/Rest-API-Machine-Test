using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencyApp.Models;
using TravelAgencyApp.ViewModel;

namespace TravelAgencyApp.Repository
{
    public class DestinationsRepository : IDestinationRepository
    {
        //data fields
        private readonly Travel_AgencyContext _context;

        //default constructor
        //constructor based dependency injection
        public DestinationsRepository(Travel_AgencyContext context)
        {
            _context = context;
        }

        //Implement the interface
        #region Get All Destinations
        public async Task<List<Destinations>> GetAllDestinations()
        {
            if (_context != null)
            {
                return await _context.Destinations.ToListAsync();
            }
            return null;
        }

        #endregion

        #region Add Destinations
        public async Task<int> AddDestinations(Destinations destinations)
        {
            if (_context != null)
            {
                await _context.Destinations.AddAsync(destinations);
                await _context.SaveChangesAsync(); //commit the transaction
                return destinations.DestinationId;
            }
            return 0;
        }
        #endregion

        #region update aDestinations
        public async Task UpdateDestinations(Destinations destinations)
        {
            if (_context != null)
            {
                _context.Entry(destinations).State = EntityState.Modified;
                _context.Destinations.Update(destinations);
                await _context.SaveChangesAsync(); //commit the transaction

            }
        }
        #endregion
        #region get Destinations by id
        public async Task<ActionResult<Destinations>> GetDestinationsById(int? id)
        {
            if (_context != null)
            {
                var destinations = await _context.Destinations.FindAsync(id);
                return destinations;
            }
            return null;
        }
        #endregion

        #region delete destinations
        public async Task<int> DeleteDestinations(int? id)
        {
            int result = 0;
            if (_context != null)
            {
                var destinations = await _context.Destinations.FirstOrDefaultAsync(emp => emp.DestinationId == id);
                //check condition
                if (destinations != null)
                {
                    //delete
                    _context.Destinations.Remove(destinations);

                    //commit
                    result = await _context.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }
        #endregion

        public async Task<string> GetMostDestination()
        {

            if (_context != null)
            {

                return " " + await (
                        from c in _context.PlananddestinationDetails
                        group c.PlanId by c.PlanId into grp
                        orderby grp.Count() descending
                        select  grp.Key).FirstOrDefaultAsync();
            }
            return null;
        }
        /* select Top 1 destination_id,count(plan_id) as counts from plananddestination_details
            group by destination_id
            order by counts desc;*/

    }
}
