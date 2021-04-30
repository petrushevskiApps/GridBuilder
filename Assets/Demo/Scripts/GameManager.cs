using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridCreator gridCreator;
    [SerializeField] private GameObject elementPrefab;
    [SerializeField] private GridData gridData;
    public void Awake()
    {
        SimpleSpawner spawner = new SimpleSpawner();

        

        //Vector3[,,] positions = gridCreator.SetElementSize(elementPrefab.transform.localScale)
        //                                    .SetDimensions(3,3,3)
        //                                    .SetPadding(new Vector3(0.2f, 0.2f, 0.2f))
        //                                    .CreateGrid();

        Vector3[,,] positions = gridCreator.SetElementSize(elementPrefab.transform.localScale).CreateGrid();

        INameGenerator nameGenerator = new NameGenerator().SetBaseName("tile")
            .SetIndex(100)
            .SetNameSort(NamingSort.DECREMENTAL);

        spawner.SetElement(elementPrefab)
            .SetNaming(nameGenerator)
            .SetParent(this.gameObject.transform)
            .SpawnElements<Tile>(positions);
    }
}
