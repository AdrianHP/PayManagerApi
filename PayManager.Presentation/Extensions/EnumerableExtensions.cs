using AutoMapper;

namespace PayManager.Presentation.Extensions
{
    public static class EnumerableExtensions
    {
        public async static Task<(IEnumerable<TR> Data, int Count)> Map<T, TR>(
            this Task<(IEnumerable<T>, int)> arg,
            IMapper mapper,
            Func<(T, TR), TR> after = null)
        {
            var (data, count) = await arg;
            var result = mapper.Map<IEnumerable<TR>>(data);
            if (after is not null)
                result = data.Zip(result).Select(after).ToList();
            return (result, count);
        }
    }
}
