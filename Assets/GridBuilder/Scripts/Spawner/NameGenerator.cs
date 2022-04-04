public class NameGenerator : INameGenerator
{
    private string name = "";
    private string delimeter = "";
    private int index = 0;

    private NamingSort sort = NamingSort.NONE;

    public NameGenerator SetBaseName(string baseName)
    {
        name = baseName;
        return this;
    }

    public NameGenerator SetDelimeter(string nameDelimeter)
    {
        delimeter = nameDelimeter;
        return this;
    }

    public NameGenerator SetIndex(int namingIndex)
    {
        index = namingIndex;
        return this;
    }
    public NameGenerator SetNameSort(NamingSort sortType)
    {
        sort = sortType;
        return this;
    }

    private string GetSortedIndex()
    {
        switch (sort)
        {
            case NamingSort.NONE:
                return "";
            case NamingSort.STATIC:
                return index.ToString();
            default:
                index += 1 * (int)sort;
                return index.ToString();
        }
    }
    public string GetName()
    {
        return $"{name}{delimeter}{GetSortedIndex()}";
    }
}
public enum NamingSort
{
    NONE = 0,
    INCREMENTAL = 1,
    DECREMENTAL = -1,
    STATIC = 2,
}