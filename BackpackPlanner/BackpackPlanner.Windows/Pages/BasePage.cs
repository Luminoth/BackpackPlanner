﻿/*
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

using Windows.ApplicationModel.Resources;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace EnergonSoftware.BackpackPlanner.Windows.Pages
{
    public class BasePage : Page
    {
        protected ResourceLoader ResourceLoader { get; private set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ResourceLoader = new ResourceLoader();
        }
    }
}