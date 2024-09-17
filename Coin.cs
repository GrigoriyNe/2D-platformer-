using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]

public class Coin : MonoBehaviour
{
    public event UnityAction<Coin> Changed;
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            Changed(this);
        }
    }


    public void Init(Vector2 start)
    {
        transform.position = start;
        transform.rotation = Quaternion.identity;
        
    }
}
