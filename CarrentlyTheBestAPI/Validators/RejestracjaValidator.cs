using CarrentlyTheBestAPI.Entities;
using FluentValidation;

namespace CarrentlyTheBestAPI.Validators
{
    public class RejestracjaValidator : AbstractValidator<Rejestracja>
    {
        public RejestracjaValidator(WypozyczenieDbContext dbContext)
        {
            
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Haslo).MinimumLength(6);
            RuleFor(x => x.PowtorzHaslo).Equal(e => e.Haslo);
            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var uzytyEmail = dbContext.Uzytkownicy.Any(u => u.Email == value);
                if (uzytyEmail)
                {
                    context.AddFailure("Email", "Email w użyciu!");
                }

            });

        }
    }
}
