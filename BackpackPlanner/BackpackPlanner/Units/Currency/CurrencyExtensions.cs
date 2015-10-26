/*
   Copyright 2015 Shane Lillie

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;

namespace EnergonSoftware.BackpackPlanner.Units.Currency
{
    /// <summary>
    /// Extensions for the Currency enumeration
    /// </summary>
    public static class CurrencyExtensions
    {
        /// <summary>
        /// Gets the currency string.
        /// </summary>
        /// <param name="currency">The currency.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">Invalid currency!</exception>
        public static string GetCurrencyString(this Currency currency)
        {
            switch(currency)
            {
            case Currency.UnitedStatesDollar:
                return "USD";
            default:
                throw new InvalidOperationException("Invalid currency!");
            }
        }

        /// <summary>
        /// Converts USDP to the currency.
        /// </summary>
        /// <param name="currency">The currency.</param>
        /// <param name="usdp">The USDP value.</param>
        /// <returns>The currency from the given USDP value</returns>
        /// <exception cref="System.InvalidOperationException">Invalid unit system!</exception>
        // ReSharper disable once InconsistentNaming
        public static float CurrencyFromUSDP(this Currency currency, int usdp)
        {
            switch(currency)
            {
            case Currency.UnitedStatesDollar:
                return CurrencyConversion.USDPToUSD(usdp);
            default:
                throw new InvalidOperationException("Invalid currency!");
            }
        }

        /// <summary>
        /// Converts the currency to USDP.
        /// </summary>
        /// <param name="currency">The currency.</param>
        /// <param name="currencyAmount">The currency amount.</param>
        /// <returns>The USDP from the given currency amount</returns>
        /// <exception cref="System.InvalidOperationException">Invalid unit system!</exception>
        // ReSharper disable once InconsistentNaming
        public static int USDPFromCurrency(this Currency currency, float currencyAmount)
        {
            switch(currency)
            {
            case Currency.UnitedStatesDollar:
                return CurrencyConversion.USDToUSDP(currencyAmount);
            default:
                throw new InvalidOperationException("Invalid currency!");
            }
        }
    }
}
