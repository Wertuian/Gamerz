using System.ComponentModel.DataAnnotations;

namespace Gamerz.WebUI.Areas.Admin.Models
{
    public class ProductFormViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Ürün Adı")]
        [Required(ErrorMessage = "Ürün Modeli girmek zorunludur.")]
        public string Name { get; set; }

        [Display(Name = "Ürün Açıklaması")]
        [MaxLength(1000)]
        public string? Description { get; set; }

        [Display(Name = "Ürün Fiyatı")]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "Stok Miktarı")]
        public int UnitInStock { get; set; }

        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "Bir kategori seçmek zorunludur.")]

        public int CategoryId { get; set; }


        [Display(Name = "Ürün Görseli")]
        public IFormFile? File { get; set; }
        [Display(Name = "Ürün Modeli")]
        [Required(ErrorMessage = "Bir Model seçmek zorunludur.")]
        public int ProductModelId { get; set; }
    }
}
