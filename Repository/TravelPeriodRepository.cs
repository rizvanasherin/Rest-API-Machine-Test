using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencyApp.Models;

namespace TravelAgencyApp.Repository
{
    public class TravelPeriodRepository : ITravelPeriodRepository
    {
        //data fields
        private readonly Travel_AgencyContext _context;

        //default constructor
        //constructor based dependency injection
        public TravelPeriodRepository(Travel_AgencyContext context)
        {
            _context = context;
        }

        //Implement the interface
        #region Get All TravelPeriod
        public async Task<List<TravelPeriod>> GetAllTravelPeriods()
        {
            if (_context != null)
            {
                return await _context.TravelPeriod.ToListAsync();
            }
            return null;
        }

        #endregion

        #region Add TravelPeriod
        public async Task<int> AddTravelPeriod(TravelPeriod travelPeriod)
        {
            if (_context != null)
            {
                await _context.TravelPeriod.AddAsync(travelPeriod);
                await _context.SaveChangesAsync(); //commit the transaction
                return travelPeriod.PeriodId;
            }
            return 0;
        }
        #endregion

        #region update TravelPeriod
        public async Task UpdateTravelPeriod(TravelPeriod travelPeriod)
        {
            if (_context != null)
            {
                _context.Entry(travelPeriod).State = EntityState.Modified;
                _context.TravelPeriod.Update(travelPeriod);
                await _context.SaveChangesAsync(); //commit the transaction

            }
        }
        #endregion
        #region get TravelPeriod by id
        public async Task<ActionResult<TravelPeriod>> GetTravelPeriodById(int? id)
        {
            if (_context != null)
            {
                var employee = await _context.TravelPeriod.FindAsync(id);
                return employee;
            }
            return null;
        }
        #endregion

        #region delete TravelPeriod
        public async Task<int> DeleteTravelPeriod(int? id)
        {
            int result = 0;
            if (_context != null)
            {                                           
                var travelPeriod = await _context.TravelPeriod.FirstOrDefaultAsync(emp => emp.PeriodId == id);
                //check condition
                if (travelPeriod != null)
                {
                    //delete
                    _context.TravelPeriod.Remove(travelPeriod);

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
