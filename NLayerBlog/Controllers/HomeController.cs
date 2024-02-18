using Core.Entities;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NLayerBlog.Controllers {
	public class HomeController : Controller {

		private readonly IGenericService<Blog> _genericService;
        public HomeController(IGenericService<Blog> genericService)
        {
            _genericService = genericService;
        }


        [HttpGet]
		public IActionResult Index()
		{
			return View(_genericService.GetAll() ?? null);
		}
	}
}
