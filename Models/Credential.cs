using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
     public class Credential
        {
            [Required]
            [Key]
            public int Id { get; set;}

            [Required]
            [Display(Name = "Username")]
            public string? Username { get; set; } = null!;
            
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string? Password { get; set; } = null!;
        }
}