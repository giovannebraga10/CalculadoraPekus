using System.ComponentModel.DataAnnotations;

namespace CalculadoraPekus.Models
{
    public class CalculadoraViewModel
    {
        public int Id { get; set; }

        [Display (Name ="Número 1")]
        public double ValueA { get; set; }

        [Display(Name ="Número 2")]
        public double ValueB { get; set; }
        public string Operation { get; set; }
        public double? Result { get; set; }
        public DateTime DateTime { get; set; }
    }
}
