using Newtonsoft.Json;
using System.Threading.Tasks;

namespace RMS.Repositories
{
    public class RecManagerRepo : IRecManager
    {
        private readonly RmsDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RecManagerRepo(RmsDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
       
        public async Task<ResponseStatus> Create(RecMMaster model)
        {
            var status = new ResponseStatus();
            var currentUser = GetCurrentUser();

            model.RMMId = 0;
            model.CUser = currentUser.FullName;
            _context.RecMMaster.Add(model);
            var result = await _context.SaveChangesAsync();

            if (result < 1)
            {
                status.StatusCode = 0;
                status.Message = "Entity Creation Failed";
                return status;
            }
            status.StatusCode = 1;
            status.Message = "Entity Created successfully";
            return status;
        }
        public async Task<List<RecMMaster>> GetAll()
        {
            var result = new List<RecMMaster>();
            var data = await _context.RecMMaster.ToListAsync();
            if (data == null)
                return result;
            foreach (var item in data)
            {
                result.Add(new RecMMaster
                {
                    RMMId = item.RMMId,
                    RMItemCode = item.RMItemCode,
                    RMItemName = item.RMItemName,
                    CUser = item.CUser
                });
            }
            return result;
        }
        public ApplicationUser GetCurrentUser()
        {
            var currentUserString = _httpContextAccessor.HttpContext.Session.GetString(ApplicationConstants.SessionEntity);
            var currentUser = JsonConvert.DeserializeObject<ApplicationUser>(currentUserString);
            return currentUser;
        }
        public async Task<RecMMaster> GetById(int id)
        {
            var data = from master in _context.RecMMaster.Where(x => x.RMMId == id).Include(x => x.RecMDetails)
                       select new RecMMaster
                       {
                           RMMId = master.RMMId,
                           RMItemCode = master.RMItemCode,
                           RMItemName = master.RMItemName,
                           CUser = master.CUser,
                           RecMDetails = master.RecMDetails
                       };

            var dbData = await data.FirstOrDefaultAsync();
            var result = new RecMMaster
            {
                RMMId = dbData.RMMId,
                RMItemCode = dbData.RMItemCode,
                RMItemName = dbData.RMItemName,
                CUser = dbData.CUser,
                RecMDetails = GenerateItemsViewModel(dbData)
            };
            return result;
        }
        private List<RecMDetails> GenerateItemsViewModel(RecMMaster model)
        {
            var result = new List<RecMDetails>();
            if (model.RecMDetails == null)
                return result;
            foreach (var item in model.RecMDetails)
            {
                result.Add(new RecMDetails
                {
                    RMDId = item.RMDId,
                    SIGItemCode = item.SIGItemCode,
                    SIGItemName = item.SIGItemName,
                    RMDQty = item.RMDQty,
                    SIGUnit = item.SIGUnit,
                    SGSUPrice = item.SGSUPrice
                });
            }
            return result;
        }
        public async Task<ResponseStatus> DeleteItemById(int id)
        {
            var status = new ResponseStatus();

            var currentUser = GetCurrentUser();
            var glItem = await _context.RecMDetails.Where(x => x.RMDId == id).FirstOrDefaultAsync();
            if (glItem == null)
            {
                status.StatusCode = 0;
                status.Message = "Item Not Found";
                return status;
            }
            else
            {
                _context.RecMDetails.Remove(glItem);
                var result = await _context.SaveChangesAsync();
                if (result < 1)
                {
                    status.StatusCode = 0;
                    status.Message = "Item deletion Failed";
                    return status;
                }
                else
                {
                    status.StatusCode = 1;
                    status.Message = "Deleted Successfully";
                    return status;
                }
            }
        }
        public async Task<ResponseStatus> Update(RecMMaster model)
        {
            var status = new ResponseStatus();
            var result = 0;
            var existingParent = _context.RecMMaster.Where(p => p.RMMId == model.RMMId).Include(p => p.RecMDetails).SingleOrDefault();
            if (existingParent != null)
            {
                model.CreateDate = existingParent.CreateDate;
                // Update parent
                _context.Entry(existingParent).CurrentValues.SetValues(model);
                result = await _context.SaveChangesAsync();

                //Delete children
                foreach (var existingChild in existingParent.RecMDetails.ToList())
                {   
                    if (model.RecMDetails != null && model.RecMDetails.Any())
                    {
                        if (!model.RecMDetails.Any(c => c.RMDId == existingChild.RMDId))
                            _context.RecMDetails.Remove(existingChild);
                    }
                }

                if (model.RecMDetails != null && model.RecMDetails.Any())
                {
                    foreach (var childModel in model.RecMDetails)
                    {
                        var existingChild = existingParent.RecMDetails.Where(c => c.RMDId == childModel.RMDId).FirstOrDefault();

                        if (existingChild != null && existingChild.RMDId > 0)
                        // Update child
                        {
                            childModel.RMMId = model.RMMId;
                            _context.Entry(existingChild).CurrentValues.SetValues(childModel);
                        }
                        else
                        {
                            // Insert child
                            childModel.RMMaster = model;
                            existingParent.RecMDetails.Add(childModel);
                        }
                    }
                    result = await _context.SaveChangesAsync();
                }
            }
            if (result < 1)
            {
                status.StatusCode = 0;
                status.Message = "Update Failed";
                return status;
            }
            status.StatusCode = 1;
            status.Message = "Updated successfully";
            return status;
        }

        public async Task<ResponseStatus> Delete(int id)
        {
            var status = new ResponseStatus();
            var data = await _context.RecMMaster.Where(x => x.RMMId == id).FirstOrDefaultAsync();

            if (data == null)
            {
                status.StatusCode = 0;
                status.Message = "Entity Updation Failed";
                return status;
            }
            _context.RecMMaster.Remove(data);
            var result = await _context.SaveChangesAsync();
            if (result < 1)
            {
                status.StatusCode = 0;
                status.Message = "Entity Updation Failed";
                return status;
            }
            status.StatusCode = 1;
            status.Message = "Entity Updated successfully";
            return status;
        }
    }
}
