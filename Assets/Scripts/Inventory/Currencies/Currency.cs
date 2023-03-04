using System;

namespace Inventory.Currencies
{
    [Serializable]
    public abstract class Currency
    {
        public string id;
        public string spriteId;
        public int quantity;
    }
}