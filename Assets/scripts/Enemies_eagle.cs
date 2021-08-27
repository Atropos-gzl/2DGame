using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_eagle : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public Transform toppoint, bottompoint;
    private float toppoint_y, bottompoint_y;
    private Collider2D coll;
    public LayerMask ground;
    public float speed;
    private bool isup=true;


    void Start()
    {
        rb=GetComponent<Rigidbody2D>();//获得刚体
        coll=GetComponent<Collider2D>();
        transform.DetachChildren();//左右点不会跟随移动
        toppoint_y= toppoint.position.y;//获得左右点坐标
        bottompoint_y=bottompoint.position.y;
        Destroy(toppoint.gameObject);//删除多余
        Destroy(bottompoint.gameObject);        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    //移动函数
    void movement()
    {
        if(isup)
        {
            rb.velocity= new Vector2(rb.velocity.x, speed);
            if(transform.position.y>toppoint_y)
            {
                isup=false;
            }
        }else
        {
            rb.velocity= new Vector2(rb.velocity.x, -speed);
            if(transform.position.y<bottompoint_y)
            {
                isup=true;
            }
        }
    }
}

