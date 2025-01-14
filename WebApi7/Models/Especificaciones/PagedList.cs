namespace WebApi7.Models.Especificaciones
{
    public class PagedList<T> : List<T>
    {
        public Metadata Metadata { get; set; }

        public PagedList(List<T> items, int  count, int pageNumber, int pageSize)
        {
            Metadata = new Metadata()
            {
                PageSize = pageSize,
                TotalCount = count,
                TotalPages  = (int)Math.Ceiling( count/ (double)pageSize ) //rendondeo.
            };
            AddRange( items );
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> entidad, int  pageNumber, int pageSize)
        {
            var count  = entidad.Count();
            var items = entidad.Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize );
        }
    }
}
