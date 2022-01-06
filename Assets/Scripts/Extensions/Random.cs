namespace Extensions
{
    public static class Random
    {
        public static int Sign()
        {
            return UnityEngine.Random.value < .5 ? -1 : 1;
        }
    }
}

