using GildedRose.Console;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {
        /// <summary>
        /// Requirement: Once the sell by date has passed, Quality degrades twice as fast
        /// </summary>
        [Fact]
        public void TestQualityPassedSellDate()
        {
            var app = new Program();
            app.SetItems(new List<Item>
            {
                new Item {Name = "Unit Test", SellIn = 1, Quality = 20}
            });

            // Quality degrades by 1
            app.UpdateQuality();
            Assert.Equal(0, app.GetItemByIndex(0)?.SellIn);
            Assert.Equal(19, app.GetItemByIndex(0)?.Quality);

            // Quality degrades by 2 (double) after SellIn days passed
            app.UpdateQuality();
            Assert.Equal(-1, app.GetItemByIndex(0)?.SellIn);
            Assert.Equal(17, app.GetItemByIndex(0)?.Quality);
        }

        /// <summary>
        /// Requirement: The Quality of an item is never negative
        /// </summary>
        [Fact]
        public void TestQualityNeverNegative()
        {
            var app = new Program();
            app.SetItems(new List<Item>
            {
                new Item {Name = "Unit Test", SellIn = 20, Quality = 1}
            });

            // Quality degrades by 1
            app.UpdateQuality();
            Assert.Equal(0, app.GetItemByIndex(0)?.Quality);

            // Quality does not degrade below 0
            app.UpdateQuality();
            Assert.Equal(0, app.GetItemByIndex(0)?.Quality);
        }

        /// <summary>
        /// Requirement: "Aged Brie" actually increases in Quality the older it gets
        /// </summary>
        [Fact]
        public void TestAgedBrieQualityIncrease()
        {
            var app = new Program();
            app.SetItems(new List<Item>
            {
                new Item {Name = "Aged Brie", SellIn = 20, Quality = 1}
            });

            // Quality increases by 1
            app.UpdateQuality();
            Assert.Equal(2, app.GetItemByIndex(0)?.Quality);

            // Quality increases by 1
            app.UpdateQuality();
            Assert.Equal(3, app.GetItemByIndex(0)?.Quality);
        }

        /// <summary>
        /// Requirement: The Quality of an item is never more than 50
        /// </summary>
        [Fact]
        public void TestQualityMaximumValue()
        {
            var app = new Program();
            app.SetItems(new List<Item>
            {
                new Item {Name = "Aged Brie", SellIn = 20, Quality = 49}
            });

            // Quality increases by 1
            app.UpdateQuality();
            Assert.Equal(50, app.GetItemByIndex(0)?.Quality);

            // Quality stays at 50
            app.UpdateQuality();
            Assert.Equal(50, app.GetItemByIndex(0)?.Quality);
        }

        /// <summary>
        /// Requirement: "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
        /// </summary>
        [Fact]
        public void TestSulfurasDegradation()
        {
            var app = new Program();
            app.SetItems(new List<Item>
            {
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 20, Quality = 80}
            });

            // SellIn and quality do not change
            app.UpdateQuality();
            Assert.Equal(20, app.GetItemByIndex(0)?.SellIn);
            Assert.Equal(80, app.GetItemByIndex(0)?.Quality);
        }

        /// <summary>
        /// Requirement: "Backstage passes", like aged brie, increases in Quality as it's SellIn value approaches;
        /// Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but Quality drops to 0 after the concert
        /// </summary>
        [Fact]
        public void TestBackstagePassesQualityIncrease()
        {
            var app = new Program();
            app.SetItems(new List<Item>
            {
                new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 11, Quality = 20}
            });

            // Quality increases by 1
            app.UpdateQuality();
            Assert.Equal(21, app.GetItemByIndex(0)?.Quality);

            // Fast forward 5 days
            for (int i = 1; i <= 5; i++)
            {
                // Quality increases by 2
                app.UpdateQuality();
                Assert.Equal(21 + i * 2, app.GetItemByIndex(0)?.Quality);
            }

            // Fast forward 5 days
            for (int i = 1; i <= 5; i++)
            {
                // Quality increases by 3
                app.UpdateQuality();
                Assert.Equal(31 + i * 3, app.GetItemByIndex(0)?.Quality);
            }
        }

        /// <summary>
        /// Requirement: "Conjured" items degrade in Quality twice as fast as normal items
        /// </summary>
        [Fact]
        public void TestConjuredQualityDegradation()
        {
            var app = new Program();
            app.SetItems(new List<Item>
            {
                new Item {Name = "Conjured Mana Cake", SellIn = 10, Quality = 20}
            });

            // Quality degrades by 2
            app.UpdateQuality();
            Assert.Equal(18, app.GetItemByIndex(0)?.Quality);

            // Quality degrades by 2
            app.UpdateQuality();
            Assert.Equal(16, app.GetItemByIndex(0)?.Quality);
        }
    }
}