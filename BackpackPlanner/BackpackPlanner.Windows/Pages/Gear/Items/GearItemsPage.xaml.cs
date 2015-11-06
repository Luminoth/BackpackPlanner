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

using System.Globalization;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using EnergonSoftware.BackpackPlanner.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Units.Units;
using EnergonSoftware.BackpackPlanner.Windows.Utils;

namespace EnergonSoftware.BackpackPlanner.Windows.Pages.Gear.Items
{
    /// <summary>
    /// Wrapper class to make the generic base concrete for XAML
    /// </summary>
    public abstract class GearItemsPageWrapper : ListItemsPage<GearItem>
    {
    }

    public sealed partial class GearItemsPage
    {
        protected override ProgressRing LoadProgressRing => LoadProgress;

        protected override TextBlock NoItemsTextBlock => NoGearItems;

        protected override TextBlock SortByTextBlock => SortBy;

        protected override ListView ItemsListView => GearItemsListView;

        public GearItemsPage()
        {
            InitializeComponent();
        }

        protected override void UpdateValues()
        {
            for(int i=0; i<GearItemsListView.Items?.Count; ++i) {
                GearItem gearItem = (GearItem)GearItemsListView.Items[i];
                DependencyObject lvi = GearItemsListView.ContainerFromIndex(i);

                TextBlock gearItemWeightText = Util.FindVisualChildByName<TextBlock>(lvi, "GearItemWeight");
                if(null != gearItemWeightText) {
                    int weightInUnits = (int)gearItem.WeightInUnits;
                    gearItemWeightText.Text = string.Format(ResourceLoader.GetString("GearItemWeight/Text"),
                        weightInUnits, App.CurrentApp.BackpackPlannerState.Settings.Units.GetSmallWeightString(weightInUnits != 1));
                }

                TextBlock gearItemCostText = Util.FindVisualChildByName<TextBlock>(lvi, "GearItemCost");
                if(null != gearItemCostText) {
                    string formattedCost = gearItem.CostInCurrency.ToString("C", CultureInfo.CurrentCulture);
                    string formattedCostPerWeight = gearItem.CostPerWeightInCurrency.ToString("C", CultureInfo.CurrentCulture);
                    gearItemCostText.Text = string.Format(ResourceLoader.GetString("GearItemCost/Text"),
                        formattedCost, formattedCostPerWeight, App.CurrentApp.BackpackPlannerState.Settings.Units.GetSmallWeightString(false));
                }
            }
        }
    }
}
