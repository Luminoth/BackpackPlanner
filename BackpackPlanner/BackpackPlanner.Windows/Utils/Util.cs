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

using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace EnergonSoftware.BackpackPlanner.Windows.Utils
{
    public static class Util
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://social.msdn.microsoft.com/Forums/en-US/a76d67e2-e4c0-4af7-8086-657a731ae0e5/find-a-control-or-place-something-in-it?forum=winappswithcsharp#d267e79b-d7a5-40f4-a750-92eb9d129db8
        /// http://stackoverflow.com/questions/26236707/access-xaml-control-inside-ith-item-in-flipview
        /// </remarks>
        public static T FindVisualChildByName<T>(DependencyObject obj, string name) where T: FrameworkElement
        {
            if(null == obj || string.IsNullOrEmpty(name)) {
                return null;
            }

            int numChildren = VisualTreeHelper.GetChildrenCount(obj);
            for(int i=0; i<numChildren; ++i) {
                FrameworkElement child = VisualTreeHelper.GetChild(obj, i) as FrameworkElement;
                if(null == child) {
                    continue;
                }

                T tChild = child as T;
                if(null != tChild && tChild.Name == name) {
                    return tChild;
                }

                tChild = FindVisualChildByName<T>(child, name);
                if(null != tChild) {
                    return tChild;
                }
            }
            return null;
        }
    }
}
