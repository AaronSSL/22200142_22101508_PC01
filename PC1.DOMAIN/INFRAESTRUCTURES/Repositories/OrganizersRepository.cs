using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PC1.DOMAIN.CORE.Entities;
using PC1.DOMAIN.CORE.Interfaces;
using PC1.DOMAIN.INFRAESTRUCTURES.Data;

namespace PC1.DOMAIN.INFRAESTRUCTURES.Repositories
{
    public class OrganizersRepository : IOrganizersRepository
    {
        private readonly EventManagementDbContext _dbContext;
        public OrganizersRepository(EventManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Organizers>> GetOrganizers()
        {
            var organizerss = await _dbContext.Organizers.ToListAsync();
            return organizerss;
        }

        public async Task<int> Insert(Organizers organizers)
        {
            await _dbContext.Organizers.AddAsync(organizers);
            int rows = await _dbContext.SaveChangesAsync();

            return rows > 0 ? organizers.Id : -1;
        }

        public async Task<bool> Delete(int id)
        {
            var organizer = await _dbContext
                            .Organizers
                            .FirstOrDefaultAsync(c => c.Id == id);

            if (organizer == null) return false;

            int rows = await _dbContext.SaveChangesAsync();
            return (rows > 0);

            //_dbContext.Category.Remove(category);

        }

    }
}
