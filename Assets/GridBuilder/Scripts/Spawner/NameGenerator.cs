public class NameGenerator : INameGenerator
{
    private string _name = "";
    private string _delimeter = "";
    private int _index = 0;

    private NamingSort sort = NamingSort.NONE;

    public NameGenerator SetBaseName(string baseName)
    {
        _name = baseName;
        return this;
    }

    public NameGenerator SetDelimeter(string nameDelimeter)
    {
        _delimeter = nameDelimeter;
        return this;
    }

    public NameGenerator SetIndex(int namingIndex)
    {
        _index = namingIndex;
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
                return _index.ToString();
            default:
                _index += 1 * (int)sort;
                return _index.ToString();
        }
    }
    public string GetName()
    {
        return $"{_name}{_delimeter}{GetSortedIndex()}";
    }
}
public enum NamingSort
{
    NONE = 0,
    INCREMENTAL = 1,
    DECREMENTAL = -1,
    STATIC = 2,
}