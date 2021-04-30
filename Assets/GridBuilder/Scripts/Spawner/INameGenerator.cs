public interface INameGenerator
{
    NameGenerator SetBaseName(string baseName);
    NameGenerator SetDelimeter(string nameDelimeter);
    NameGenerator SetIndex(int namingIndex);
    NameGenerator SetNameSort(NamingSort sortType);
    string GetName();
}