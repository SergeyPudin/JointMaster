using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y < 0)
            Destroy(gameObject);
    }
}