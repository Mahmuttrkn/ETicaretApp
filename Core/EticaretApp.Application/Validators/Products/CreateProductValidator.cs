using EticaretApp.Application.ViewModuls.Products;
using FluentValidation;


namespace EticaretApp.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<Product_Create_ViewModel>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen Ürün Adını Boş Geçmeyiniz.")
                .MaximumLength(150)
                .MinimumLength(2)
                    .WithMessage("Lütfen Ürün Adını 2 ile 150 Karakter Arasında Giriniz.");
            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen Stock Adetini Boş Geçmeyiniz.")
                .Must(s => s >= 0)
                    .WithMessage("Stok Bilgisi Negatif Olamaz");
            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen Ürün Fiyatını Boş Geçmeyiniz.")
                .Must(s => s >= 0)
                .WithMessage("Ürün Fiyatı Negatif Olamaz");
        }   
    }
}
