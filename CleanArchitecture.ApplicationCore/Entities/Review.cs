using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int Rating { get; set; }
        public string UserId { get; set; }

        [ForeignKey("Villa")]
        public int VillaId { get; set; }
        public Villa? Villa { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
