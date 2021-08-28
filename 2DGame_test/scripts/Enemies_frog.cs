using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_frog : Enemy
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public Transform leftpoint, rightpoint;
    private bool faceleft=true;
    public float speed,jumpforce;
    private float left_x, right_x;
    //private Animator anim;
    private Collider2D coll;
    public LayerMask ground;

    protected override void Start()
    {
        base.Start();
        rb=GetComponent<Rigidbody2D>();//获得刚体
        //anim=GetComponent<Animator>();//获得动画
        coll=GetComponent<Collider2D>();
        transform.DetachChildren();//左右点不会跟随移动
        left_x= leftpoint.position.x;//获得左右点坐标
        right_x=rightpoint.position.x;
        Destroy(leftpoint.gameObject);//删除多余
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
      SwitchAnim();
    }


    //移动函数
    void movement()
    {
         if(faceleft)
         {
             if(coll.IsTouchingLayers(ground))
             {
                 anim.SetBool("jumping", true);
                 rb.velocity= new Vector2(-speed, jumpforce);
             if(transform.position.x<left_x)
             {
                 rb.velocity= new Vector2(speed, jumpforce);
                 transform.localScale = new Vector3(-1, 1, 1);
                 faceleft=false;
             }

             }
         }else
         {
             if(coll.IsTouchingLayers(ground))
             {
                 anim.SetBool("jumping", true);
                 rb.velocity= new Vector2(speed, jumpforce);
             }
             if(transform.position.x>right_x)
             {
                 rb.velocity= new Vector2(-speed, jumpforce);
                 transform.localScale = new Vector3(1, 1, 1);
                 faceleft=true;
             }
         }
    }
    
    //动画函数
    void SwitchAnim()
    {
        if(anim.GetBool("jumping"))
        {
            if(rb.velocity.y<0.1)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }  
        }
        if(coll.IsTouchingLayers(ground) && anim.GetBool("falling"))
        {
            anim.SetBool("falling", false);
        }
    }

    void Death()
    {
        Destroy(gameObject);

    }

    public void JumpOn()
    {
        anim.SetTrigger("death");
    }

}
