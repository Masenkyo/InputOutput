using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Variables
    GameObject player;
    Rigidbody2D rb;
    [SerializeField] Vector3 speed = new Vector3(1, 0, 0);
    [SerializeField] float jumpForce;
    [SerializeField] float jumpStartTime;
    private float jumpTime;
    private bool isJumping;
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    #endregion Variables
    void Start()
    {
        player = GameObject.Find("player");
        rb = player.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        PlayerMovement();
        Jump();
    }
    #region MovementFunctions
    void PlayerMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space)) speed *= 2;
        else if (Input.GetKeyUp(KeyCode.Space)) speed /= 2;
        if (Input.GetKey(KeyCode.A)) player.transform.position -= speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) player.transform.position += speed * Time.deltaTime;
    }
    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded == true && Input.GetKeyDown(KeyCode.W)) {
            isJumping = true;
            jumpTime = jumpStartTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetKey(KeyCode.W) && isJumping == true) {
            if (jumpTime > 0) {
                rb.velocity = Vector2.up * jumpForce;
                jumpTime -= Time.deltaTime;
            }
            else isJumping = false;
        }
        if (Input.GetKeyUp(KeyCode.W)) isJumping = false;
        if (Input.GetKey(KeyCode.S) && isGrounded == false) rb.AddForce(new Vector2(0, -2));
    }
    #endregion MovementFunctions
}
