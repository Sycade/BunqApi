using Sycade.BunqApi.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sycade.BunqApi.Collections
{
    public class BunqCollection<TItem> : IEnumerable<TItem>
        where TItem : BunqEntity
    {
        public Pagination Pagination { get; set; }
        internal IEnumerable<TItem> Items { get; set; }

        public BunqCollection(Pagination pagination, IEnumerable<BunqEntity> entities)
        {
            Pagination = pagination;

            Items = entities.OfType<TItem>();
        }


        public IEnumerator<TItem> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
