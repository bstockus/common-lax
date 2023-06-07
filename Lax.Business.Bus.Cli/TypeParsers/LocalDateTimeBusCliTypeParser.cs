using System;
using NodaTime;
using NodaTime.Text;

namespace Lax.Business.Bus.Cli.TypeParsers {

    public class LocalDateTimeBusCliTypeParser : SimpleBusCliTypeParser<LocalDateTime> {

        public override LocalDateTime ParseValue(string value) =>
            value.ToUpper().Equals("NOW()")
                ? LocalDateTime.FromDateTime(DateTime.Now)
                : (LocalDateTimePattern.CreateWithInvariantCulture("g")).Parse(value).Value;

    }

}