using System.ComponentModel.DataAnnotations;

namespace ProjectRSI.Domain
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        public string? NombreCat{ get; set; }
        public List<ActivoFijo>? ActivosFijos { get; set; }
    }
}
