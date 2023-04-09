
namespace RMS.Interfaces
{
    public interface IResDClose
    {
        PaginatedList<ResDClose> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5); //read all
        ResDClose GetItem(int rdcid); // read particular item
        ResDClose Create(ResDClose resdclose);
        ResDClose Edit(ResDClose resdclose);

        public DateTime getDate(); 
    }
}

