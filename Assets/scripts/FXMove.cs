using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FXMove : MonoBehaviour
{

    public Rigidbody2D rb;
    public Animator anmia;
    public LayerMask Ground;
    public Collider2D collider;
    public Text number;
    public AudioSource jumpAS;

    public float speed;
    public float jumpf;
    public bool Canjump = true;

    private int score;
    
    
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void FixedUpdate()
    {
        //Move();
    }

    void Move()
    {
        float horizontalmove = Input.GetAxisRaw("Horizontal");
        float facedirctoin = Input.GetAxisRaw("Horizontal");
        Vector2 position = transform.position;

      //player move control
        if (horizontalmove != 0)
        {
            position.x += horizontalmove * speed * Time.deltaTime;
            transform.position = position;
          //  Debug.Log(Mathf.Abs(horizontalmove));
            anmia.SetFloat("Runing", Mathf.Abs(horizontalmove));
            
            //rb.velocity = new Vector2(horizontalmove * speed *Time.deltaTime, rb.velocity.y);
        }
        if (horizontalmove == 0)
        {
            
            anmia.SetFloat("Runing", 0);

            //rb.velocity = new Vector2(horizontalmove * speed *Time.deltaTime, rb.velocity.y);
        }

        if (facedirctoin != 0)
        {
            transform.localScale = new Vector3(facedirctoin, 1, 1);
        }
    }
      

    void Jump()
    {
        // player jump control
        if ((Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.K)) && Canjump )
        {
            rb.AddForce(Vector2.up*jumpf*100);
            anmia.SetBool("Jumping", true);
            Canjump = false;
            jumpAS.Play();
        }

        if (anmia.GetBool("Jumping"))
        {
            if(rb.velocity.y < 0)
            {
                anmia.SetBool("Jumping", false);
                anmia.SetBool("Falling", true);
            }
            
        }
        else if ( collider.IsTouchingLayers(Ground))
        {
            //Debug.Log(anmia.GetBool("Jumping"));
           // anmia.SetBool("Jumping", false);
            anmia.SetBool("Falling", false);
            anmia.SetBool("ToIdel",true);
            Canjump = true;
        }
    }

    public void SetScore(int x)
    {
        score += x;
       // Debug.Log(score);
        number.text = score.ToString();
    } 
}
