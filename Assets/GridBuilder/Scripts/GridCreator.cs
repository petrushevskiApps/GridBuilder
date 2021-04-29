using System;
using UnityEngine;
using UnityEngine.Events;

namespace Grid
{
    public class GridCreator : MonoBehaviour
    {
        public static UnityGridWorldEvent OnGridCreated = new UnityGridWorldEvent();

        [SerializeField] private GameObject element;
        [SerializeField] private int gridRows;
        [SerializeField] private int gridColumns;
        [SerializeField] private int gridDepth;

        [SerializeField] private Vector3 gridPadding = new Vector3(0f, 0f, 0f);

        public GridWorldSize worldSize;

        private Vector3 size;
        
        private GameObject[,,] gridElements;
        private ISpawner spawner;

        public GridCreator SetSpawner(ISpawner spawner)
        {
            this.spawner = spawner;
            spawner.SetElement(element)
                    .SetNaming("tile", "_", 0, NamingSort.INCREMENTAL)
                    .SetParent(this.gameObject.transform);

            return this;
        }

        public GridCreator SetSize(int rows, int columns, int depth)
        {
            gridRows = rows;
            gridColumns = columns;
            gridDepth = depth;

            return this;
        }

        public GridCreator SetPadding(Vector3 girdPadding)
        {
            this.gridPadding = girdPadding;

            return this;
        }

        public GameObject[,,] CreateGrid()
        {
            worldSize = new GridWorldSize();

            gridElements = new GameObject[gridRows, gridColumns, gridDepth];

            size = element.transform.localScale;

            Vector3 startPosition = GetPosition();

            worldSize.SetWorldSize(startPosition);

            for (int depth = 0; depth < gridDepth; depth++)
            {
                float z = startPosition.z - (depth * (size.z + gridPadding.z));
                worldSize.SetDepth(z);

                for (int row = 0; row < gridRows; row++)
                {
                    float y = startPosition.y - ((row * (size.y + gridPadding.y)));
                    worldSize.SetHeight(y);

                    for (int column = 0; column < gridColumns; column++)
                    {
                        float x = startPosition.x - ((column * (size.x + gridPadding.x)));
                        worldSize.SetWidth(x);
                        gridElements[row, column, depth] = spawner.SpawnElementAt(new Vector3(x,y,z));
                    }
                }
            }
            

            OnGridCreated.Invoke(worldSize);
            return gridElements;
        }

        private Vector3 GetPosition()
        {
            return new Vector3
            {
                x = CalculateStartPosition(gridColumns, size.x / 2f, gridPadding.x, WorldSide.LEFT),
                y = CalculateStartPosition(gridRows, size.y / 2f, gridPadding.y, WorldSide.TOP),
                z = CalculateStartPosition(gridDepth, size.z / 2f, gridPadding.z, WorldSide.FRONT)
            };
        }

        
        private float CalculateStartPosition(int totalElements, float extentSize,float padding, WorldSide side)
        {
            float startPosition = 0;

            int elements = (int) Math.Ceiling(totalElements / 2f);

            float halfExtendsCount = totalElements % 2 + 1;
            startPosition += halfExtendsCount * extentSize;
            startPosition += halfExtendsCount * (padding / 2);

            float fullExtentsCount = elements - halfExtendsCount;
            startPosition += fullExtentsCount * (extentSize * 2);
            startPosition += fullExtentsCount * padding;

            return startPosition * (int)side;
        }


        public class UnityGridWorldEvent : UnityEvent<GridWorldSize>
        {

        }
    }

    
}