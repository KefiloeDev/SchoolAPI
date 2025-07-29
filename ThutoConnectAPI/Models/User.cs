using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ThutoConnectAPI.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Surname { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        public string? Img { get; set; } = string.Empty;
        [Required]
        public string Gender { get; set; } = string.Empty;
        [Required]
        public DateOnly Birthday { get; set; }
        public string? Password { get; set; } = string.Empty;
        public string Role { get; set; } = "Student";
        [Required]
        public string SchoolName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
    }
}
