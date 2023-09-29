using System;
using System.Collections.Generic;
namespace GildedRoseKata
{
    public class GildedRose
    {
        IList<Item> Items;
        public static string AGED_BRIE = "Aged Brie";
        public static string BACKSTAGE_PASS = "Backstage passes to a TAFKAL80ETC concert";
        public static string SULFURAS = "Sulfuras, Hand of Ragnaros";
        public static string CONJURED = "Conjured";
        public static int MAXIMUM_QUALITY = 50;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }
        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                Item item = Items[i];
                UpdateRegular(item);
                UpdateBackStage(item);
                UpdateAgedBrie(item);
                UpdateConjured(item);
                UpdateSulfuras(item);
            }
        }
        private void UpdateSulfuras(Item item)
        {
        }
        private void UpdateConjured(Item item)
        {
            if (isConjured(item))
                EditSellInandQuality(item, -2);
        }
        private void UpdateAgedBrie(Item item)
        {
            if (isAgedBrie(item))
                EditSellInandQuality(item, 1);
        }
        private void UpdateBackStage(Item item)
        {
            if (isBackStage(item))
                EditSellInandQuality(item, GetValueForBack(item));
        }
        private void UpdateRegular(Item item)
        {
            if (isRegular(item))
                EditSellInandQuality(item, -1);
        }
        private void EditSellInandQuality(Item item, int qualityValue)
        {
            item.SellIn--;
            if (qualityValue == 0)
                item.Quality = 0;
            item.Quality += qualityValue;
            if (item.SellIn < 0)
                item.Quality += qualityValue;
            item.Quality = Math.Clamp(item.Quality, 0, MAXIMUM_QUALITY);
        }
        private int GetValueForBack(Item item)
        {
            if (item.SellIn <= 0) return 0;
            if (item.SellIn < 6) return 3;
            if (item.SellIn < 11) return 2;
            return 1;
        }
        private bool isConjured(Item item) => item.Name == CONJURED;
        private bool isRegular(Item item) => !(isAgedBrie(item) || isBackStage(item) || isSulfuras(item) || isConjured(item));
        private bool isSulfuras(Item item) => item.Name == SULFURAS;
        private bool isBackStage(Item item) => item.Name == BACKSTAGE_PASS;
        private bool isAgedBrie(Item item) => item.Name == AGED_BRIE;
    }
}
