using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoInmobilaria.Data.Repository;
using ProyectoInmobilaria.Data.Repository.Interfaces;
using ProyectoInmobilaria.Models;
using ProyectoInmobilaria.Models.ViewModels;
using ProyectoInmobilaria.Utilities;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace ProyectoInmobilaria.Areas.Customer.Controllers
{
    [Area("Cliente")]
    [Authorize]
    public class PropiedadesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICurrencyConverter _currencyConverter;

        public PropiedadesController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, ICurrencyConverter currencyConverter)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _currencyConverter = currencyConverter;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string currency = "CRC", List<int> caracteristicasSeleccionadas = null)
        {
            //Limpiar filtros si se solicita
            if (Request.Query.ContainsKey("limpiar"))
            {
                HttpContext.Session.Remove("FiltrosCaracteristicas");
                caracteristicasSeleccionadas = new List<int>();
            }
            //Si vienen filtros, guardarlos en sesión
            else if (caracteristicasSeleccionadas?.Any() == true)
            {
                HttpContext.Session.SetString("FiltrosCaracteristicas", JsonSerializer.Serialize(caracteristicasSeleccionadas));
            }
            //Si no vienen, intentar recuperar desde sesión
            else
            {
                var stored = HttpContext.Session.GetString("FiltrosCaracteristicas");
                if (!string.IsNullOrEmpty(stored))
                {
                    caracteristicasSeleccionadas = JsonSerializer.Deserialize<List<int>>(stored);
                }
            }

            //Consulta base de propiedades
            var propiedadesQuery = _unitOfWork.Propiedad
                .GetAll(includeProperties: "Ubicacion,PropiedadCaracteristicas.Caracteristicas");

            //Aplicar filtro si hay características seleccionadas
            if (caracteristicasSeleccionadas?.Any() == true)
            {
                propiedadesQuery = propiedadesQuery
                    .Where(p => caracteristicasSeleccionadas
                        .All(id => p.PropiedadCaracteristicas.Any(pc => pc.CaracteristicasId == id)));
            }

            // Obtener ID del usuario actual si está autenticado
            string userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = _userManager.GetUserId(User);
            }

            // Obtener IDs de propiedades favoritas del usuario
            var favoritosIds = new List<int>();
            if (!string.IsNullOrEmpty(userId))
            {
                favoritosIds = _unitOfWork.Favorito
                    .GetAll()
                    .Where(f => f.UserId == userId)
                    .Select(f => f.PropiedadId)
                    .ToList();
            }

            // Obtener las 3 propiedades más populares (con más favoritos)
            var propiedadesDestacadasIds = _unitOfWork.Favorito
                .GetAll(includeProperties: "Propiedad")
                .GroupBy(f => f.PropiedadId)
                .OrderByDescending(g => g.Count())
                .Take(3)
                .Select(g => g.Key)
                .ToList();

            // Propiedades destacadas similar, incluyendo Ubicacion:
            var propiedadesDestacadas = propiedadesDestacadasIds.Any()
                ? _unitOfWork.Propiedad
                    .GetAll(includeProperties: "Ubicacion")
                    .Where(p => propiedadesDestacadasIds.Contains(p.Id))
                    .Select(p => new PropiedadVM
                    {
                        Propiedad = p,
                        UrlFoto = _unitOfWork.Fotografia.Get(f => f.PropiedadId == p.Id)?.Url,
                        EsFavorita = favoritosIds.Contains(p.Id),
                        Ubicacion = p.Ubicacion
                    })
                    .ToList()
                : new List<PropiedadVM>();

            // Proyección a PropiedadVM con Ubicacion:
            var propiedadVMs = propiedadesQuery
                .Select(p => new PropiedadVM
                {
                    Propiedad = p,
                    UrlFoto = _unitOfWork.Fotografia.Get(f => f.PropiedadId == p.Id)?.Url,
                    EsFavorita = favoritosIds.Contains(p.Id),
                    Ubicacion = p.Ubicacion
                })
                .ToList();

            var viewModel = new PropiedadesIndexVM
            {
                PropiedadesVm = propiedadVMs,
                PropiedadesDestacadas = propiedadesDestacadas,
                SelectedCurrency = currency,
                AvailableCurrencies = _currencyConverter.GetAvailableCurrencies(),
                CurrencySymbols = _currencyConverter.GetAvailableCurrencies()
                                    .ToDictionary(c => c, c => _currencyConverter.GetCurrencySymbol(c)),
                TodasLasCaracteristicas = _unitOfWork.Caracteristicas.GetAll().ToList(),
                CaracteristicasSeleccionadas = caracteristicasSeleccionadas ?? new List<int>(),
                EstadosDisponibles = new List<SelectListItem>
        {
            new SelectListItem("Disponible", "Disponible"),
            new SelectListItem("Vendido", "Vendido"),
            new SelectListItem("Reservado", "Reservado")
        },
                TiposDisponibles = new List<SelectListItem>
        {
            new SelectListItem("Casa", "Casa"),
            new SelectListItem("Apartamento", "Apartamento"),
            new SelectListItem("Lote", "Lote")
        }
            };

            // Pasar convertidor a la vista vía ViewData
            ViewData["CurrencyConverter"] = _currencyConverter;

            return View(viewModel);
        }



        public IActionResult MyProperties()
        {
            var userId = _userManager.GetUserId(User);
            var misProps = _unitOfWork.Propiedad
                         .GetAll()
                         .Where(p => p.UserId == userId)
                         .ToList();

            return View(misProps);
        }

        [HttpGet]
        public IActionResult MyPropertiesTable() {

            return View(); // Retorna la vista para la tabla de propiedades del usuario
        }

        [HttpGet]
        public IActionResult GetPropertiesTable()
        {

            var userId = _userManager.GetUserId(User);
            var misProps = _unitOfWork.Propiedad
                         .GetAll().Where(x => x.UserId == userId).Select(p => new
                         {
                             p.Id,
                             p.Titulo,
                             p.Descripcion,
                             p.Precio,
                             p.Estado,
                             p.Fecha_publicacion,
                             p.Likes,
                         }).ToList();

            //var Ubicacion = _unitOfWork.Ubicacion.Get(u => u.PropiedadId == 13).Direccion;

            return Json(new { data = misProps });
        }

        [HttpGet]
        [Authorize]
        public IActionResult Upsert(int? id)
        {
            var userId = _userManager.GetUserId(User);
            PropiedadVM viewModel = new();

            viewModel.TodasLasCaracteristicas = _unitOfWork.Caracteristicas.GetAll().ToList();

            if (id == null || id <= 0)
            {
                viewModel.Propiedad = new Propiedad();
                viewModel.Ubicacion = new Ubicacion { Pais = "Costa Rica" };
                return View(viewModel);
            }

            viewModel.Propiedad = _unitOfWork.Propiedad.Get(p => p.Id == id, includeProperties: "Ubicacion");
            if (viewModel.Propiedad == null || viewModel.Propiedad.UserId != userId)
            {
                return Forbid();
            }

            // Si existe Ubicacion previa, la usamos; si no, inicializamos con País por defecto
            if (viewModel.Propiedad.Ubicacion != null)
            {
                viewModel.Ubicacion = viewModel.Propiedad.Ubicacion;
            }
            else
            {
                viewModel.Ubicacion = new Ubicacion { Pais = "Costa Rica" };
            }

            // Obtener características seleccionadas
            viewModel.CaracteristicasSeleccionadas = _unitOfWork.PropiedadCaracteristica
                .GetAll()
                .Where(pc => pc.PropiedadId == viewModel.Propiedad.Id)
                .Select(pc => pc.CaracteristicasId)
                .ToList();

            return View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var propiedad = _unitOfWork.Propiedad.Get(p => p.Id == id, includeProperties: "Ubicacion");
            var fotos = _unitOfWork.Fotografia.GetAll()
                .Where(f => f.PropiedadId == id)
                .Select(f => f.Url)
            .ToList();

            var usuario = _userManager.Users
                .FirstOrDefault(u => u.Id == propiedad.UserId);

            var viewModel = new PropiedadVM
            {
                Propiedad = propiedad,
                URLs = fotos,
                NumeroTelefono = usuario?.PhoneNumber?.Replace("+", "").Replace(" ", "").Replace("-", ""),
                CorreoElectronico = usuario?.Email
            };

            return propiedad == null ? NotFound() : View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(PropiedadVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.TodasLasCaracteristicas = _unitOfWork.Caracteristicas.GetAll().ToList();

                return View(viewModel);
            }

            var userId = _userManager.GetUserId(User);
            bool esNueva = viewModel.Propiedad.Id == 0;

            if (esNueva)
            {
                viewModel.Propiedad.Fecha_publicacion = DateTime.Now;
                viewModel.Propiedad.Likes = 0;
                viewModel.Propiedad.UserId = userId;

                _unitOfWork.Propiedad.Add(viewModel.Propiedad);
                _unitOfWork.Save(); // Para generar ID
            }
            else
            {
                var propiedadExistente = _unitOfWork.Propiedad.Get(p => p.Id == viewModel.Propiedad.Id);
                if (propiedadExistente == null || propiedadExistente.UserId != userId)
                    return Forbid();

                propiedadExistente.Titulo = viewModel.Propiedad.Titulo;
                propiedadExistente.Descripcion = viewModel.Propiedad.Descripcion;
                propiedadExistente.Precio = viewModel.Propiedad.Precio;
                propiedadExistente.Estado = viewModel.Propiedad.Estado;
                propiedadExistente.Tipo = viewModel.Propiedad.Tipo;

                _unitOfWork.Save();
            }

            // 🔹 GUARDAR O ACTUALIZAR UBICACIÓN (strings lat/lng)
            if (viewModel.Ubicacion != null)
            {
                viewModel.Ubicacion.PropiedadId = viewModel.Propiedad.Id;

                if (viewModel.Ubicacion.Id == 0)
                {
                    // Nueva ubicación
                    _unitOfWork.Ubicacion.Add(viewModel.Ubicacion);
                }
                else
                {
                    // Ubicación existente: buscar la entidad y actualizar campos
                    var ubExist = _unitOfWork.Ubicacion.Get(u => u.Id == viewModel.Ubicacion.Id);
                    if (ubExist != null)
                    {
                        ubExist.Provincia = viewModel.Ubicacion.Provincia;
                        ubExist.Ciudad = viewModel.Ubicacion.Ciudad;
                        ubExist.Direccion = viewModel.Ubicacion.Direccion;
                        ubExist.Latitud = viewModel.Ubicacion.Latitud;
                        ubExist.Longitud = viewModel.Ubicacion.Longitud;
                        // Si tu repositorio necesita Update: _unitOfWork.Ubicacion.Update(ubExist);
                    }
                    else
                    {
                        // Raro: no existe en BD, lo agregamos
                        _unitOfWork.Ubicacion.Add(viewModel.Ubicacion);
                    }
                }
                _unitOfWork.Save();
            }

            // 🔹 Guardar características seleccionadas
            var existentes = _unitOfWork.PropiedadCaracteristica
                .GetAll()
                .Where(pc => pc.PropiedadId == viewModel.Propiedad.Id)
                .ToList();
            _unitOfWork.PropiedadCaracteristica.RemoveRange(existentes);
            foreach (var idCaract in viewModel.CaracteristicasSeleccionadas)
            {
                _unitOfWork.PropiedadCaracteristica.Add(new PropiedadCaracteristica
                {
                    PropiedadId = viewModel.Propiedad.Id,
                    CaracteristicasId = idCaract
                });
            }
            _unitOfWork.Save();

            // 🔹 Procesar imágenes (igual que antes)
            if (viewModel.Fotografias != null && viewModel.Fotografias.Any())
            {
                string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");
                if (!Directory.Exists(wwwRootPath))
                    Directory.CreateDirectory(wwwRootPath);

                if (!esNueva)
                {
                    var fotosAntiguas = _unitOfWork.Fotografia.GetAll()
                        .Where(x => x.PropiedadId == viewModel.Propiedad.Id).ToList();
                    foreach (var foto in fotosAntiguas)
                    {
                        string rutaCompleta = Path.Combine(wwwRootPath, Path.GetFileName(foto.Url));
                        if (System.IO.File.Exists(rutaCompleta))
                            System.IO.File.Delete(rutaCompleta);
                        _unitOfWork.Fotografia.Remove(foto);
                    }
                    _unitOfWork.Save();
                }

                foreach (var archivo in viewModel.Fotografias)
                {
                    string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(archivo.FileName);
                    string rutaArchivo = Path.Combine(wwwRootPath, nombreArchivo);
                    using (var fileStream = new FileStream(rutaArchivo, FileMode.Create))
                    {
                        await archivo.CopyToAsync(fileStream);
                    }
                    var nuevaFoto = new Fotografia
                    {
                        PropiedadId = viewModel.Propiedad.Id,
                        Url = "/imagenes/" + nombreArchivo
                    };
                    _unitOfWork.Fotografia.Add(nuevaFoto);
                }
                _unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }



        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var propiedad = _unitOfWork.Propiedad.Get(p => p.Id == id);
            var userId = _userManager.GetUserId(User);

            // Verificar propiedad y dueño
            if (propiedad == null || propiedad.UserId != userId)
            {
                return Forbid();
            }

            _unitOfWork.Propiedad.Remove(propiedad);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Propiedad eliminada" });
        }

        public IActionResult Favoritos()
        {
            var userId = _userManager.GetUserId(User);

            var favoritos = _unitOfWork.Favorito
                .GetAll(includeProperties: "Propiedad")
                .Where(f => f.UserId == userId)
                .Select(f => new PropiedadVM
                {
                    Propiedad = f.Propiedad,
                    UrlFoto = _unitOfWork.Fotografia.Get(foto => foto.PropiedadId == f.Propiedad.Id)?.Url
                }).ToList();

            return View(favoritos);
        }

        [Authorize]
        [HttpPost]
        public IActionResult ToggleFavorito(int propiedadId)
        {
            var userId = _userManager.GetUserId(User);

            var favoritoExistente = _unitOfWork.Favorito.Get(f => f.PropiedadId == propiedadId && f.UserId == userId);

            if (favoritoExistente != null)
            {
                // Si ya existe, lo eliminamos
                _unitOfWork.Favorito.Remove(favoritoExistente);
                _unitOfWork.Save();
                return Json(new { success = true, favorito = false });
            }
            else
            {
                // Si no existe, lo agregamos
                var nuevoFavorito = new Favorito
                {
                    PropiedadId = propiedadId,
                    UserId = userId
                };

                _unitOfWork.Favorito.Add(nuevoFavorito);
                _unitOfWork.Save();
                return Json(new { success = true, favorito = true });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetUbicaciones()
        {
            var props = _unitOfWork.Propiedad
                .GetAll(includeProperties: "Ubicacion")
                .Where(p => p.Ubicacion != null
                            && !string.IsNullOrEmpty(p.Ubicacion.Latitud)
                            && !string.IsNullOrEmpty(p.Ubicacion.Longitud))
                .Select(p => new {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Latitud = p.Ubicacion.Latitud,
                    Longitud = p.Ubicacion.Longitud,
                    UrlDetalle = Url.Action("Details", "Propiedades", new { area = "Cliente", id = p.Id })
                })
                .ToList();
            return Json(props);
        }


    }
}
