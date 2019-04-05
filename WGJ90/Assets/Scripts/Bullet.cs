using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime=1.0f;
    private float countTime;
    public Rigidbody rb;
    public Vector3 initVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(initVelocity,ForceMode.VelocityChange);
        
        GameObject.Destroy(this.gameObject,lifeTime);
    }

    private void OnCollisionEnter(Collision other) {

        //Debug.Log(other.gameObject.name);
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.gameObject.GetComponent<enimy1>().takeDmg(1.0f);
        }
        GameObject.Destroy(this.gameObject);
    }
    
}
