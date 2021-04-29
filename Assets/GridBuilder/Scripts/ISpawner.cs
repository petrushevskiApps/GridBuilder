using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawner
{
    ISpawner SetParent(Transform parent);
    ISpawner SetElement(GameObject element);
    ISpawner SetRotation(Quaternion rotation);
    ISpawner SetNaming(string baseName, string delimeter, int startIndex, NamingSort namingSort);
    T SpawnElementAt<T>(Vector3 position);
    GameObject SpawnElementAt(Vector3 position);
}
