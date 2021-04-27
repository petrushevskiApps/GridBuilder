using UnityEngine;


namespace Grid
{
    public class GridWorldSize
    {
        public float LowXPosition  { get; private set; } = Mathf.Infinity;
        public float HighXPosition { get; private set; } = Mathf.NegativeInfinity;

        public float LowYPosition  { get; private set; } = Mathf.Infinity;
        public float HighYPosition { get; private set; } = Mathf.NegativeInfinity;
        
        public float LowZPosition  { get; private set; } = Mathf.Infinity;
        public float HighZPosition { get; private set; } = Mathf.NegativeInfinity;

        public Vector2 GetWorldSize()
        {
            float x = Mathf.Abs(LowXPosition) + Mathf.Abs(HighXPosition);
            float y = Mathf.Abs(LowYPosition) + Mathf.Abs(HighYPosition);
            float z = Mathf.Abs(LowZPosition) + Mathf.Abs(HighZPosition);

            return new Vector3(x, y, z);
        }

        public void SetXPosition(float value)
        {
            if (value < LowXPosition) LowXPosition = value;
            if (value > HighXPosition) HighXPosition = value;
        }

        public void SetYPosition(float value)
        {
            if (value < LowYPosition) LowYPosition = value;
            if (value > HighYPosition) HighYPosition = value;
        }
        
        public void SetZPosition(float value)
        {
            if (value < LowZPosition) LowZPosition = value;
            if (value > HighZPosition) HighZPosition = value;
        }

        
    }
}