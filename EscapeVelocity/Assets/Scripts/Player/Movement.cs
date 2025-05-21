using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb1;
    private Rigidbody2D rb2;

    private Vector2 input1;
    private Vector2 input2;

    void Start()
    {
        rb1 = player1.GetComponent<Rigidbody2D>();
        rb2 = player2.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input1.x = Input.GetAxisRaw("P1_Horizontal");
        input1.y = Input.GetAxisRaw("P1_Vertical");
        input1.Normalize();

        input2.x = Input.GetAxisRaw("P2_Horizontal");
        input2.y = Input.GetAxisRaw("P2_Vertical");
        input2.Normalize();
    }

    void FixedUpdate()
    {
        rb1.velocity = input1 * speed;
        rb2.velocity = input2 * speed;
    }
}