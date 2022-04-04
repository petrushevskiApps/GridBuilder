using Grid;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GridCreator gridCreator;
    [SerializeField] private GameObject elementPrefab;
    [SerializeField] private GridData gridData;
    public void Awake()
    {
        SimpleSpawner spawner = new SimpleSpawner();
        
        Vector3[,,] positions = gridCreator.SetElementSize(elementPrefab.transform.localScale).CreateGrid();

        INameGenerator nameGenerator = new NameGenerator()
            .SetBaseName("tile")
            .SetIndex(100)
            .SetNameSort(NamingSort.DECREMENTAL);

        spawner
            .SetElement(elementPrefab)
            .SetNaming(nameGenerator)
            .SetParent(gameObject.transform)
            .SpawnElements<Tile>(positions);
    }
}
