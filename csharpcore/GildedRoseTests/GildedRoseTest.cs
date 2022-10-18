using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests
{
    public class GildedRoseTest
    {
        [Theory]
        [InlineData("ItemNameToTest", "ItemNameToTest")]
        public void UpdateQuality_CheckItemName_IsTheSameAfterUpdate(string ItemName, string ExpectedNameAfterUpdate)
        {
            IList<Item> Items = new List<Item> { new Item { Name = ItemName, SellIn = 0, Quality = 0 } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(ExpectedNameAfterUpdate, Items[0].Name);
        }

        [Theory]
        [InlineData("TestItem", 10, 10)]
        [InlineData("TestItem", 1, 10)]
        public void UpdateQuality_QualityDegrade_IsOk(string ItemName, int SellIn, int Quality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = ItemName, SellIn = SellIn, Quality = Quality } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(Quality - 1, Items[0].Quality);
        }

        [Theory]
        [InlineData("TestItem", 0, 10)]
        [InlineData("TestItem", -5, 10)]
        public void UpdateQuality_QualityDegradeTwiceAsFastWhenSellPassed_IsOk(string ItemName, int SellIn, int Quality)
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
    }
}
