using CMSClone.Shared;

namespace CMSClone.Client.Features
{
    public class PagingRespone<T>
    {
        public List<T> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
}
