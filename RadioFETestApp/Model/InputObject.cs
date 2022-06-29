using System.ComponentModel.DataAnnotations;

namespace RadioFETestApp.Model
{
    public class InputObject
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Input { get; set; }
    }
}
