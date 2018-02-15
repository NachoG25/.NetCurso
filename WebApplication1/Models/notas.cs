namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Serializable()]
    public partial class notas
    {
        [Key]
        public int idnotas { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Escriba el contenido de su nota")]
        [StringLength(500)]
        public string texto { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Escriba el nombre de su nota")]
        [StringLength(500)]
        public string nombre { get; set; }

        public int notas_usuario_fk { get; set; }

        public virtual usuarios usuarios { get; set; }

        public notas()
        {

        }

        public notas(int id, string text, string nomb, int fk_user)
        {
            idnotas = id;
            texto = text;
            nombre = nomb;
            notas_usuario_fk = fk_user;
        }

        class myViewNotas
        {
            public List<notas> listNotas { get; set; }
        }
    }
}
