using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.MonoBehaviour;

public class SimpleSpawner : ISpawner
{
    private Transform parent = null;
    private GameObject element = new GameObject();
    private Quaternion rotation = Quaternion.identity;

    private string baseName = "";
    private string nameDelimeter = "_";
    private int namingIndex = 0;
    private NamingSort namingSort = NamingSort.NONE;

    public ISpawner SetElement(GameObject element)
    {
        this.element = element;
        return this;
    }

    public ISpawner SetParent(Transform parent)
    {
        this.parent = parent;
        return this;
    }
    public ISpawner SetRotation(Quaternion rotation)
    {
        this.rotation = rotation;
        return this;
    }

    public ISpawner SetNaming(string baseName,string delimeter, int startIndex, NamingSort namingSort)
    {
        this.baseName = baseName;
        this.namingIndex = startIndex;
        this.namingSort = namingSort;
        this.nameDelimeter = delimeter;
        return this;
    }

    public T SpawnElementAt<T>(Vector3 position)
    {
        GameObject go = Object.Instantiate(element, position, rotation, parent);
        go.name = GetName();
        return go.GetComponent<T>();
    }

    public GameObject SpawnElementAt(Vector3 position)
    {
        GameObject go = Object.Instantiate(element, position, rotation, parent);
        go.name = GetName();
        return go;
    }

    private string GetName()
    {
        namingIndex += 1 * (int) namingSort;
        return $"{baseName}{nameDelimeter}{namingIndex}";
    }

}

public enum NamingSort
{
    NONE = 0,
    INCREMENTAL = 1,
    DECREMENTAL = -1,
}