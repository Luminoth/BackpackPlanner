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

using System.IO;
using System.Text;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Core.Logging;

namespace EnergonSoftware.BackpackPlanner.Core.PlayServices
{
// TODO: rename this DatabaseSyncManifest and move it up a level
    public sealed class PlayServicesManifest
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(PlayServicesManifest));

        public const string FileTitle = "BackpackPlanner.manifest";

        public const string ContentType = "text/plain";

        public int LastSyncTimestampSeconds { get; set; }

        public int LastUpdateTimestampSeconds { get; set; }

        public async Task Read(Stream stream)
        {
await Task.Delay(0).ConfigureAwait(false);
        }

        public async Task Write(Stream stream)
        {
            string content = $@"Backpacking Planner
Last Sync: {LastSyncTimestampSeconds}
Last Update: {LastUpdateTimestampSeconds}
";

            var contentBytes = Encoding.UTF8.GetBytes(content);
            Logger.Debug($"Manifest file is {contentBytes.Length} bytes");
            await stream.WriteAsync(contentBytes, 0, contentBytes.Length).ConfigureAwait(false);
        }
    }
}
