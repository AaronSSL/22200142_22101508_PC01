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
    public class AttendeesRepository : IAttendeesRepository
    {
        private readonly EventManagementDbContext _dbContext;

        public AttendeesRepository(EventManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Obtener todos los asistentes
        public async Task<IEnumerable<Attendees>> GetAttendees()
        {
            var attendees = await _dbContext.Attendees.ToListAsync();
            return attendees;
        }

        // Insertar un nuevo asistente
        public async Task<int> Insert(Attendees attendee)
        {
            await _dbContext.Attendees.AddAsync(attendee);
            int rows = await _dbContext.SaveChangesAsync();

            return rows > 0 ? attendee.Id : -1;
        }

        // Eliminar un asistente
        public async Task<bool> Delete(int id)
        {
            var attendee = await _dbContext.Attendees.FirstOrDefaultAsync(c => c.Id == id);

            if (attendee == null) return false;

            _dbContext.Attendees.Remove(attendee);
            int rows = await _dbContext.SaveChangesAsync();

            return (rows > 0);
        }
    }

}

