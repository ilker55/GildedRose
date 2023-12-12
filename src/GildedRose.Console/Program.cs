using System.Collections.Generic;

namespace GildedRose.Console
{
    public class Program
    {
        private const string ItemAgedBrie = "Aged Brie";
        private const string ItemSulfuras = "Sulfuras, Hand of Ragnaros";
        private const string ItemBackstage = "Backstage passes to a TAFKAL80ETC concert";
        private const string ItemConjured = "Conjured Mana Cake";

        IList<Item> Items;
        private int currentDay = 1;

        static void Main(string[] args)
        {
            //System.Console.WriteLine("OMGHAI!");

            var app = new Program()
            {
                Items = new List<Item>
                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                              new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                          }

            };

            //app.UpdateQuality();

            //System.Console.ReadKey();

            app.StartDayProgression();
        }

        public void StartDayProgression()
        {
            // Set a maximum of 10 days for testing purposes.
            while (currentDay <= 10)
            {
                // Display current day and items
                System.Console.WriteLine($"Day {currentDay}");
                ShowAllItems();

                // Wait for next day
                System.Console.ReadKey();

                // Progress to next day
                UpdateQuality();
                currentDay++;
            }
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if (item.Name == ItemSulfuras)
                    continue;

                switch (item.Name)
                {
                    case ItemAgedBrie:
                    case ItemBackstage:
                        if (item.Quality >= 50)
                            break;

                        item.Quality++;

                        if (item.Quality < 50)
                        {
                            if (item.SellIn < 11)
                                item.Quality++;

                            if (item.SellIn < 6)
                                item.Quality++;
                        }
                        break;
                    default:
                        if (item.Quality > 0)
                            item.Quality -= ItemQualityDegradation(item);
                        break;
                }

                item.SellIn--;

                if (item.SellIn >= 0)
                    continue;

                switch (item.Name)
                {
                    case ItemAgedBrie:
                        if (item.Quality < 50)
                        {
                            item.Quality++;
                        }
                        break;
                    case ItemBackstage:
                        item.Quality -= item.Quality;
                        break;
                    default:
                        item.Quality -= ItemQualityDegradation(item);
                        break;
                }
            }
        }

        public void ShowAllItems()
        {
            System.Console.WriteLine("Items:");
            foreach (var item in Items)
            {
                System.Console.WriteLine($" - {item.Name} (SellIn: {item.SellIn}, Quality: {item.Quality})");
            }
        }

        public int ItemQualityDegradation(Item item) => item.Name == ItemConjured ? 2 : 1;

        public void SetItems(IList<Item> items) => Items = items;

        public Item GetItemByIndex(int index) => index < Items.Count ? Items[index] : null;
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
