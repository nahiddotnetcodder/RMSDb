using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace RMS.Repositories
{
    public class ResSalesRepo : IResSales
    {
        private readonly RmsDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ResSalesRepo(RmsDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
       
        public async Task<ResponseStatus> Create(ResSalesMaster model)
        {
            var status = new ResponseStatus();
            var currentUser = GetCurrentUser();

            var resTable = await _context.ResTable.Where(x => x.RTNumber == model.RSMTableNo).ToListAsync();
            if (resTable.Count == 0)
            {
                status.StatusCode = 0;
                status.Message = "Table Number not valid";
                return status;
            }

            model.RSMId = 0;
            model.CUser = currentUser.FullName;
            _context.ResSalesMaster.Add(model);
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
        public async Task<List<ResSalesMaster>> GetAll()
        {
            var result = new List<ResSalesMaster>();
            var data = await _context.ResSalesMaster.Include(x=>x.HREmpDetails).ToListAsync();
            if (data == null)
                return result;
            foreach (var item in data)
            {
                result.Add(new ResSalesMaster
                {
                    RSMId = item.RSMId,
                    RSMTableNo = item.RSMTableNo,
                    RDCId = item.RDCId,
                    RDCDate = item.RDCDate,
                    HREmpDetailsName = item.HREmpDetails.HREDEName,
                    RSMTime = item.RSMTime,
                    RSMPerson = item.RSMPerson,
                    IsBPrint = item.IsBPrint,
                    IsBPaid = item.IsBPaid,
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
        public async Task<ResSalesMaster> GetById(int id)
        {
            var data = from master in _context.ResSalesMaster.Where(x => x.RSMId == id).Include(x => x.ResSalesDetails)
                       select new ResSalesMaster
                       {
                           RSMId = master.RSMId,
                           RSMTableNo = master.RSMTableNo,
                           RDCId = master.RDCId,
                           RDCDate = master.RDCDate,
                           HREDId = master.HREDId,
                           HREmpDetailsName = master.HREmpDetails == null ? string.Empty : master.HREmpDetails.HREDEName,
                           RSMTime = master.RSMTime,
                           RSMPerson = master.RSMPerson,
                           IsBPrint = master.IsBPrint,
                           IsBPaid = master.IsBPaid,
                           CUser = master.CUser,
                           ResSalesDetails = master.ResSalesDetails
                       };

            var dbData = await data.FirstOrDefaultAsync();
            var result = new ResSalesMaster
            {
                RSMId = dbData.RSMId,
                RSMTableNo = dbData.RSMTableNo,
                RDCId = dbData.RDCId,
                RDCDate = dbData.RDCDate,
                HREDId = dbData.HREDId,
                HREmpDetailsName = dbData.HREmpDetailsName,
                RSMTime = dbData.RSMTime,
                RSMPerson = dbData.RSMPerson,
                IsBPrint = dbData.IsBPrint,
                IsBPaid = dbData.IsBPaid,
                CUser = dbData.CUser,
                ResSalesDetails = GenerateItemsViewModel(dbData)
            };
            return result;
        }
        private List<ResSalesDetails> GenerateItemsViewModel(ResSalesMaster model)
        {
            var result = new List<ResSalesDetails>();
            if (model.ResSalesDetails == null)
                return result;
            foreach (var item in model.ResSalesDetails)
            {
                result.Add(new ResSalesDetails
                {
                    RSDId = item.RSDId,
                    RSDItemCode = item.RSDItemCode,
                    RSDItemName = item.RSDItemName,
                    RSDUPrice = item.RSDUPrice,
                    RSDQty = item.RSDQty,
                    RSDTotalPrice = item.RSDTotalPrice,
                    RSDKotNo = item.RSDKotNo == null ? item.RSDKotNo = 0 : item.RSDKotNo,
                    IsCancel = item.IsCancel
                });
            }
            return result;
        }
        public async Task<ResponseStatus> DeleteItemById(int id)
        {
            var status = new ResponseStatus();

            var currentUser = GetCurrentUser();
            var Item = await _context.ResSalesDetails.Where(x => x.RSDId == id).FirstOrDefaultAsync();
            if (Item == null)
            {
                status.StatusCode = 0;
                status.Message = "Item Not Found";
                return status;
            }
            else
            {
                _context.ResSalesDetails.Remove(Item);
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
        public async Task<ResponseStatus> Update(ResSalesMaster model)
        {
            var status = new ResponseStatus();
            var result = 0;
            var existingParent = _context.ResSalesMaster.Where(p => p.RSMId == model.RSMId).Include(p => p.ResSalesDetails).SingleOrDefault();
            if (existingParent != null)
            {
                model.CreateDate = existingParent.CreateDate;
                // Update parent
                _context.Entry(existingParent).CurrentValues.SetValues(model);
                result = await _context.SaveChangesAsync();

                //Delete children
                foreach (var existingChild in existingParent.ResSalesDetails.ToList())
                {   
                    if (model.ResSalesDetails != null && model.ResSalesDetails.Any())
                    {
                        if (!model.ResSalesDetails.Any(c => c.RSDId == existingChild.RSDId))
                            _context.ResSalesDetails.Remove(existingChild);
                    }
                }

                if (model.ResSalesDetails != null && model.ResSalesDetails.Any())
                {
                    foreach (var childModel in model.ResSalesDetails)
                    {
                        var existingChild = existingParent.ResSalesDetails.Where(c => c.RSDId == childModel.RSDId).FirstOrDefault();

                        if (existingChild != null && existingChild.RSDId > 0)
                        // Update child
                        {
                            childModel.RSMId = model.RSMId;
                            _context.Entry(existingChild).CurrentValues.SetValues(childModel);
                        }
                        else
                        {
                            // Insert child
                            childModel.ResSalesMaster = model;
                            existingParent.ResSalesDetails.Add(childModel);
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
            var data = await _context.ResSalesMaster.Where(x => x.RSMId == id).FirstOrDefaultAsync();

            if (data == null)
            {
                status.StatusCode = 0;
                status.Message = "Entity Updation Failed";
                return status;
            }
            _context.ResSalesMaster.Remove(data);
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

        public DateTime getdate()
        {
            try
            {
                var lastDate = _context.ResDClose.Max(n => n.RDCDate);
                return lastDate.AddDays(1);
            }
            catch
            {
                return DateTime.Now;
            }
        }
    }
}
