using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests
{
    public class GildedRoseTest
    {
        [Theory]
        [InlineData("ItemNameToTest", "ItemNameToTest")]
        public void UpdateQuality_WhenUpdate_ItemNameIsUnaltered(string ItemName, string ExpectedNameAfterUpdate)
        {
            IList<Item> Items = new List<Item> { new Item { Name = ItemName, SellIn = 0, Quality = 0 } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(ExpectedNameAfterUpdate, Items[0].Name);
        }

        [Theory]
        [InlineData("TestItem", 10, 10)]
        [InlineData("TestItem", 1, 10)]
        public void UpdateQuality_WhenUpdate_QualityDegrade(string ItemName, int SellIn, int Quality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = ItemName, SellIn = SellIn, Quality = Quality } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(Quality - 1, Items[0].Quality);
        }

        [Theory]
        [InlineData("TestItem", 0, 10)]
        [InlineData("TestItem", -5, 10)]
        public void UpdateQuality_WhenSellPassed_QualityDegradeTwiceAsFast(string ItemName, int SellIn, int Quality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = ItemName, SellIn = SellIn, Quality = Quality } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(Quality  - 2, Items[0].Quality);
        }

        [Theory]
        [InlineData("TestItem", 10, 10, 100)]
        [InlineData("TestItem", 0, 10, 100)]
        [InlineData("TestItem", -5, 10, 100)]
        public void UpdateQuality_UpdateManyTimes_QualityIsNeverNegative(string ItemName, int SellIn, int Quality, int Iterations)
        {
            IList<Item> Items = new List<Item> { new Item { Name = ItemName, SellIn = SellIn, Quality = Quality } };
            GildedRose app = new(Items);
            for (int x = 0; x < Iterations; x++)
                app.UpdateQuality();
            Assert.True(Items[0].Quality >= 0);
        }

        [Theory]
        [InlineData("Aged Brie", 10, 10)]
        public void UpdateQuality_ForAgedBrie_QualityIncrease(string ItemName, int SellIn, int Quality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = ItemName, SellIn = SellIn, Quality = Quality } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.True(Items[0].Quality > Quality);
        }

        [Theory]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 10, 10)]
        public void UpdateQuality_ForBackstagePassesBeforeSellDate_QualityIncrease(string ItemName, int SellIn, int Quality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = ItemName, SellIn = SellIn, Quality = Quality } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.True(Items[0].Quality > Quality);
        }

        [Theory]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 0, 10)]
        public void UpdateQuality_ForBackstagePassesAfterSellDate_QualityDecrease(string ItemName, int SellIn, int Quality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = ItemName, SellIn = SellIn, Quality = Quality } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.True(Items[0].Quality < Quality);
        }

        [Theory]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 50, 10)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 30, 10)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 20, 10)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 11, 10)]
        public void UpdateQuality_ForBackstagePasses10DaysOrMore_QualityIncreaseBy1(string ItemName, int SellIn, int Quality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = ItemName, SellIn = SellIn, Quality = Quality } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(Quality + 1, Items[0].Quality);
        }

        [Theory]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 10, 10)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 9, 10)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 8, 10)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 7, 10)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 6, 10)]
        public void UpdateQuality_ForBackstagePassesBetween10And6_QualityIncreaseBy2(string ItemName, int SellIn, int Quality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = ItemName, SellIn = SellIn, Quality = Quality } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(Quality + 2, Items[0].Quality);
        }

        [Theory]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 5, 10)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 4, 10)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 3, 10)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 2, 10)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 1, 10)]
        public void UpdateQuality_ForBackstagePassesBetween5And1_QualityIncreaseBy3(string ItemName, int SellIn, int Quality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = ItemName, SellIn = SellIn, Quality = Quality } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(Quality + 3, Items[0].Quality);
        }

        [Theory]
        [InlineData("Aged Brie", 10, 49, 100)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 10, 49, 10)]
        public void UpdateQuality_UpdateManyTimes_QualityIsNeverAbove50(string ItemName, int SellIn, int Quality, int Iterations)
        {
            IList<Item> Items = new List<Item> { new Item { Name = ItemName, SellIn = SellIn, Quality = Quality } };
            GildedRose app = new(Items);
            for (int x = 0; x < Iterations; x++)
                app.UpdateQuality();
            Assert.True(Items[0].Quality > Quality);
            Assert.Equal(50, Items[0].Quality);
        }

        [Theory]
        [InlineData("Sulfuras, Hand of Ragnaros", 10, 80, 100)]
        public void UpdateQuality_UpdateManyTimes_SulfurasUnaltered(string ItemName, int SellIn, int Quality, int Iterations)
        {
            IList<Item> Items = new List<Item> { new Item { Name = ItemName, SellIn = SellIn, Quality = Quality } };
            GildedRose app = new(Items);
            for (int x = 0; x < Iterations; x++)
                app.UpdateQuality();
            Assert.Equal(SellIn, Items[0].SellIn);
            Assert.Equal(Quality, Items[0].Quality);
        }

        [Theory]
        [InlineData("Conjured", 10, 10)]
        public void UpdateQuality_ForConjuredItems_QualityDecreaseTwice(string ItemName, int SellIn, int Quality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = ItemName, SellIn = SellIn, Quality = Quality } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(Quality - 2, Items[0].Quality);
        }
    }
}
