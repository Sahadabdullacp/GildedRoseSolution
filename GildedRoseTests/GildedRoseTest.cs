using Xunit;
using System.Collections.Generic;
using GildedRoseKata;
using System;
namespace GildedRoseTests
{
    public class GildedRoseTest
    {
        private Item createAndUpdate(int sellIn, int quality, string name = "foo")
        {
            IList<Item> items = new List<Item> { new Item { Name = name, SellIn = sellIn, Quality = quality } };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            return items[0];
        }
        [Fact]
        public void testFrameworkWorks()
        {
            Item item = createAndUpdate(0, 0);
            Assert.Equal("foo", item.Name);
        }
        [Fact]
        public void systemLowersValues()
        {
            Item item = createAndUpdate(15, 25);
            Assert.Equal(14, item.SellIn);
            Assert.Equal(24, item.Quality);
        }
        [Fact]
        public void qualityDegradesTwiceAsFast()
        {
            Item item = createAndUpdate(0, 17);
            Assert.Equal(15, item.Quality);
        }
        [Fact]
        public void qaualityIsNeverNegative()
        {
            Item item = createAndUpdate(0, 0);
            Assert.Equal(0, item.Quality);
        }

        [Fact]
        public void agedBrieIncreasesInQuality()
        {
            Item item = createAndUpdate(15, 25, GildedRose.AGED_BRIE);
            Assert.Equal(26, item.Quality);
        }

        [Fact]
        public void agedBrieNeverExpires()
        {
            Item item = createAndUpdate(0, 42, GildedRose.AGED_BRIE);
            Assert.Equal(44, item.Quality);
            Assert.Equal(-1, item.SellIn);
        }
        [Fact]
        public void backstagePassMaximumQuality()
        {
            Item item = createAndUpdate(10, 48, GildedRose.BACKSTAGE_PASS);
            Assert.Equal(GildedRose.MAXIMUM_QUALITY, item.Quality);
            item = createAndUpdate(10, 49, GildedRose.BACKSTAGE_PASS);
            Assert.Equal(GildedRose.MAXIMUM_QUALITY, item.Quality);
        }

        [Fact]
        public void qualityNeverMoreThanMaximum()
        {
            Item item = createAndUpdate(10, 48, GildedRose.AGED_BRIE);
            Assert.Equal(49, item.Quality);
        }
    }
}
