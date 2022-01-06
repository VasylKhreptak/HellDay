namespace Extensions
{
    public static class Vector2
    {
        public static UnityEngine.Vector2 RandomDirection01()
        {
            return new UnityEngine.Vector2(UnityEngine.Random.Range(-1f, 1f),
                UnityEngine.Random.Range(-1f, 1f));
        }

        public static UnityEngine.Vector2 RandomDirection1()
        {
            float x = UnityEngine.Random.Range(-1f, 1f);
            float y = Random.Sign() * UnityEngine.Mathf.Sqrt(1 - UnityEngine.Mathf.Pow(x, 2));
            
            return new UnityEngine.Vector2(x, y);
        }
    }
}
