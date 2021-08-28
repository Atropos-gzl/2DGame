using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAttack : MonoBehaviour
{

    public Vector3 v1;
    public Vector3 v2;
    public FXMove score;
    public bool getscore = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        getscore = false;
       // Debug.Log("attack");
        Transform t1 = this.gameObject.GetComponent<Transform>();
        v1 = this.transform.position;
        v2 = other.transform.position;
        Vector3 f = v2 - v1;
        Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
        if(rb!= null)
        {
            rb.velocity = f*10;
            score.SetScore(-20);
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        getscore = true;
    }
}
