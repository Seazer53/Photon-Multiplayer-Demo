using UnityEngine;

public static class Utils
{
    public static Vector3 GetRandomSpawnPoint()
    {
        return new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));
    }
}
