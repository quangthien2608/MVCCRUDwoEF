using System.ComponentModel.DataAnnotations;

namespace MVCCRUDwoEF.Models
{
    public class PenViewModel
    {
        [Key]
        public int PenID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Type { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "should be greated than 1")]
        public int Price { get; set; }
    }
}
