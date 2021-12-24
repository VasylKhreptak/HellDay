namespace Extensions
{
    public static class Mathf
    {
        public static bool Approximately(float a, float b, float accuracy)
        {
            return UnityEngine.Mathf.Abs(a - b) < accuracy;
        }
    }
}


