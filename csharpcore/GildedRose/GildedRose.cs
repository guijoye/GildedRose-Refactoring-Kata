using System.Collections.Generic;

namespace GildedRoseKata
{
    public class GildedRose
    {
        IList<Item> Items;
        private const string AgedBrieItemName = "Aged Brie";
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

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if (item.Name.Equals("Sulfuras, Hand of Ragnaros"))
                    continue;

                if (ProcessBrieItem(item))
                    continue;

                if (item.Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (item.Quality > 0)
                    {
                        item.Quality = item.Quality - 1;
                    }
                }
                else
                {
                    if (item.Quality < 50)
                    {
                        item.Quality = item.Quality + 1;

                        if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                        {
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
                    }
                }

                if (item.SellIn <= 0)
                {
                    if (item.Name != "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (item.Quality > 0)
                        {
                            item.Quality = item.Quality - 1;
                        }
                    }
                    else
                    {
                        item.Quality = item.Quality - item.Quality;
                    }
                }

                item.SellIn = item.SellIn - 1;
            }
        }
    }
}
