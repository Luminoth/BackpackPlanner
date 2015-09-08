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

namespace EnergonSoftware.BackpackPlanner.Units
{
    /// <summary>
    /// Unit conversion utilities
    /// </summary>
    public static class CurrencyConversion
    {
        /// <summary>
        /// Converts USDP to USD.
        /// </summary>
        /// <param name="usdp">The USDP value.</param>
        /// <returns>The USDP in USD</returns>
        // ReSharper disable once InconsistentNaming
        public static double USDPToUSD(int usdp)
        {
            return usdp * 0.01;
        }

        /// <summary>
        /// Converts USD to USDP.
        /// </summary>
        /// <param name="usd">The USD value.</param>
        /// <returns>The USD in USDP</returns>
        // ReSharper disable once InconsistentNaming
        public static double USDToUSDP(int usd)
        {
            return usd * 100.0;
        }
    }
}
