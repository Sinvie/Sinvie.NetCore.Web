using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sinvie.Cms.Models;
using Sinvie.Cms.ViewModels;

namespace Sinvie.Cms.WebUI.Controllers
{
    public class ContentController : Controller
    {
        public IActionResult Index()
        {
            var contents = new List<M_T_Content>();
            for (int i = 1; i < 11; i++)
            {
                contents.Add(new M_T_Content { Id = i+1, title = $"{i}的标题", content = $"{i}的内容", status = i % 2 == 0 ? 1 : 0, add_time = DateTime.Now.AddDays(-i) });
            }
            return View(new M_T_ViewContent { Contents = contents });
        }

        public IActionResult Analysis() {
            return View();
        }
    }
}