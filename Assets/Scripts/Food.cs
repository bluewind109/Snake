using UnityEngine;

public class Food : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        var snake = other.gameObject.GetComponent<Snake>();
        if (snake != null)
        {
            Destroy(this.gameObject);
        }
    }
}
