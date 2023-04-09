using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using RMS.Models;

namespace RMS.Repositories
{
    public class ResDCloseRepo : IResDClose
    {
        private readonly RmsDbContext _context; // for connecting to efcore.
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ResDCloseRepo(RmsDbContext context, IHttpContextAccessor httpContextAccessor) // will be passed by dependency injection.
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public ResDClose Create(ResDClose resdclose)
        {
            var currentUser = GetCurrentUser();
            resdclose.RDCId = 0;
            resdclose.CUser = currentUser.Id;
            _context.ResDClose.Add(resdclose);
            _context.SaveChanges();
            return resdclose;
        }
        public ResDClose Edit(ResDClose resdclose)
        {         
            _context.ResDClose.Attach(resdclose);
            _context.Entry(resdclose).State = EntityState.Modified;
            _context.SaveChanges();
            return resdclose;
        }
        private List<ResDClose> DoSort(List<ResDClose> items, string SortProperty, SortOrder sortOrder)
        {
            if (SortProperty.ToLower() == "CUser")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(n => n.CUser).ToList();
                else
                    items = items.OrderByDescending(n => n.CUser).ToList();
            }
            return items;
        }
        public PaginatedList<ResDClose> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<ResDClose> items;
            if (SearchText != "" && SearchText != null)
            {
                items = _context.ResDClose.Where(n => n.CUser.Contains(SearchText))
                    .ToList();
            }
            else
                items = _context.ResDClose.ToList();
                items = DoSort(items, SortProperty, sortOrder);
                PaginatedList<ResDClose> retItems = new PaginatedList<ResDClose>(items, pageIndex, pageSize);
                return retItems;
        }
        public ResDClose GetItem(int rdcid)
        {
            ResDClose item = _context.ResDClose.Where(u => u.RDCId == rdcid).FirstOrDefault();
            return item;
        }
        public ApplicationUser GetCurrentUser()
        {
            var currentUserString = _httpContextAccessor.HttpContext.Session.GetString(ApplicationConstants.SessionEntity);
            var currentUser = JsonConvert.DeserializeObject<ApplicationUser>(currentUserString);
            return currentUser;
        }

        public DateTime getDate()
        {
            try
            {
                var lastDate = _context.ResDClose.Max(n => n.RDCDate);
                return lastDate.AddDays(1);
            }
            catch (Exception)
            {
                return DateTime.Now.Date;
            }
        }
    }
}
