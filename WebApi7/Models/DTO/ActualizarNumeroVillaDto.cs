﻿using System.ComponentModel.DataAnnotations;

namespace WebApi7.Models.DTO
{
    public class ActualizarNumeroVillaDto
    {
        [Required]
        public int NoVilla { get; set; }

        [Required]
        public int VillaId { get; set; }

        [MaxLength(300)]
        public string Detalle { get; set; }
    }
}
