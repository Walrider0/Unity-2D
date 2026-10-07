using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int hp = 10;
    public GameObject snowBall;
    public float throwInterval = 2f;
    private Player player;
    private float throwTimer;
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        throwTimer = throwInterval;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void EnemyMovement()
    {
        if (player == null) return;
        //float distance = 

    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"Taken {damage} amount of damage");
        if ( hp <= 0 )
        {
            Debug.LogError("Died");
            Destroy(gameObject);
        }
    }
}
