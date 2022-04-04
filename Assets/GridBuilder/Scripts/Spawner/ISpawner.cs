using UnityEngine;

public interface ISpawner
{
    ISpawner SetParent(Transform parent);
    ISpawner SetElement(GameObject element);
    ISpawner SetRotation(Quaternion rotation);
    ISpawner SetNaming(INameGenerator nameGenerator);
    T SpawnElementAt<T>(Vector3 position);
    GameObject SpawnElementAt(Vector3 position);

    T[,,] SpawnElements<T>(Vector3[,,] positions);
}
