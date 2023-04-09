using Newtonsoft.Json;
using System.Threading.Tasks;

namespace RMS.Controllers
{
    public class ResSalesController : Controller
    {
        private readonly IResSales _repo;
        private readonly IResMenu _resMenu;
        private readonly IResKitchenInfo _info;
        private readonly IHREmpDetails _empDetails;

        public ResSalesController(IResSales repo,IResMenu resMenu,IHREmpDetails empDetails, IResKitchenInfo info)
        {
            _repo = repo;
            _resMenu = resMenu;
            _empDetails= empDetails;    
            _info= info;    

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult masterDetails()
        {
 
            return View();
        }
        public IActionResult ItemDetails()
        {
            return View();
        }
        public async Task<IActionResult> Edit(string id)
        {
            var rsmId = Convert.ToInt32(id);
            var data = await _repo.GetById(rsmId);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> GetInitData()
        {
            var chartMaster = await _resMenu.GetAll();
            var chartMasterDD = chartMaster.Select(x => new NameIdAccountGroupPair
            {
                Id = x.RMId,
                AccountGroupId = x.RKId,
                Name = x.RMItemCode + " " + x.RMItemName,
                Code = x.RMItemCode,
                Description = x.RMItemName,
                Price = (float)x.RMUPrice
            }).ToList();
            var chartType = await _info.GetAll();
            var chartTypeDD = chartType.Select(x => new NameIdPair
            {
                Id = x.RKId,
                Name = x.RKitchenName
            }).ToList();

            var empName = await _empDetails.GetAll();
            var empNameDD = empName.Select(x => new NameIdPair
            {
                Id = x.HREDId,
                Name = x.HREDEName
            }).ToList();

            return new JsonResult(new { chartMasterDD, chartTypeDD , empNameDD });
        }
        [HttpPost]
        public async Task<ActionResult> Save(ResSalesMaster model)
        {
            if (model.RSItems == "[]")
            {
                TempData["ErrorMessage"] = "Item code was null!";
                return View();
            }
            if (model.RSItems != null)
            {
                var jsonItems = JsonConvert.DeserializeObject<List<ResSalesDetails>>(model.RSItems);
                model.ResSalesDetails = jsonItems;
            }
            var result = await _repo.Create(model);
            return Json(result);
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var data = await _repo.GetAll();
            return new JsonResult(data);
        }
        [HttpGet]
        public async Task<ActionResult> GetById(int id)
        {
            var data = await _repo.GetById(id);
            return Json(data);
        }
        [HttpGet]
        public async Task<ActionResult> DeleteItemById(int id)
        {
            var data = await _repo.DeleteItemById(id);
            return Json(data);
        }
        [HttpPost]
        public async Task<ActionResult> Update(ResSalesMaster model)
        {
            if (model.RSItems != null)
            {
                var jsonItems = JsonConvert.DeserializeObject<List<ResSalesDetails>>(model.RSItems);
                model.ResSalesDetails = jsonItems;
            }
            var result = await _repo.Update(model);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _repo.Delete(id);
            return Json(data);
        }

        [HttpGet]
        public  ActionResult DateValue()
        {
            var date =  _repo.getdate();
            return new JsonResult(date);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSalesItems()
        {
            var chartType = await _info.GetAll();
            var chartTypeDD = chartType.Select(x => new NameIdPair
            {
                Id = x.RKId,
                Name = x.RKitchenName
            }).ToList();
            var chartMaster = await _resMenu.GetAll();
            var chartMasterDD = chartMaster.Select(x => new NameIdAccountGroupPair
            {
                Id = x.RMId,
                AccountGroupId = x.RKId,
                Name = x.RMItemCode + " " + x.RMItemName,
                Code = x.RMItemCode,
                Description = x.RMItemName,
                Price = (float)x.RMUPrice
            }).ToList();
            return new JsonResult(new { chartMasterDD, chartTypeDD });
        }
    }
}
