using UnityEngine;

public class SnakeSegment : MonoBehaviour
{
    public BoxCollider2D _collider;

    public float invulnerableTimer;
    public float invulnerableDuration = 1.0f;

    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        invulnerableTimer = invulnerableDuration;
        _collider.enabled = false;
    }

    void Update()
    {
        if (invulnerableTimer > 0)
        {
            invulnerableTimer -= Time.deltaTime;
            if (invulnerableTimer <= 0)
            {
                _collider.enabled = true;
            }
        }
    }
}
