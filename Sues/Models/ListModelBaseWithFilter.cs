namespace Sues.Models
{
    using System.Collections.Generic;

    public class ListModelBaseWithFilter<TItem, TFilter>
    {
        public IEnumerable<TItem> Items { get; set; }
        public TFilter Filter { get; set; }
    }
}