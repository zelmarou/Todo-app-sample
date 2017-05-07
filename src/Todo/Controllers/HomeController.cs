using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Todo.Model;
using Todo.Model.WebpackAssetsChildren;

namespace Todo.Controllers
{
    [Route("/")]
    public sealed class HomeController : Controller
    {
        private readonly IOptions<main> _webpackMain;
        private readonly IOptions<vendor> _webpackVendor;
        private readonly IOptions<ConfigCore> _configCore;

        public HomeController(IOptions<main> webpackMain, 
                                IOptions<vendor> webpackVendor,
                                IOptions<ConfigCore> configCore)
        {
            _webpackMain = webpackMain;
            _webpackVendor = webpackVendor;
            _configCore = configCore;
        }

        public IActionResult Index()
        {
            var CdnPath = _configCore.Value.CdnUrl.ToString();
            ViewBag.VendorJs = CdnPath + _webpackVendor.Value.js;
            ViewBag.MainJs = CdnPath + _webpackMain.Value.js;
            ViewBag.Css = CdnPath + "styles.css";
            return View();
        }
    }
}