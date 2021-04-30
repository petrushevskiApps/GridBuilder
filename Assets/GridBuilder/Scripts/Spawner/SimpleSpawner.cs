using UnityEngine;

public class SimpleSpawner : ISpawner
{
    private Transform parent = null;
    private GameObject element = new GameObject();
    private Quaternion rotation = Quaternion.identity;

    private INameGenerator nameGenerator;

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

    public ISpawner SetNaming(INameGenerator nameGenerator)
    {
        this.nameGenerator = nameGenerator;
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
        GameObject go = Object.Instantiate(element, position, rotation, parent);
        go.name = nameGenerator.GetName();
        return go.GetComponent<T>();
    }

    public GameObject SpawnElementAt(Vector3 position)
    {
        GameObject go = Object.Instantiate(element, position, rotation, parent);
        go.name = nameGenerator.GetName();
        return go;
    }

    

}

