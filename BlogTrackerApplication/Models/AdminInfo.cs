using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogTrackerApplication.Models
{
    [Table("AdminInfo")]
    public class AdminInfo
    {
        [Key]
        public int AdminInfoId { get; set; }

        [Required]
        [EmailAddress]
        public string? EmailId { get; set; }

        [Required]
        [MinLength(6)]

        public string? Password { get; set; }

    }
}