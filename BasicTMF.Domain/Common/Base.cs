
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicTMF.Domain.Common
{
    public class Base
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string CreatedBy { get; set; } = default!;
        public DateTime CreatedAt { get; set; } 

        public string? ModifiedBy { get; set; } = default!;
        public DateTime? ModifiedAt { get; set; }

    }
}
