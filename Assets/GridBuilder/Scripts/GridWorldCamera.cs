using UnityEngine;

namespace Grid
{
    public class GridWorldCamera : MonoBehaviour
    {
        private Camera _gridCamera;

        private void Awake()
        {
            GridBuilder.OnGridCreated.AddListener(OnGridCreated);
            _gridCamera = GetComponent<Camera>();
        }

        private void OnDestroy()
        {
            GridBuilder.OnGridCreated.RemoveListener(OnGridCreated);
        }

        private void OnGridCreated(GridWorldSize worldSize)
        {
            if (_gridCamera != null)
            {
                _gridCamera.orthographicSize = worldSize.GetWorldSize().x + 3.5f;
            }
        }
    }

}

