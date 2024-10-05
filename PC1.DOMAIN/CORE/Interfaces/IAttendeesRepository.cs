using PC1.DOMAIN.CORE.Entities;

namespace PC1.DOMAIN.CORE.Interfaces
{
    public interface IAttendeesRepository
    {
        Task<bool> Delete(int id);
        Task<IEnumerable<Attendees>> GetAttendees();
        Task<int> Insert(Attendees attendee);
    }
}