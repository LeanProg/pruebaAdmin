using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecuperacionT2.Models.Entidades;
using RecuperacionT2.Models.Repositorio;
using RecuperacionT2.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace RecuperacionT2.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly Datadb dataContext;
        private readonly IMapper mapper;

        public UsuarioController(ILogger<HomeController> logger, Datadb dataContext, IMapper mapper)
        {
            this.logger = logger;
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public IActionResult Login()
        {
            return View(new UsuarioLoginViewModel());
        }
        [HttpPost]
        public IActionResult Login(UsuarioLoginViewModel userVM)
        {
            try
            {
                if (dataContext.usuarios.IsUserRegister(userVM.nombreusuario, userVM.Clave))
                {
                    HttpContext.Session.SetString("usuariologeado", userVM.nombreusuario);
                    return Redirect("~/Home/Index");
                }
                else
                {
                    userVM.ErrorMessage = "Usuario o contraseña Incorrecto";
                    return View(userVM);
                }
            }
            catch (Exception ex)
            {

                string error = ex.ToString();
                userVM.ErrorMessage = "Usuario o contraseña Incorrecto";
                return View(userVM);
            }

        }
        public IActionResult Logout()
        {
            try
            {
                Usuario usuario = dataContext.usuarios.LoginUser(HttpContext.Session.GetString("usuariologeado"));
                var usuVm = mapper.Map<UsuarioLoginViewModel>(usuario);

                if (usuVm != null)
                {
                    HttpContext.Session.Clear();
                    return Redirect("~/Usuario/Login");
                }
                else
                {
                    return Redirect("~/Usuario/Login");
                }
            }
            catch (Exception ex)
            {

                string message = ex.ToString();
                return Redirect("~/Usuario/Login");
            }
        }
        public IActionResult AltaUsuario()
        {
            return View(new UsuarioAltaViewModel());
        }
        [HttpPost]
        public IActionResult AltaUsuario(UsuarioAltaViewModel usuarioVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (usuarioVM.Clave != usuarioVM.Confirm_Password)
                    {
                        usuarioVM.ErrorMessage = "Las Contraseñas son incorrectas";
                        return View(usuarioVM);
                    }
                    else
                    {
                        Usuario newuser = mapper.Map<Usuario>(usuarioVM);

                        if (!dataContext.usuarios.IsUserRegister(newuser.Nombreusuario, newuser.Clave))
                        {
                            dataContext.usuarios.InsertUsuarios(newuser);
                            return RedirectToAction("Login");

                        }
                        else
                        {
                            usuarioVM.ErrorMessage = "El usuario" + usuarioVM.nombreusuario + " ya se encuentra Registrado";
                            return View(usuarioVM);
                        }
                    }
                }
                else
                {
                    usuarioVM.ErrorMessage = "Ha ocurrido un error, Intente nuevamente Gracias";
                    return View(usuarioVM);

                }

            }
            catch (Exception ex)
            {

                string error = ex.ToString();
                usuarioVM.ErrorMessage = "Ha ocurrido un error, Intente nuevamente Gracias";
                return View(usuarioVM);
            }
        }
        public ActionResult BajaUsuario(int idusuario)
        {
            try
            {
                
                if (HttpContext.Session.GetString("usuariologeado")!=null)
                {
                    dataContext.usuarios.DeleteUsuario(idusuario);
                    return Redirect("~/Home");
                }
                else
                {
                    return Redirect("~/Usuarios/Login");
                }
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
                logger.LogError(msg);
                HttpContext.Session.Clear();
                return Redirect("~/Usuarios/Login");
            }


        }
        public IActionResult EditUsuario(int idusuario)
        {
            try
            {
                
                if (HttpContext.Session.GetString("usuariologeado")!=null)
                {
                    var listusuer = dataContext.usuarios.ReadUsuarios();
                    var usuer = listusuer.Find(a => a.Idusuario == idusuario);
                    var uservm = mapper.Map<UsuarioUpdateViewModel>(usuer);
                    return View(uservm);
                }
                else
                {
                    return Redirect("~/Usuarios/Login");
                }
            }
            catch (Exception ex)
            {

                logger.LogError(ex.ToString());
                return Redirect("~/Usuarios/Login");
            }
        }
        [HttpPost]
        public IActionResult EditUsuario(UsuarioUpdateViewModel userVM)
        {
            try
            {
                
                if ((HttpContext.Session.GetString("usuariologeado")!=null) && ModelState.IsValid)
                {
                    if (userVM.Clave == userVM.Confirm_Password)
                    {
                        var usuario = mapper.Map<Usuario>(userVM);
                        dataContext.usuarios.UpdateUsuarios(usuario);
                        return Redirect("Usuarios");
                    }
                    else
                    {
                        userVM.ErrorMessage = "Las Claves deben ser iguales";
                        return View(userVM);
                    }
                   

                }
                else
                {
                    return Redirect("~/Usuarios/Login");
                }
            }
            catch (Exception EX)
            {

                string erro = EX.ToString();
                logger.LogError(erro);
                return Redirect("~/Usuarios/Login");
            }
        }
        public IActionResult Usuarios()
        {
            try
            {
                
                if (HttpContext.Session.GetString("usuariologeado")!=null)
                {
                    var listaUsuVm = mapper.Map<List<UsuarioViewModel>>(dataContext.usuarios.ReadUsuarios());
                    return View(listaUsuVm);
                }
                else
                {
                    return Redirect("~/Home/Index");
                }

            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                logger.LogError(error);
                return Redirect("~/Home/Index");
            }
        }
    }
}
