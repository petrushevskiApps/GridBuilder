using UnityEngine;

public class SimpleSpawner : ISpawner
{
    private Transform _parent = null;
    private GameObject _element = new GameObject();
    private Quaternion _rotation = Quaternion.identity;

    private INameGenerator _nameGenerator;

    public ISpawner SetElement(GameObject element)
    {
        _element = element;
        return this;
    }

    public ISpawner SetParent(Transform parent)
    {
        _parent = parent;
        return this;
    }
    public ISpawner SetRotation(Quaternion rotation)
    {
        _rotation = rotation;
        return this;
    }

    public ISpawner SetNaming(INameGenerator nameGenerator)
    {
        _nameGenerator = nameGenerator;
        return this;
    }

    public T[,,] SpawnElements<T>(Vector3[,,] positions)
    {
        T[,,] elements = new T[positions.GetLength(0), positions.GetLength(1), positions.GetLength(2)];
        
        for (int k = 0; k < positions.GetLength(2); k++)
        {
            for (int j = 0; j < positions.GetLength(1); j++)
            {
                for (int i = 0; i < positions.GetLength(0); i++)
                {
                    elements[i,j,k] = SpawnElementAt<T>(positions[i,j,k]);
                }
            }
        }

        return elements;
    }

    public T SpawnElementAt<T>(Vector3 position)
    {
        GameObject go = Object.Instantiate(_element, position, _rotation, _parent);
        go.name = _nameGenerator.GetName();
        return go.GetComponent<T>();
    }

    public GameObject SpawnElementAt(Vector3 position)
    {
        GameObject go = Object.Instantiate(_element, position, _rotation, _parent);
        go.name = _nameGenerator.GetName();
        return go;
    }
}

