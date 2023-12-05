using System.ComponentModel.DataAnnotations;

namespace Gamerz.WebUI.Areas.Admin.Models
{
    public class ProductModelFormViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ürün Modeli girmek zorunludur.")]
        public string Name { get; set; }
        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "Bir kategori seçmek zorunludur.")]
        public int CategoryId { get; set; }
    }
}
