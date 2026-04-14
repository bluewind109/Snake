using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public static Snake Instance { get; private set; }

    public Vector2 direction = Vector2.right;
    private List<Transform> segments = new List<Transform>();

    private float moveTimer;
    public float speed = 100f;
    public float speedIncrease = 0.1f;
    public GameObject segmentPrefab;

    public bool IsMovingRight => direction == Vector2.right;
    public bool IsMovingLeft => direction == Vector2.left;
    public bool IsMovingUp => direction == Vector2.up;
    public bool IsMovingDown => direction == Vector2.down;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        Instance = null;
    }

    void Start()
    {
        segments.Add(this.transform);

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x),
            Mathf.Round(this.transform.position.y),
            0f
        );
    }

    public void Grow()
    {
        GameObject segment = Instantiate(segmentPrefab);
        segment.transform.position = segments[segments.Count - 1].position;
        segments.Add(segment.transform);
    }

    private void ResetState()
    {
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(this.transform);

        this.transform.position = Vector3.zero;
    }

    void Update()
    {
        if (!IsMovingDown && Input.GetKeyDown(KeyCode.W))
            direction = Vector2.up;
        else if (!IsMovingUp && Input.GetKeyDown(KeyCode.S))
            direction = Vector2.down;
        else if (!IsMovingRight && Input.GetKeyDown(KeyCode.A))
            direction = Vector2.left;
        else if (!IsMovingLeft && Input.GetKeyDown(KeyCode.D))
            direction = Vector2.right;
    }

    void FixedUpdate()
    {
        if (speed <= 0f)
        {
            return;
        }

        moveTimer += Time.fixedDeltaTime;
        float stepInterval = 1f / (speed * (1f + speedIncrease * (segments.Count - 1)));

        while (moveTimer >= stepInterval)
        {
            moveTimer -= stepInterval;

            for (int i = segments.Count - 1; i > 0; i--)
            {
                segments[i].position = segments[i - 1].position;
            }

            this.transform.position = new Vector3(
                Mathf.Round(this.transform.position.x) + direction.x,
                Mathf.Round(this.transform.position.y) + direction.y,
                0f
            );
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Food>() != null)
        {
            Grow();
        }
        else if (other.gameObject.GetComponent<Wall>() != null || 
            other.gameObject.GetComponent<SnakeSegment>() != null)
        {
            ResetState();
        }
    }
}
