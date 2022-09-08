namespace VacationPackageWebApi.Domain.Helpers;

public static class ListsHelper
{
    // source https://stackoverflow.com/a/4104601/17953724
    /// <summary>
    /// Splits a <see cref="List{T}"/> into multiple chunks.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list">The list to be chunked.</param>
    /// <param name="chunkSize">The size of each chunk.</param>
    /// <returns>A list of chunks.</returns>
    public static List<List<T>> SplitIntoChunks<T>(List<T> list, int chunkSize)
    {
        if (chunkSize <= 0)
        {
            throw new ArgumentException("chunkSize must be greater than 0.");
        }

        List<List<T>> retVal = new List<List<T>>();
        int index = 0;
        while (index < list.Count)
        {
            int count = list.Count - index > chunkSize ? chunkSize : list.Count - index;
            retVal.Add(list.GetRange(index, count));

            index += chunkSize;
        }

        return retVal;
    }

    public static IEnumerable<IEnumerable<T>> Chunk<T>(IEnumerable<T> list, int totalSize, int chunkSize) {
        int i = 0;
        while(i < totalSize) {
            yield return list.Skip(i).Take(chunkSize);
            i += chunkSize;
        }
    }
// convenience for "countable" lists
    private static IEnumerable<IEnumerable<T>> Chunk<T>(ICollection<T> list, int chunkSize) {
        return Chunk(list, list.Count, chunkSize);
    }
    private static IEnumerable<IEnumerable<T>> Chunk<T>(IEnumerable<T> list, int chunkSize) {
        return Chunk(list, list.Count(), chunkSize);
    }
}