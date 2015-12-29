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
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EnergonSoftware.BackpackPlanner.Core.PlayServices
{
    public class PlayServicesLockFile
    {
        public const string FileTitle = "BackpackPlanner.lock";
        public const string ContentType = "text/plain";

        public async Task Read(Stream stream)
        {
await Task.Delay(0).ConfigureAwait(false);
        }

        public async Task Write(Stream stream)
        {
            string content = $"{DateTime.Now}";

            var contentBytes = Encoding.UTF8.GetBytes(content);
            await stream.WriteAsync(contentBytes, 0, contentBytes.Length).ConfigureAwait(false);
        }
    }
}
