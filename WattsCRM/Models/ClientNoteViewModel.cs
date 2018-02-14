using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WattsCRM.Models
{
    public class ClientNoteViewModel
    {
        [Required]
        [Key]
        public Guid ClientNoteId { get; set; }

        public Guid ClientId { get; set; }

        [Required]
        public string Note { get; set; }

        public DateTime Created { get; set; }
    }
}
