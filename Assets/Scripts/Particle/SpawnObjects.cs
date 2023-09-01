using UnityEngine;
public class SpawnObjects : MonoBehaviour
{
    public PoolingManager poolingManager;

    private void Start()
    {
        poolingManager = PoolingManager.Instance;
    }

    void FixedUpdate()
    {
        poolingManager.GetFromPool("Cube", transform.position, Quaternion.identity);
    }
}