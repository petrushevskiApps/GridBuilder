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
        [SerializeField] private Vector3 padding = new Vector3(0f, 0f, 0f);

        public GridWorldSize worldSize;

        private Vector3 size;

        public T[,] CreateGrid<T>()
        {
            worldSize = new GridWorldSize();
            T[,] gridElements = new T[gridRows,gridColumns];

            size = element.transform.localScale;

            Vector2 startPosition = GetPosition();

            worldSize.SetXPosition(startPosition.x);
            worldSize.SetYPosition(startPosition.y);

            for (int row = 0; row < gridRows; row++)
            {
                float y = startPosition.y - ((row * (size.y + padding.y)));
                worldSize.SetYPosition(y);

                for (int column = 0; column < gridColumns; column++)
                {
                    float x = startPosition.x - ((column * (size.x + padding.x)));
                    worldSize.SetXPosition(x);
                    gridElements[row, column] = CreateElementAt<T>(x, y);
                }
            }

            OnGridCreated.Invoke(worldSize);
            return gridElements;
        }

        private Vector2 GetPosition()
        {
            return new Vector2
            {
                x = CalculateStartPosition(gridRows, size.x / 2f, padding.x, WorldSide.LEFT),
                y = CalculateStartPosition(gridColumns, size.y / 2f, padding.y, WorldSide.TOP)
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

        private T CreateElementAt<T>(float x, float y)
        {
            Vector3 position = new Vector3(x, y, 0);
            GameObject go = Instantiate(element, position, Quaternion.identity, transform);
            return go.GetComponent<T>();
        }


        public class UnityGridWorldEvent : UnityEvent<GridWorldSize>
        {

        }
    }

    
}