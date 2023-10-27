using ProjectRSI.Domain;

namespace ProjectRSI.Aplication
{
    public interface IActivoFijo
    {
        IEnumerable<ActivoFijo> GetListActivos();
        int Add(ActivoFijo activo);
        int Remove(ActivoFijo activo);
        int Update(ActivoFijo activo);
        IEnumerable<ActivoFijo> SearchActivos(Func<ActivoFijo, bool> predicate);
        ActivoFijo? FindId(int id);
    }
}
