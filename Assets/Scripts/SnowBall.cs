using System.Collections;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    public float speed = 1f;

    public int damage = 2;

    private Vector2 direction;

    private bool fromPlayer;

    public void Launch(Vector2 dir, bool fromPlayer)
    {
        direction = dir.normalized;
        this.fromPlayer = fromPlayer;
        Destroy(gameObject, 3);
    }

    //IEnumerator WaitForDestroySnowball()
    //{
    //    yield return new WaitForEndOfFrame();
    //}

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (fromPlayer && collision.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (!fromPlayer && collision.TryGetComponent(out Player player))
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
