using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencyApp.Models;

namespace TravelAgencyApp.Repository
{
    public class TransportationRepository : ITransportationRepository
    {
        //data fields
        private readonly Travel_AgencyContext _context;

        //default constructor
        //constructor based dependency injection
        public TransportationRepository(Travel_AgencyContext context)
        {
            _context = context;
        }

        //Implement the interface
        #region Get All Transportation
        public async Task<List<Transportation>> GetAllTransportations()
        {
            if (_context != null)
            {
                return await _context.Transportation.ToListAsync();
            }
            return null;
        }

        #endregion

        #region Add Transportation
        public async Task<int> AddTransportation(Transportation transportation)
        {
            if (_context != null)
            {
                await _context.Transportation.AddAsync(transportation);
                await _context.SaveChangesAsync(); //commit the transaction
                return transportation.TransportationId;
            }
            return 0;
        }
        #endregion

        #region update Transportation
        public async Task UpdateTransportation(Transportation transportation)
        {
            if (_context != null)
            {
                _context.Entry(transportation).State = EntityState.Modified;
                _context.Transportation.Update(transportation);
                await _context.SaveChangesAsync(); //commit the transaction

            }
        }
        #endregion
        #region get Transportation by id
        public async Task<ActionResult<Transportation>> GetTransportationById(int? id)
        {
            if (_context != null)
            {
                var transportation = await _context.Transportation.FindAsync(id);
                return transportation;
            }
            return null;
        }
        #endregion

        #region delete Transportation
        public async Task<int> DeleteTransportation(int? id)
        {
            int result = 0;
            if (_context != null)
            {                                       
                var transportation = await _context.Transportation.FirstOrDefaultAsync(emp => emp.TransportationId == id);
                //check condition
                if (transportation != null)
                {
                    //delete
                    _context.Transportation.Remove(transportation);

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
