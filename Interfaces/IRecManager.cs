using System.Threading.Tasks;

namespace RMS.Interfaces
{
    public interface IRecManager
    {
        Task<ResponseStatus> Create(RecMMaster model);
        Task<ResponseStatus> Update(RecMMaster model);
        Task<List<RecMMaster>> GetAll();
        Task<RecMMaster> GetById(int id);
        Task<ResponseStatus> DeleteItemById(int id);
        Task<ResponseStatus> Delete(int id);
    }
}

