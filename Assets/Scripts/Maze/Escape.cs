using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Escape : MonoBehaviour 
{
    private new Collider2D collider2D;
    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
            player.Win();
    }
}
