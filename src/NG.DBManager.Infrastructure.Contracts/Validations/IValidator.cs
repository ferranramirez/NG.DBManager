using NG.DBManager.Infrastructure.Contracts.Models;

namespace NG.DBManager.Infrastructure.Contracts.Validations
{
    public interface IValidator<T>
    {
        bool Validate(T t);
    }

    public class TourValidator : IValidator<Tour>
    {
        private const int NameMaxLength = 50;

        public bool Validate(Tour t)
        {
            bool rightNameLength = t.Name.Length <= NameMaxLength;
            bool rightDuration = t.Duration >= 0 && t.Duration <= int.MaxValue;
            return rightNameLength && rightDuration;
        }
    }
}
