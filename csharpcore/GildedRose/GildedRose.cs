using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace GildedRoseKata
{
    public class GildedRose
    {
        readonly IList<Item> Items;
        private const string AgedBrieItemName = "Aged Brie";
        private const string BackstagePassesItemName = "Backstage passes to a TAFKAL80ETC concert";
        private const string SulfurasItemName = "Sulfuras, Hand of Ragnaros";
        private const string ConjuredItemName = "Conjured";

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        private void DegradeItem(Item item, int degradeRatio)
        {
            int degradeValue = 1 * degradeRatio;

            item.Quality -= degradeValue;

            // Once the sell by date has passed, Quality degrades twice as fast
            if (item.SellIn <= 0)
                item.Quality -= degradeValue;
        }

        private void ProcessCommonRules(Item item)
        {
            // The Quality of an item is never more than 50
            if (item.Quality > 50)
                item.Quality = 50;

            // The Quality of an item is never negative
            if (item.Quality <= 0)
                item.Quality = 0;

            // Always decrease date by 1
            item.SellIn--;
        }

        private bool ProcessSulfurasItem(Item item)
        {
            if (!item.Name.Equals(SulfurasItemName))
                return false;

            // "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
            return true;
        }

        private bool ProcessBrieItem(Item item)
        {
            // "Aged Brie" actually increases in Quality the older it gets
            item.Quality++;

            return true;
        }

        private bool ProcessBackstageItem(Item item)
        {
            // "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches;
            // Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but
            // Quality drops to 0 after the concert
            
            item.Quality++;
            if (item.SellIn < 11)
                item.Quality++;
            if (item.SellIn < 6)
                item.Quality++;
            if (item.SellIn <= 0)
                item.Quality = 0;

            return true;
        }

        private bool ProcessConjuredItem(Item item)
        {
            // "Conjured" items degrade in Quality twice as fast as normal items
            DegradeItem(item, 2);

            return true;
        }

        private bool ProcessNormalItem(Item item)
        {
            DegradeItem(item, 1);

            return true;
        }


        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if (ProcessSulfurasItem(item))
                    continue;

                switch (item.Name)
                {
                    case AgedBrieItemName:
                        ProcessBrieItem(item);
                        break;
                    case BackstagePassesItemName:
                        ProcessBackstageItem(item);
                        break;
                    case string ItemName when ItemName.StartsWith(ConjuredItemName):
                        ProcessConjuredItem(item);
                        break;
                    default:
                        ProcessNormalItem(item);
                        break;
                }

                ProcessCommonRules(item);
            }
        }
    }
}
