using System.ComponentModel.DataAnnotations;

namespace OnlineEvent.Model.CategoryModels
{
    public class UpdateCategoryModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "CategorName alanı zorunludur")]
        [MaxLength(100, ErrorMessage = "Max 100 karakter olabilir")]
        public string CategoryName { get; set; } = null!;
    }
}
