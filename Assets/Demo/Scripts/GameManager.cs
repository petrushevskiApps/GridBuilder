using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridCreator gridCreator;

    public void Awake()
    {
        gridCreator.CreateGrid<Tile>();
    }
}
