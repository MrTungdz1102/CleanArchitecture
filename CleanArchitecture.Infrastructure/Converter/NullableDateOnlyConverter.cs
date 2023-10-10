using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Converter
{
    public class NullableDateOnlyConverter : ValueConverter<DateOnly?, DateTime?>
    {
        public NullableDateOnlyConverter() : base(d => d == null ? null : new DateTime?(d.Value.ToDateTime(TimeOnly.MinValue)), d => d == null ? null : new DateOnly?(DateOnly.FromDateTime(d.Value))) { }
    }
}
