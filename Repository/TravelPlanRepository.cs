using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencyApp.Models;
using TravelAgencyApp.ViewModel;

namespace TravelAgencyApp.Repository
{
    public class TravelPlanRepository : ITravelPlanRepository
    {
        //data fields
        private readonly Travel_AgencyContext _context;

        //default constructor
        //constructor based dependency injection
        public TravelPlanRepository(Travel_AgencyContext context)
        {
            _context = context;
        }


        //Implement the interface
        #region Get All TravelPlans
        public async Task<List<TravelPlans>> GetAllTravelPlans()
        {
            if (_context != null)
            {
                return await _context.TravelPlans.ToListAsync();
            }
            return null;
        }

        #endregion

        #region Add TravelPlans
        public async Task<int> AddTravelPlans(TravelPlans travelPlans)
        {
            if (_context != null)
            {
                await _context.TravelPlans.AddAsync(travelPlans);
                await _context.SaveChangesAsync(); //commit the transaction
                return travelPlans.PlanId;
            }
            return 0;
        }
        #endregion

        #region update TravelPlans
        public async Task UpdateTravelPlans(TravelPlans travelPlans)
        {
            if (_context != null)
            {
                _context.Entry(travelPlans).State = EntityState.Modified;
                _context.TravelPlans.Update(travelPlans);
                await _context.SaveChangesAsync(); //commit the transaction

            }
        }
        #endregion
        #region get TravelPlans by id
        public async Task<ActionResult<TravelPlans>> GetTravelPlansById(int? id)
        {
            if (_context != null)
            {
                var travelPlans = await _context.TravelPlans.FindAsync(id);
                return travelPlans;
            }
            return null;
        }
        #endregion


        #region delete an employee
        public async Task<int> DeleteTravelPlans(int? id)
        {
            int result = 0;
            if (_context != null)
            {                                      
                var travelPlans = await _context.TravelPlans.FirstOrDefaultAsync(emp => emp.PlanId == id);
                //check condition
                if (travelPlans != null)
                {
                    //delete
                    _context.TravelPlans.Remove(travelPlans);

                    //commit
                    result = await _context.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }
        #endregion

        #region get all plans having period > 7
        public async Task<List<TravelPeriodViewModel>> GetPlans()
        {
            if (_context != null)
            {
                return await (from c in _context.TravelPeriod
                                    join
                                    p in _context.TravelPlans
                                    on c.PlanId equals p.PlanId
                                    where c.PeriodOfDays > 7
                                    select new TravelPeriodViewModel
                                    {

                                        PlanId = p.PlanId,
                                        PlanName = p.PlanName,
                                        PeriodOfDays = c.PeriodOfDays
                                    }
                                    ).ToListAsync();
            }
            return null;
        }
        #endregion
}
}
