using System.Collections.Generic;

namespace GildedRoseKata
{
    public class GildedRose
    {
        IList<Item> Items;
        private const string AgedBrieItemName = "Aged Brie";
        private const string BackstagePassesItemName = "Backstage passes to a TAFKAL80ETC concert";
        private const string SulfurasItemName = "Sulfuras, Hand of Ragnaros";
        private const string ConjuredItemName = "Conjured";
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public bool ProcessBrieItem(Item item)
        {
            if (item.Name != AgedBrieItemName)
                return false;

            if (item.Quality < 50)
                item.Quality = item.Quality + 1;

            item.SellIn = item.SellIn - 1;

            return true;
        }

        public bool ProcessBackstageItem(Item item)
        {
            if (item.Name != BackstagePassesItemName)
                return false;

            if (item.Quality < 50)
            {
                item.Quality = item.Quality + 1;

                if (item.SellIn < 11)
                {
                    if (item.Quality < 50)
                    {
                        item.Quality = item.Quality + 1;
                    }
                }

                if (item.SellIn < 6)
                {
                    if (item.Quality < 50)
                    {
                        item.Quality = item.Quality + 1;
                    }
                }
            }

            if (item.SellIn <= 0)
                item.Quality = item.Quality - item.Quality;

            item.SellIn = item.SellIn - 1;

            return true;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if (item.Name.Equals(SulfurasItemName))
                    continue;

                if (ProcessBrieItem(item))
                    continue;

                if (ProcessBackstageItem(item))
                    continue;

                if (item.Quality > 0)
                {
                    item.Quality = item.Quality - 1;
                }

                if (item.SellIn <= 0)
                {
                    if (item.Quality > 0)
                    {
                        item.Quality = item.Quality - 1;
                    }
                }

                item.SellIn = item.SellIn - 1;
            }
        }
    }
}
