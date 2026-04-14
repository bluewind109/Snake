using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    private float moveTimer;
    public float speed = 100f;

    void Start()
    {
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x),
            Mathf.Round(this.transform.position.y),
            0f
        );
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
        }
        else if  (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;
        }
    }

	void FixedUpdate()
	{
        if (speed <= 0f)
        {
            return;
        }

        moveTimer += Time.fixedDeltaTime;
        float stepInterval = 1f / speed;

        while (moveTimer >= stepInterval)
        {
            moveTimer -= stepInterval;
            this.transform.position = new Vector3(
                Mathf.Round(this.transform.position.x) + direction.x,
                Mathf.Round(this.transform.position.y) + direction.y,
                0f
            );
        }
	}
}
