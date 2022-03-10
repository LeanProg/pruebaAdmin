using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecuperacionT2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RecuperacionT2.Models.Repositorio;
using RecuperacionT2.Models.Entidades;
using RecuperacionT2.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace RecuperacionT2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Datadb dataContext;
        private readonly IMapper mapper;

        public HomeController(ILogger<HomeController> logger,Datadb dataContext,IMapper mapper)
        {
            _logger = logger;
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            try
            {
                
                
                if (HttpContext.Session.GetString("usuariologeado")!= null)
                {
                    var uservm = new UsuarioViewModel();
                    uservm.Nombreusuario = HttpContext.Session.GetString("usuariologeado");
                    return View(uservm);
                }
                else
                {
                    return Redirect("~/Usuario/Login");
                }
            }
            catch (Exception ex)
            {

                string error = ex.ToString();
                return Redirect("~/Usuario/Login");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
