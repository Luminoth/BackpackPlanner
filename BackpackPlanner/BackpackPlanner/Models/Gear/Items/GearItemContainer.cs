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


using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

namespace EnergonSoftware.BackpackPlanner.Models.Gear.Items
{
    public interface IGearItemContainer
    {
        int GetGearItemCount(List<int> visitedGearItems=null);

        int GetTotalWeightInGrams(List<int> visitedGearItems=null);

        int GetTotalCostInUSDP(List<int> visitedGearItems=null);
    }

    public static class IGearItemContainerExtensions
    {
        public static int GetGearItemCount<T>(List<T> containers, [CanBeNull] List<int> visitedGearItems) where T: IGearItemContainer
        {
            return containers.Sum(container => container?.GetGearItemCount(visitedGearItems) ?? 0);
        }
    }
}
