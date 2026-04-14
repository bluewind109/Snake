using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;

    void Start()
    {
        RandomizePosition();
    }

    private void RandomizePosition()
    {
        if (!gridArea) return;
        Bounds bounds = this.gridArea.bounds;

        float x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
        float y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));

        this.transform.position = new Vector3(x, y, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var snake = other.gameObject.GetComponent<Snake>();
        if (snake != null)
        {
            RandomizePosition();
        }
    }
}
