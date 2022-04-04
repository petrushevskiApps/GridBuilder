using System;
using UnityEngine;
using UnityEngine.Events;

namespace Grid
{
    public class GridCreator : MonoBehaviour
    {
        public static UnityGridWorldEvent OnGridCreated = new UnityGridWorldEvent();

        [SerializeField] private GridData gridData;

        private Vector3 elementSize;
        private int gridRows;
        private int gridColumns;
        private int gridDepth;
        private Vector3 elementPadding = new Vector3(0f, 0f, 0f);

        public GridWorldSize worldSize;

        private Vector3[,,] gridPositions;

        private void Awake()
        {
            SetGridData(gridData);
        }

        public GridCreator SetGridData(GridData data)
        {
            gridRows = data.GridDimension.Rows;
            gridColumns = data.GridDimension.Columns;
            gridDepth = data.GridDimension.Depth;
            elementSize = data.ElementSize;
            elementPadding = data.ElementPadding;

            return this;
        }

        public GridCreator SetElementSize(Vector3 elementSize)
        {
            this.elementSize = elementSize;
            return this;
        }

        public GridCreator SetDimensions(int rows, int columns, int depth)
        {
            gridRows = rows;
            gridColumns = columns;
            gridDepth = depth;
            return this;
        }

        public GridCreator SetPadding(Vector3 girdPadding)
        {
            elementPadding = girdPadding;
            return this;
        }

        public Vector3[,,] CreateGrid()
        {
            worldSize = new GridWorldSize();

            gridPositions = new Vector3[gridRows, gridColumns, gridDepth];

            Vector3 startPosition = GetPosition();

            worldSize.SetWorldSize(startPosition);

            for (int depth = 0; depth < gridDepth; depth++)
            {
                float z = startPosition.z - (depth * (elementSize.z + elementPadding.z));
                worldSize.SetDepth(z);

                for (int row = 0; row < gridRows; row++)
                {
                    float y = startPosition.y - ((row * (elementSize.y + elementPadding.y)));
                    worldSize.SetHeight(y);

                    for (int column = 0; column < gridColumns; column++)
                    {
                        float x = startPosition.x - ((column * (elementSize.x + elementPadding.x)));
                        worldSize.SetWidth(x);
                        gridPositions[row, column, depth] = new Vector3(x,y,z);
                    }
                }
            }
            

            OnGridCreated.Invoke(worldSize);
            return gridPositions;
        }

        private Vector3 GetPosition()
        {
            return new Vector3
            {
                x = CalculateStartPosition(gridColumns, elementSize.x / 2f, elementPadding.x, WorldSide.LEFT),
                y = CalculateStartPosition(gridRows, elementSize.y / 2f, elementPadding.y, WorldSide.TOP),
                z = CalculateStartPosition(gridDepth, elementSize.z / 2f, elementPadding.z, WorldSide.FRONT)
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


        public class UnityGridWorldEvent : UnityEvent<GridWorldSize> { }
    }

    
}