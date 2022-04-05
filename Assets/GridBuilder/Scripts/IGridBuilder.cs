using UnityEngine;

namespace Grid
{
    public interface IGridBuilder
    {
        GridBuilder SetGridData(GridData data);
        GridBuilder SetElementSize(Vector3 elementSize);
        GridBuilder SetDimensions(int rows, int columns, int depth);
        GridBuilder SetPadding(Vector3 girdPadding);
        Vector3[,,] BuildGrid();
    }
}