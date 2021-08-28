using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrogContrl : MonoBehaviour
{
    public Animator anmia;
    public Collider2D colliderf;
    public Rigidbody2D rb;
    public LayerMask ground;
    public Vector2  facediraction;
    public int face = 1;
    public bool ChangeJ=true;

  
    public float hori;
    public float jumpf;
    
    // Start is called before the first frame update
    void Start()
    {
  
        facediraction = new Vector2(hori,jumpf);
       
    }

    // Update is called once per frame
    void Update()
    {
        RandJ();
        Fall();
        
    }

    void Jump()
    {
        //rb.AddForce(Vector2.up * jumpf * 100);
        rb.velocity = facediraction;
        anmia.SetBool("Jumping", true);
        anmia.SetBool("ToIdel", false);

    }

    void Fall()
    {
     
        if (anmia.GetBool("Jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anmia.SetBool("Jumping", false);
                anmia.SetBool("Faling", true);
                
            }

        }
        else if (colliderf.IsTouchingLayers(ground)&&!ChangeJ)
        {
            
            anmia.SetBool("Faling", false);
            anmia.SetBool("ToIdel", true);
            ChangeDirction();
            ChangeJ = true;

        }
    }


    void RandJ()
    {
        System.Random rd = new System.Random();
        int x = rd.Next(0, 99);
        //Debug.Log(x);

        FrogKill fk = this.gameObject.GetComponent<FrogKill>();
        

        if(x == 5&& colliderf.IsTouchingLayers(ground) &&fk.canmove)
        {
            Jump();
            ChangeJ = false;
        }

    }

    void ChangeDirction()
    {
        face *= -1;
        facediraction = new Vector2(face * hori, jumpf);
        transform.localScale = new Vector3(face, 1, 1);
    }
}
