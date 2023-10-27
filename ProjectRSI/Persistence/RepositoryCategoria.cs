using ProjectRSI.Aplication;
using ProjectRSI.Domain;

namespace ProjectRSI.Persistence
{
    public class RepositoryCategoria : ICategoria
    {
        private readonly ContextDB context;

        public RepositoryCategoria(ContextDB context)
        {
            this.context = context;
        }
        public int Add(Categoria categoria)
        {
            context.Add(categoria);
            return context.SaveChanges();
        }

        public Categoria? GetCategoryById(int categoryId)
        {
            return context.Categorias.FirstOrDefault(c => c.IdCategoria == categoryId);
        }

        public IEnumerable<Categoria> GetListCategoria()
        {
            return context.Categorias;
        }
    }
}
