using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enimy1 : MonoBehaviour
{
    // Start is called before the first frame update
    CharacterController cc;
    public float speed = 1.0f;
    public float HP = 4.0f;

    public GameObject bloodEffect;
    private Animator ac;
    private SpriteRenderer sr;

    public float DMG = 1.0f;
    private bool StartMove = false;
    void Start()
    {
        cc = GetComponent<CharacterController>();
        ac = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        this.transform.localScale =  new Vector3(-1.0f,1.0f,1.0f);
        ac.SetFloat("HP",this.HP);
    }

    // Update is called once per frame
    bool frontRight = true;
    void FixedUpdate() {
        if(StartMove)
        {
            if(sr.flipX)
            {
                cc.SimpleMove(this.transform.right * speed);
            }else
            {
                cc.SimpleMove(this.transform.right * -speed);
            }
            ac.SetFloat("HP",this.HP);
        }
    }

    public void takeDmg(float dmg)
    {
        this.HP -= dmg;
        Instantiate(bloodEffect,this.transform.position,Quaternion.identity);
        if(this.HP <= 0.0f)
        {
            GameObject.Destroy(this.gameObject,0.2f);
        }else
        {
           // Time.timeScale = 0.1f;
            StartCoroutine("blink");

        }
    }

    IEnumerator blink() 
    {
        
        sr.color = Global.takeDMGColor;
        yield return new WaitForSeconds(.05f);
        sr.color = Global.normalColor;
        yield return new WaitForSeconds(.05f);
        
       // Time.timeScale = 1.0f;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
     
        
        if(hit.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            Debug.Log(this.transform.localScale.x > 0.9f);
            sr.flipX = !sr.flipX;
        }else if(hit.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            hit.gameObject.GetComponent<charcon2>().takeDmg(this.DMG);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartMove = true;
        }
    }


    
}
