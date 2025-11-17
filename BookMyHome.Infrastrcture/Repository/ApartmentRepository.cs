using BookMyHome.Application.Interfaces;
using BookMyHome.Domain;
using BookMyHome.Infrastrcture.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyHome.Infrastrcture.Repository
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly DbContextBookMyHome _context;

        public ApartmentRepository(DbContextBookMyHome context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Apartment>> GetAllAsync()
        {
            return await _context.Apartments.ToListAsync();
        }

        public async Task<Apartment> GetByIdAsync(Guid id)
        {
            return await _context.Apartments.FindAsync(id);
        }

        public async Task CreateAsync(Apartment apartment)
        {
            try
            {
                await _context.Apartments.AddAsync(apartment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Log the exception (ex) as needed
                throw new Exception("An error occurred while creating the apartment.", ex);
            }
        }

        public async Task<bool> UpdateAsync(Guid id, Apartment apartment)
        {
            var existingApartment = await _context.Apartments.FindAsync(id);
            if (existingApartment == null) return false;

            existingApartment.Address = apartment.Address;
            existingApartment.Description = apartment.Description;
            existingApartment.Price = apartment.Price;
            existingApartment.Image = apartment.Image;
            existingApartment.AvailabiltyStatus = apartment.AvailabiltyStatus;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingApartment = await _context.Apartments.FindAsync(id);
            if (existingApartment == null) return false;

            _context.Apartments.Remove(existingApartment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}


//Task<IEnumerable<Apartment>> GetAllAsync();
//Task<Apartment> GetByIdAsync(Guid id);
//Task CreateAsync(Apartment apartment);
//Task<bool> UpdateAsync(Guid id, Apartment apartment);
//Task<bool> DeleteAsync(Guid id);