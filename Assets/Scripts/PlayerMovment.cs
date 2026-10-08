using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D rb;

    [HideInInspector]
    public Vector2 moveDir;




    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        InputManagmant();
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    void InputManagmant()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(x, y).normalized;
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed); 
    }
}
