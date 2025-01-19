using FluentValidation;
using System.Data;
using Web_Api.Model;

namespace Web_Api.Validations
{
    public class CountryValidation:AbstractValidator<CountryModel>
    {
        public CountryValidation() 
        {
            RuleFor(country => country.CountryName).NotEmpty().WithMessage("Country Name is Required");

            RuleFor(country => country.CountryCode).NotEmpty().WithMessage("Country Code is Required");

        }
    }
}
