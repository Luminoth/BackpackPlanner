/*
   Copyright 2017 Shane Lillie

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

using System.Text;
using System.Threading.Tasks;

namespace EnergonSoftware.BackpackPlanner.Core.Util.Crypt
{
    /// <summary>
    /// Hashes values
    /// </summary>
    public abstract class Hasher
    {
        /// <summary>
        /// Hashes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The hash</returns>
        public abstract Task<byte[]> HashAsync(string value);

        /// <summary>
        /// Hashes the value and converts it to a hexadecimal string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The hash value as a hexadecimal string</returns>
        public async Task<string> HashHexAsync(string value)
        {
            var digest = await HashAsync(value).ConfigureAwait(false);

            StringBuilder builder = new StringBuilder();
            foreach(byte b in digest) {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
