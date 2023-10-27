using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectRSI.Aplication;
using ProjectRSI.Domain;

namespace ProjectRSI.Controllers
{
    public class ActivoFijoController : Controller
    {
        private readonly IActivoFijo activo;
        private readonly ICategoria categoria;

        public ActivoFijoController(IActivoFijo activo, ICategoria categoria)
        {
            this.activo = activo;
            this.categoria = categoria;
        }
        public IActionResult Index()
        {
            var listactivo = activo.GetListActivos();
            return View(listactivo);
        }
        public IActionResult Create()
        {
            var listcategoria = categoria.GetListCategoria();
            ViewBag.ListaCategoria = new SelectList(listcategoria, "IdCategoria", "NombreCat");

            return View(new ActivoFijo());
        }
        [HttpPost]
        public IActionResult Create(ActivoFijo activofijo)
        {
            if (ModelState.IsValid)
            {
                var status = activo.Add(activofijo);
                if (status < 1)
                {
                    TempData["message"] = "Error: no se pudo insertar el registro";
                    return RedirectToAction("Index");
                }
                TempData["message"] = "Registro creado exitosamente";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Search(string codigoIdent, string marca, string modelo, string categoria)
        {
            IEnumerable<ActivoFijo> searchResults = null ?? Enumerable.Empty<ActivoFijo>();

            Func<ActivoFijo, bool> predicate = activo =>
                (string.IsNullOrEmpty(codigoIdent) || (activo.CodigoIdent?.Contains(codigoIdent.Trim()) ?? false)) &&
                (string.IsNullOrEmpty(marca) || (activo.Marca?.Contains(marca.Trim()) ?? false)) &&
                (string.IsNullOrEmpty(modelo) || (activo.Modelo?.Contains(modelo.Trim()) ?? false)) &&
                (string.IsNullOrEmpty(categoria) ||
                    (activo.Categoria != null && !string.IsNullOrEmpty(activo.Categoria.NombreCat) && activo.Categoria.NombreCat.Contains(categoria.Trim()))
                );
            searchResults = activo.SearchActivos(predicate);

            return View("index", searchResults);
        }

        public IActionResult Edit(int id)
        {
            var validation = activo.FindId(id);
            if (validation == null)
            {
                return NotFound();
            }
            var listaCategorias = categoria.GetListCategoria();
            ViewBag.ListaCategorias = new SelectList(listaCategorias, "IdCategoria", "NombreCat");



            return View(validation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditActivoFijo(ActivoFijo act)
        {
            if (ModelState.IsValid)
            {
                var status = activo.Update(act);
                if (status < 1)
                {
                    TempData["message"] = "Error: no se pudo editar el registro";
                    return RedirectToAction("Index");
                }
                TempData["message"] = "Registro editado exitosamente";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var validation = activo.FindId(id);
            if (validation == null)
            {
                return NotFound();
            }
            return View(validation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteActivoFijo(ActivoFijo act)
        {
            if (ModelState.IsValid)
            {
                var status = activo.Remove(act);
                if (status < 1)
                {
                    TempData["message"] = "Error: no se pudo eliminar el registro";
                    return RedirectToAction("Index");
                }
                TempData["message"] = "Registro eliminado exitosamente";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
