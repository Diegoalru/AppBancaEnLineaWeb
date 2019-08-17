using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AppBancaEnLineaWeb.Utils
{
    class ConverMoneda
    {
        public string CurrencyConversion(decimal amount, string fromCurrency, string toCurrency)
        {
            string Output = "";
            string fromCurrency1 = fromCurrency;
            string toCurrency1 = toCurrency;
            decimal amount1 = Convert.ToDecimal(amount);

            // For other currency symbols see http://finance.yahoo.com/currency-converter/
            // Construct URL to query the Yahoo! Finance API

            const string urlPattern = "http://finance.yahoo.com/d/quotes.csv?s={0}{1}=X&f=l1";
            string url = string.Format(urlPattern, fromCurrency1, toCurrency1);

            // Obtener String
            string response = new WebClient().DownloadString(url);

            // Convertir el string a decimal
            decimal exchangeRate =
                decimal.Parse(response, System.Globalization.CultureInfo.InvariantCulture);

            // Resultado final de la conversion
            Output = (amount1 * exchangeRate).ToString();
     
            return Output;
        }


    }
}
