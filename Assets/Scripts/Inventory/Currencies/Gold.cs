using System;

namespace Inventory.Currencies
{
    public class Gold : Currency
    {
        public const string GoldId = "currency_gold";

        public Gold(int quantity)
        {
            id = GoldId;
            spriteId = GoldId;
            this.quantity = quantity;
        }
    }
}