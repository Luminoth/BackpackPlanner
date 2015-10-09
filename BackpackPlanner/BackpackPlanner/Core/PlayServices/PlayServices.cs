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

namespace EnergonSoftware.BackpackPlanner.Core.PlayServices
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://android-developers.blogspot.com/2015/09/google-play-services-81-and-android-60.html
    /// TODO: this name "PlayServices" sucks
    /// TODO: make this a base class (tho we need to solve the need to subclass the Java Object class first)
    /// </remarks>
    public interface IPlayServices
    {
        event EventHandler<EventArgs> PlayServicesConnectedEvent;

        bool Init();

        void Destroy();

        void Connect();

        void Disconnect();
    }
}
