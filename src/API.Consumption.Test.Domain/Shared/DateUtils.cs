using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace API.Consumption.Test.Domain.Shared
{
    public static class DateUtils
    {
        public static DateTime TrimSecondsAndMiliseconds(DateTime dt)
        {
            dt = dt.AddMilliseconds(-dt.Millisecond);
            dt = dt.AddSeconds(-dt.Second);
            return dt;
        }

        public static string GetUnixTimestampString()
        {
            TimeSpan ts = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0));
            string timeStamp = ts.TotalSeconds.ToString();
            return timeStamp.Substring(0, timeStamp.IndexOf(","));
        }

        public static DateTime TentarConverterParaData(this string texto)
        {
            DateTime resultado = DateTime.MinValue;
            DateTime.TryParse(texto, CultureInfo.GetCultureInfo("pt-BR").DateTimeFormat, DateTimeStyles.None, out resultado);
            return resultado;
        }

        public static string FormatarData(this DateTime data)
            => data.ToString("dd/MM/yyyy");

        public static string FormatarDataAmericana(this DateTime data)
            => data.ToString("MM/dd/yyyy");

        public static string FormatarDataHora(this DateTime data)
            => data.ToString("dd/MM/yyyy HH:mm:ss");

        public static DateTime? DataValidaOuNull(this DateTime data)
            => data != DateTime.MinValue ?
                new DateTime?(data)
                    : null;
    }
}
