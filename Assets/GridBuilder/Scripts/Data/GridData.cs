using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridData", menuName = "Grid/GridData", order = 1)]
public class GridData : ScriptableObject
{
    [SerializeField] private GridDimension gridDimension;
    [SerializeField] private Vector3 elementSize;
    [SerializeField] private Vector3 elementPadding;

    public GridDimension GridDimension => gridDimension;
    public Vector3 ElementSize => elementSize;
    public Vector3 ElementPadding => elementPadding;
}

[Serializable]
public class GridDimension
{
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private int depth;

    public int Rows => rows;
    public int Columns => columns;
    public int Depth => depth;
}