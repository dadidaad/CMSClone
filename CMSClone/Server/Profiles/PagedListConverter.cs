using AutoMapper;
using CMSClone.Server.Models;
using CMSClone.Shared;
using CMSClone.Shared.Models;

namespace CMSClone.Server.Profiles
{
    public class PagedListConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>> where TSource : class where TDestination : class
    {
        public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
        {
            var collection = context.Mapper.Map<List<TSource>, List<TDestination>>(source);

            return new PagedList<TDestination>(collection, source.MetaData.TotalCount, source.MetaData.CurrentPage, source.MetaData.PageSize);
        }
    }
}
