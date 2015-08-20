using System.Collections.Generic;
using System.Linq;

namespace AbsorbCodingChallenge
{
    public static class ScannedItemsDeserializer
    {
        public static IList<ScannedItem> Create(string[] items)
        {
            return items.Select(c => new ScannedItem
            {
                Name = c
            }).ToList();
        }
    }
}