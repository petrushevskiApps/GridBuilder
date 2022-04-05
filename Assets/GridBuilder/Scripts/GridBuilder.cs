using System;
using UnityEngine;
using UnityEngine.Events;

namespace Grid
{
    /// <summary>
    /// Build element positions grid to be used for spawning
    /// elements in 3D Grid.
    /// </summary>
    public class GridBuilder : IGridBuilder
    {
        public static UnityGridWorldEvent OnGridCreated = new UnityGridWorldEvent();

        private Vector3 _elementSize;
        private int _gridRows;
        private int _gridColumns;
        private int _gridDepth;
        private Vector3 _elementPadding = new Vector3(0f, 0f, 0f);

        private GridWorldSize _worldSize;

        private Vector3[,,] _elementPositions;

        public GridBuilder SetGridData(GridData data)
        {
            _gridRows = data.GridDimension.Rows;
            _gridColumns = data.GridDimension.Columns;
            _gridDepth = data.GridDimension.Depth;
            _elementSize = data.ElementSize;
            _elementPadding = data.ElementPadding;

            return this;
        }

        public GridBuilder SetElementSize(Vector3 elementSize)
        {
            _elementSize = elementSize;
            return this;
        }

        public GridBuilder SetDimensions(int rows, int columns, int depth)
        {
            _gridRows = rows;
            _gridColumns = columns;
            _gridDepth = depth;
            return this;
        }

        public GridBuilder SetPadding(Vector3 girdPadding)
        {
            _elementPadding = girdPadding;
            return this;
        }

        public Vector3[,,] BuildGrid()
        {
            _worldSize = new GridWorldSize();

            _elementPositions = new Vector3[_gridRows, _gridColumns, _gridDepth];

            Vector3 startPosition = GetPosition();

            _worldSize.SetWorldSize(startPosition);

            for (int depth = 0; depth < _gridDepth; depth++)
            {
                float z = startPosition.z - depth * (_elementSize.z + _elementPadding.z);
                _worldSize.SetDepth(z);

                for (int row = 0; row < _gridRows; row++)
                {
                    float y = startPosition.y - row * (_elementSize.y + _elementPadding.y);
                    _worldSize.SetHeight(y);

                    for (int column = 0; column < _gridColumns; column++)
                    {
                        float x = startPosition.x - column * (_elementSize.x + _elementPadding.x);
                        _worldSize.SetWidth(x);
                        _elementPositions[row, column, depth] = new Vector3(x,y,z);
                    }
                }
            }
            

            OnGridCreated.Invoke(_worldSize);
            return _elementPositions;
        }

        private Vector3 GetPosition()
        {
            return new Vector3
            {
                x = CalculateStartPosition(_gridColumns, _elementSize.x / 2f, _elementPadding.x, WorldSide.LEFT),
                y = CalculateStartPosition(_gridRows, _elementSize.y / 2f, _elementPadding.y, WorldSide.TOP),
                z = CalculateStartPosition(_gridDepth, _elementSize.z / 2f, _elementPadding.z, WorldSide.FRONT)
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