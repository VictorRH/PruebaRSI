using System.ComponentModel.DataAnnotations;

namespace ProjectRSI.Domain
{
    public class ActivoFijo
    {
        [Key]
        public int IdActivo { get; set; }
        public string? CodigoIdent { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? NumSerie { get; set; }
        public string? ValorAdqui { get; set; }

        [Display(Name = "Categoria")] 
        [Required(ErrorMessage = "The categoria field is required.")]
        public int IdCategoria { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Categoria? Categoria { get; set; } 

    }
}
