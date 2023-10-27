using Microsoft.AspNetCore.Mvc;
using ProjectRSI.Aplication;
using ProjectRSI.Domain;

namespace ProjectRSI.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoria categoria;
        public CategoriaController(ICategoria categoria)
        {
            this.categoria = categoria;
        }
        public IActionResult Index()
        {
            var listCategoria = categoria.GetListCategoria();

            return View(listCategoria);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Categoria category)
        {
            if (ModelState.IsValid)
            {
               var status =  categoria.Add(category);
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
    }
}
