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
using Windows.UI.Xaml.Controls;

using EnergonSoftware.BackpackPlanner.DAL.Models.Meals;

namespace EnergonSoftware.BackpackPlanner.Windows.Pages.Meals
{
    /// <summary>
    /// Wrapper class to make the generic base concrete for XAML
    /// </summary>
    public abstract class MealsPageWrapper : ListItemsPage<Meal>
    {
    }

    public sealed partial class MealsPage
    {
        protected override ProgressRing LoadProgressRing => LoadProgress;

        protected override TextBlock NoItemsTextBlock => NoMeals;

        protected override TextBlock SortByTextBlock => SortBy;

        protected override ListView ItemsListView => MealsListView;

        public MealsPage()
        {
            InitializeComponent();
        }

        protected override GetItemsCommand<Meal> CreateGetItemsCommand()
        {
            return new GetMealsCommand();
        }

        protected override void UpdateValues()
        {
            for(int i=0; i<MealsListView.Items?.Count; ++i) {
                Meal meal = (Meal)MealsListView.Items[i];
                DependencyObject lvi = MealsListView.ContainerFromIndex(i);
            }
        }
    }
}
