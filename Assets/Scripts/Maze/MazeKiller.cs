using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MazeKiller : MonoBehaviour 
{
    private new Collider2D collider2D;

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if(player != null)
        {
            player.Die(transform.position);
        }
    }
}
