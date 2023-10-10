using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CleanArchitecture.Infrastructure.Converter
{
    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        public DateOnlyConverter(): base(dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
           dateTime => DateOnly.FromDateTime(dateTime)) { }
    }
}
