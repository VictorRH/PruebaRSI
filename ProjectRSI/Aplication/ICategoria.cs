using ProjectRSI.Domain;

namespace ProjectRSI.Aplication
{
    public interface ICategoria
    {
        IEnumerable<Categoria> GetListCategoria();
        int Add(Categoria categoria);
        Categoria? GetCategoryById(int categoryId);
    }
}
