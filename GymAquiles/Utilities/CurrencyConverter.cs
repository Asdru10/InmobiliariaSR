using System.Collections.Generic;
using System.Linq;

namespace ProyectoInmobilaria.Utilities
{
    public interface ICurrencyConverter
    {
        decimal Convert(decimal amount, string fromCurrency, string toCurrency);
        IEnumerable<string> GetAvailableCurrencies();
        string GetCurrencySymbol(string currencyCode);
    }

    public class CurrencyConverter : ICurrencyConverter
    {
        // Tasas basadas en CRC (1 CRC = X OtraMoneda)
        private readonly Dictionary<string, decimal> _exchangeRates = new()
        {
            {"CRC", 1m},       // Base CRC
            {"USD", 0.002036m},  // Precio dolar
            {"EUR", 0.001718m}    // Precio euro
        };

        public decimal Convert(decimal amount, string fromCurrency, string toCurrency)
        {
            if (fromCurrency == toCurrency) return amount;

            if (!_exchangeRates.ContainsKey(fromCurrency))
                throw new ArgumentException($"Moneda no soportada: {fromCurrency}");
            if (!_exchangeRates.ContainsKey(toCurrency))
                throw new ArgumentException($"Moneda no soportada: {toCurrency}");

            // Convertir a CRC primero
            decimal amountInCrc = amount / _exchangeRates[fromCurrency];

            // Convertir a moneda destino
            return amountInCrc * _exchangeRates[toCurrency];
        }

        public IEnumerable<string> GetAvailableCurrencies()
        {
            return _exchangeRates.Keys.OrderBy(c => c == "CRC" ? 0 : 1);
        }

        public string GetCurrencySymbol(string currencyCode)
        {
            return currencyCode switch
            {
                "CRC" => "₡",
                "USD" => "$",
                "EUR" => "€",
                _ => currencyCode
            };
        }
    }
}