using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private MobileJoystick joystick;
    private Rigidbody2D rb;
    
    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = joystick.GetMoveVector() * moveSpeed * Time.deltaTime;
    }

}