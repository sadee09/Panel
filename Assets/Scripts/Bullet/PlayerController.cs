using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Transform bulletSpawnPoint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullet();
        }

        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * (Time.deltaTime * speed * verticalInput));
    }

    void FireBullet()
    {


        GameObject bulletUI = ObjectPool.instance.GetObjectFromPool();
        bulletUI.transform.SetParent(GameObject.Find("Rocket").transform, false);

        if (bulletUI != null)
        {
            bulletUI.transform.position = bulletSpawnPoint.position;
            bulletUI.SetActive(true);
        }
    }
    
}