using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Movement : MonoBehaviour
{

    #region Variables
    public ArduinoSerialCommunication arduinoSerialCommunication;
    public GameObject scoree;
    GameObject Speed;
    GameObject Camera;
    GameObject player;
    Rigidbody2D rb;
    float speed = 1;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpStartTime;
    private float jumpTime;
    public bool isJumping;
    public bool isGrounded;
    public bool isSpeeding;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    float time;
    public int score = 0;
    public int highScore;
    float amp = 1;
    #endregion Variables
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", highScore);
        Speed = GameObject.Find("Speed");
        scoree = GameObject.Find("Score");
        Camera = GameObject.Find("Main Camera");
        arduinoSerialCommunication = Camera.GetComponent<ArduinoSerialCommunication>();
        player = GameObject.Find("player");
        rb = player.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        PlayerMovement();
        Jump();
        Score();
        time += Time.deltaTime * amp;
        PlayerPrefs.SetInt("highScore", highScore);
    }
    public void Score()
    {
        amp += Time.deltaTime / 10;
        score = (int)time / 10;
        scoree.GetComponent<TMP_Text>().text = "Score = " + score.ToString() + " HighScore = " + highScore.ToString();
        if (highScore < score)
        {
            highScore = score;
        }
    }
    #region MovementFunctions
    public void PlayerMovement()
    {
        Speed.GetComponent<TMP_Text>().text = "Speed = " + speed.ToString();
        speed += Time.deltaTime / 10;
        if (arduinoSerialCommunication.speeding == true && isSpeeding == false)
        {
            speed *= 2;
            isSpeeding = true;
        }
        if (arduinoSerialCommunication.speeding == false && isSpeeding == true)
        {
            speed /= 2;
            isSpeeding = false;
        }
        if (arduinoSerialCommunication.swapping == true) player.transform.position -= new Vector3(speed,0,0) * Time.deltaTime;
        if (arduinoSerialCommunication.swapping == false) player.transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
    }
    public void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded == true && arduinoSerialCommunication.jumping == true) {
            isJumping = true;
            jumpTime = jumpStartTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        if (arduinoSerialCommunication.jumping == true && isJumping == true) {
            if (jumpTime > 0) {
                rb.velocity = Vector2.up * jumpForce;
                jumpTime -= Time.deltaTime;
            }
            else isJumping = false;
        }
        if (arduinoSerialCommunication.jumping == false) isJumping = false;
        if (Input.GetKey(KeyCode.S) && isGrounded == false) rb.AddForce(new Vector2(0, -2));
    }
    #endregion MovementFunctions
}