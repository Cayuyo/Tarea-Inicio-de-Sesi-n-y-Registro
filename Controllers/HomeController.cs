#pragma warning disable CS8618
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tarea_Inicio_de_Sesión_y_Registro.Models;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Tarea_Inicio_de_Sesión_y_Registro.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var nombre = HttpContext.Session.GetString("Nombre");

        if (nombre != null)
        {
            return RedirectToAction("Success");
        }
        else
        {
            return View();
        }
    }

    [HttpGet]
    [SessionCheck]
    public IActionResult Registro()
    {
        return View("Index");
    }

    [HttpPost]
    [Route("Usuario/registro")]
    public IActionResult ProcesaRegistro(Usuario NuevoUsuario)
    {

        var existeUsuario = _context.Usuarios.FirstOrDefault(u => u.Correo == NuevoUsuario.Correo);

        if (existeUsuario != null)
        {
            ModelState.AddModelError("Email", "El correo ya está registrado.");
            return View("Index");
        }

        if (ModelState.IsValid)
        {
            PasswordHasher<Usuario> Hasher = new();
            NuevoUsuario.Contrasena = Hasher.HashPassword(NuevoUsuario, NuevoUsuario.Contrasena);
            _context.Usuarios.Add(NuevoUsuario);
            _context.SaveChanges();

            HttpContext.Session.SetString("Nombre", NuevoUsuario.Nombre);
            HttpContext.Session.SetString("Apellido", NuevoUsuario.Apellido);
            HttpContext.Session.SetString("Correo", NuevoUsuario.Correo);
            HttpContext.Session.SetInt32("UsuarioId", NuevoUsuario.UsuarioId);
            Console.WriteLine(NuevoUsuario.UsuarioId);
            return View("Success");
        }
        return View("Index");
    }

    [HttpPost]
    [Route("Usuario/login")]
    public IActionResult ProcesaLogin(Login login)
    {
        Console.WriteLine(ModelState.IsValid);
        if (ModelState.IsValid)
        {
            Usuario? usuario = _context.Usuarios.FirstOrDefault(usu => usu.Correo == login.Correo);

            if (usuario != null)
            {
                PasswordHasher<Login> Hasher = new();
                var result = Hasher.VerifyHashedPassword(login, usuario.Contrasena, login.Contrasena);

                if (result == PasswordVerificationResult.Success)
                {
                    // La contraseña es correcta
                    HttpContext.Session.SetString("Nombre", usuario.Nombre);
                    HttpContext.Session.SetString("Apellido", usuario.Apellido);
                    HttpContext.Session.SetString("Correo", usuario.Correo);
                    HttpContext.Session.SetInt32("UsuarioId", usuario.UsuarioId);

                    return View("Success");
                }
                else
                {
                    // Contraseña incorrecta
                    ModelState.AddModelError(string.Empty, "Contraseña incorrecta");
                }
            }
            else
            {
                // Email no registrado
                ModelState.AddModelError("Correo", "El Correo no está registrado");
            }

            return View("Index");
        }

        return View("Index");
    }

    [HttpGet]
    [Route("Usuario/ProcesaLogout")]
    public IActionResult ProcesaLogout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
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

public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Find the session, but remember it may be null so we need int?
        string? email = context.HttpContext.Session.GetString("Email");
        // Check to see if we got back null
        if (email == null)
        {
            // Redirect to the Index page if there was nothing in session
            // "Home" here is referring to "HomeController", you can use any controller that is appropriate here
            context.Result = new RedirectToActionResult("Index", "", null);
        }
    }
}
