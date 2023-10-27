using Microsoft.EntityFrameworkCore;
using ProjectRSI.Aplication;
using ProjectRSI.Domain;

namespace ProjectRSI.Persistence
{
    public class RepositoryActivoFijo : IActivoFijo
    {
        private readonly ContextDB context;

        public RepositoryActivoFijo(ContextDB context)
        {
            this.context = context;
        }
        public int Add(ActivoFijo activo)
        {
            var validaitonCode = context.ActivosFijos.Where(x => x.CodigoIdent == activo.CodigoIdent).FirstOrDefault();
            if (validaitonCode?.CodigoIdent != null)
            {
                return 0;
            }
            var validationCategoria = context.Categorias.Find(activo.IdCategoria);
            if (validationCategoria == null)
            {
                return 0;
            }

            Random random = new();
            var codigoAleatorio = random.Next(100000000, 999999999).ToString();
            activo.CodigoIdent = $"RSI-{codigoAleatorio}";
            activo.FechaRegistro = DateTime.UtcNow;
            context.Add(activo);
            return context.SaveChanges();
        }

        public ActivoFijo? FindId(int id)
        {
            return context.ActivosFijos
                    .Include(activo => activo.Categoria) // Incluye la relación con Categoria
                    .FirstOrDefault(activo => activo.IdActivo == id);
        }

        public IEnumerable<ActivoFijo> GetListActivos()
        {
            return context.ActivosFijos.Include(a => a.Categoria).OrderByDescending(x=>x.FechaRegistro);
        }
        public int Remove(ActivoFijo activo)
        {
            context.Remove(activo);
            return context.SaveChanges();
        }
        public IEnumerable<ActivoFijo> SearchActivos(Func<ActivoFijo, bool> predicate)
        {
            return context.ActivosFijos.Include(x=>x.Categoria).Where(predicate).ToList();
        }

        public int Update(ActivoFijo activo)
        {
            context.Update(activo);
            return context.SaveChanges();
        }
    }
}
