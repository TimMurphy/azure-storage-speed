using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AzureStorageSpeed.Library;
using AzureStorageSpeed.WebApplication.ViewModels.Tables;

namespace AzureStorageSpeed.WebApplication.Controllers
{
    public class TablesController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(new IndexViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Index(IndexViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var tableWriter = new TableSpeedTest(viewModel.ConnectionString, viewModel.TableName, viewModel.StringLength, viewModel.Rows);
            var results = await tableWriter.RunAsync();

            viewModel.Results = results.ToArray();

            return View(viewModel);
        }
    }
}