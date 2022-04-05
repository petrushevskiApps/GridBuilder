using Grid;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject elementPrefab;
    [SerializeField] private GridData gridData;
    public void Awake()
    {
        SimpleSpawner spawner = new SimpleSpawner();
        IGridBuilder gridBuilder = new GridBuilder();
        
        Vector3[,,] positions = gridBuilder
            .SetElementSize(elementPrefab.transform.localScale)
            .SetGridData(gridData)
            .BuildGrid();

        INameGenerator nameGenerator = new NameGenerator()
            .SetBaseName("tile")
            .SetIndex(100)
            .SetNameSort(NamingSort.DECREMENTAL);

        spawner
            .SetElement(elementPrefab)
            .SetNaming(nameGenerator)
            .SetParent(gameObject.transform)
            .SpawnElements<GameObject>(positions);
    }
}
