using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enimy2 : enimy1
{
    // Start is called before the first frame update
    public GameObject bullet;
    public Transform firePoint;
    protected void Start()
    {
        base.Start();
        firePoint = this.transform.GetChild(0);
        
    }

    public void shoot()
    {
        //Debug.Log("shoot");
        Instantiate(bullet,this.firePoint.position,Quaternion.identity);
    }

    // Update is called once per frame
    void FixedUpdate() {
        
        if(StartMove)
        {
            ac.SetBool("attack",true);
        }else
        {
            ac.SetBool("attack",false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartMove = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartMove = false;
        }
    }
}
