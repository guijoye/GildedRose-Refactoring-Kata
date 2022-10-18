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
    }
}
