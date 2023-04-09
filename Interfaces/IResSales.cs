using System.Threading.Tasks;

namespace RMS.Interfaces
{
    public interface IResSales
    {
        Task<ResponseStatus> Create(ResSalesMaster model);
        Task<ResponseStatus> Update(ResSalesMaster model);
        Task<List<ResSalesMaster>> GetAll();
        Task<ResSalesMaster> GetById(int id);
        Task<ResponseStatus> DeleteItemById(int id);
        Task<ResponseStatus> Delete(int id);
        public DateTime getdate(); //get ResDClose Date
    }
}

