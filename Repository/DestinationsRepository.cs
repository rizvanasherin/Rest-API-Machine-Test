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
        #region destination with max number of plans
        public string Getdest()
        {
            var result = (
                             (
                              from t in _context.TravelPlans
                              from d in _context.Destinations
                              from p in _context.PlananddestinationDetails
                              where d.DestinationId == p.DestinationId && t.PlanId == p.PlanId
                              select new
                              {
                                  Destination = d.DestinationName
                              }
                              ).ToList()
                          );

            int count = 0, count1 = 0;
            string destination = "";
            foreach (var t in result.GroupBy(x => x.Destination))
            {
                foreach (var d in t)
                {
                    count++;
                }
                if (count1 < count)
                {
                    count1 = count;
                    destination = t.Key;
                }
                count = 0;
            }
            return destination;
        }
        #endregion
    }
}
