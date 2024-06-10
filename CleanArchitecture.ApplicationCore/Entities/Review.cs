﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.ApplicationCore.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string? ReviewContent { get; set; }
        public int? Rating { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        [ForeignKey("Villa")]
        public int VillaId { get; set; }
        public Villa? Villa { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
