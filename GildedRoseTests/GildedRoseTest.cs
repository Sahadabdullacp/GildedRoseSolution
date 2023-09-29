using GildedRoseKata;
using System.Collections.Generic;
using Xunit;
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
        public void conjuredIncreasesTwiceInQuality()

        {
            Item item = createAndUpdate(15, 25, GildedRose.CONJURED);
            Assert.Equal(23, item.Quality);
        }
        [Fact]

        public void conjuredSellInLessThanZeroQuality()
        {
            Item item = createAndUpdate(-1, 25, GildedRose.CONJURED);
            Assert.Equal(21, item.Quality);
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

        public void agedBrieQualityNeverMoreThanMaximum()

        {
            Item item = createAndUpdate(-2, 49, GildedRose.AGED_BRIE);
            Assert.Equal(50, item.Quality);
        }
        [Fact]

        public void backstagePassMaximumQuality()
        {
            Item item = createAndUpdate(10, 48, GildedRose.BACKSTAGE_PASS);
            Assert.Equal(GildedRose.MAXIMUM_QUALITY, item.Quality);
            item = createAndUpdate(4, 30, GildedRose.BACKSTAGE_PASS);
            Assert.Equal(33, item.Quality);
            item = createAndUpdate(0, 30, GildedRose.BACKSTAGE_PASS);
            Assert.Equal(0, item.Quality);
        }
        [Fact]

        public void backstagePassLessThanThreshold()

        {

            Item item = createAndUpdate(4, 30, GildedRose.BACKSTAGE_PASS);
            Assert.Equal(33, item.Quality);
            item = createAndUpdate(8, 30, GildedRose.BACKSTAGE_PASS);
            Assert.Equal(32, item.Quality);
            item = createAndUpdate(0, 30, GildedRose.BACKSTAGE_PASS);
            Assert.Equal(0, item.Quality);

        }

        [Fact]
        public void backstageSellInLessThanZero()
        {
            Item item = createAndUpdate(0, 30, GildedRose.BACKSTAGE_PASS);
            Assert.Equal(0, item.Quality);
        }
    }

}
