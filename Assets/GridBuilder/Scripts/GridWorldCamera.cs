﻿using UnityEngine;

namespace Grid
{
    public class GridWorldCamera : MonoBehaviour
    {
        private Camera gridCamera;

        private void Awake()
        {
            GridCreator.OnGridCreated.AddListener(OnGridCreated);
            gridCamera = GetComponent<Camera>();
        }

        private void OnDestroy()
        {
            GridCreator.OnGridCreated.RemoveListener(OnGridCreated);
        }

        private void OnGridCreated(GridWorldSize worldSize)
        {
            if (gridCamera != null)
            {
                gridCamera.orthographicSize = worldSize.GetWorldSize().x + 3.5f;
            }
        }
    }

}

