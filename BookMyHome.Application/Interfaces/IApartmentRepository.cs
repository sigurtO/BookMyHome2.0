using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyHome.Domain;

namespace BookMyHome.Application.Interfaces
{
    public interface IApartmentRepository
    {
        Task<IEnumerable<Apartment>> GetAllAsync();
        Task<Apartment> GetByIdAsync(Guid id);
        Task CreateAsync(Apartment apartment);
        Task<bool> UpdateAsync(Guid id, Apartment apartment);
        Task<bool> DeleteAsync(Guid id);
    }
}
