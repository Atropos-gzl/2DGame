using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogKill : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anmia;
    public bool timerBegin = false;
    public bool canmove = true;
    public float timer = 0;

    public Collider2D collider ;
    public LayerMask ground;
    public AudioSource bang;
    public FXMove fx;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyThis();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (anmia.GetBool("ToIdel")&&collider.IsTouchingLayers(ground) )
        {

            anmia.SetBool("Dead", true);
             fx = other.gameObject.GetComponent<FXMove>();
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, 8);
            //fx.SetScore(100);
            timerBegin = true;
            bang.Play();
           // Destroy(this.GetComponentInParent<Collider2D>());
            
            //Destroy(this.GetComponent<Rigidbody2D>());
            canmove = false;
            //DestroyThis();
        }
 
    }
    void DestroyThis()
    {
        if (timer > 0.7)
        {
            Destroy(gameObject);
            fx.SetScore(100);
        }
        if (timerBegin)
            timer += Time.deltaTime;
    }


}
