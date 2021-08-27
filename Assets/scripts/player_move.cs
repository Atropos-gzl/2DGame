using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_move : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jump;
    private Animator anim;
    public LayerMask ground;
    public Collider2D coll;
    public float JumpForce;
    public int gem;
    public Text Gemnumber;
    private bool isHurt;



    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        
    }
    
    //角色跳跃

    void Update()
   {
        if(!isHurt){
        if(Input.GetKeyDown(KeyCode.Space) && anim.GetBool("idle"))
        //if(Input.GetButtonDown("Jump"))
        {
            rb.velocity= new Vector2(rb.velocity.x, JumpForce);
            anim.SetBool("jumping", true);
        }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isHurt)
        {
        movement();
        }
        SwitchAnim();
        /*
        if(Input.GetKeyDown(KeyCode.Space))
        //if(Input.GetButtonDown("Jump"))
        {
            //Debug.Log("Jump");
            rb.velocity= new Vector2(rb.velocity.x, jump*Time.deltaTime);
            anim.SetBool("jumping", true);
        }
        */
    }

    void movement()
    {
        
        float lianfangxiang=Input.GetAxisRaw("Horizontal");
        float move=Input.GetAxis("Horizontal");
        //float tiao = Input.GetAxisRaw("Vertical");
        //角色跳跃
        // if(tiao == 1 )
        //角色移动
        if(move!=0)
        //if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
        {

            rb.velocity= new Vector2(move*speed*Time.deltaTime, rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(lianfangxiang));//调用动画
        }
        if(lianfangxiang!=0)
        {
            transform.localScale= new Vector3(lianfangxiang,1,1);
        }
       
    }
    
    //动画函数
    void SwitchAnim()
    {
        anim.SetBool("idle", false);
        if(rb.velocity.y<0.1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }
        if(anim.GetBool("jumping"))
        {
            if(rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }else if(isHurt)
            {
                anim.SetBool("hurt", true);
                anim.SetFloat("running", 0);
                if(Mathf.Abs(rb.velocity.x)<0.1f)
                {
                    anim.SetBool("hurt", false);
                    anim.SetBool("idle", true);
                    isHurt=false;
                }
            }
        else if(coll.IsTouchingLayers(ground))//如果碰撞到地面
        {
                anim.SetBool("falling", false);
                anim.SetBool("idle", true);
        }
    }

    //碰到可收集物
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.tag=="Collection")
        {
            Destroy(collision.gameObject);
            gem++;
            Gemnumber.text=gem.ToString();
        }
    } 

    //碰到敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Enemies")//如果碰到敌人
        {
               Enemies_frog frog= collision.gameObject.GetComponent<Enemies_frog>();
               if(anim.GetBool("falling"))//从上面碰撞，消灭敌人
               {
                   frog.JumpOn();
                   rb.velocity= new Vector2(rb.velocity.x, JumpForce);
                   anim.SetBool("jumping", true);
               }
               //从两侧碰撞
                else if(transform.position.x<collision.gameObject.transform.position.x)
               {
                   rb.velocity=new Vector2(-4, rb.velocity.y);
                   isHurt= true;
               }
                 else if(transform.position.x>collision.gameObject.transform.position.x)
               {
                   rb.velocity=new Vector2(4, rb.velocity.y);
                   isHurt= true;
               }
        }

    }
}






