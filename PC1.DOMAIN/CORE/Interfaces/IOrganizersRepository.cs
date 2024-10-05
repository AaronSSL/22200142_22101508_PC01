using PC1.DOMAIN.CORE.Entities;

namespace PC1.DOMAIN.CORE.Interfaces
{
    public interface IOrganizersRepository
    {
        Task<bool> Delete(int id);
        Task<IEnumerable<Organizers>> GetOrganizers();
        Task<int> Insert(Organizers organizers);
    }
}