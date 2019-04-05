using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charcon2 : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    public Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    public float FireRate = 0.1f;
    private float fireNext;

    public GameObject bullet;
    public bool Rightface;
    private GameObject buletObj;
    private Bullet bulletScript;
    private Animator anime;
    float idleTime = 0.0f;
    
    Vector3 normalFace = new Vector3(1.0f,1.0f,1.0f);
    Vector3 invFace = new Vector3(-1.0f,1.0f,1.0f);
    private Transform firePoint;
    public float MaxHP = 10;
    private float HP = 10;
    public float skipFrameTime = 1.0f;
    private float skipFrameNext = 0.0f;
    private SpriteRenderer sr;
    public RectTransform HPBar;

    public GameObject GameOverObj;
    public GameObject GameWinObj;

    private AudioSource audios;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        firePoint = this.transform.GetChild(0);
        audios = GetComponent<AudioSource>();
        

        // let the gameObject fall down
       // gameObject.transform.position = new Vector3(0, 5, 0);
        anime = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        HP = MaxHP;
        moveDirection = Vector3.zero;
        Rightface = true;
    }



    void Update()
    {
        idleTime += Time.deltaTime;

#if false
      /*   if (controller.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection.x = new Vector3(Input.GetAxis("Horizontal"), 0.0f,0.0f);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }else
        {
           // moveDirection.x  += Input.GetAxis("Horizontal") *0.3f;
        }

        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
        */
#else
    moveDirection.x = Input.GetAxis("Horizontal");
    moveDirection.x = moveDirection.x * speed;

    if (Input.GetButton("Jump") && controller.isGrounded)
    {
        Debug.Log("Jump!!");
        moveDirection.y = jumpSpeed;
    }else if(controller.isGrounded)
    {
        
        moveDirection.y = 0.0f;
    
    }

    // Apply gravity
    moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
    

    controller.Move(moveDirection * Time.deltaTime);
#endif

        anime.SetFloat("Speed",controller.velocity.magnitude/2.0f);
        anime.SetBool("OnGround",controller.isGrounded);
        anime.SetFloat("IdleTime",idleTime);
        
        
        if(controller.velocity.magnitude > 0.01f)
        {
            idleTime = 0.0f;
        }

        if(moveDirection.x > 0.2f)
        {
            Rightface=true;
            this.transform.localScale = normalFace;
        }else if(moveDirection.x < -0.2f)
        {
            Rightface=false;
            this.transform.localScale = invFace;
        }
        anime.SetBool("RightFace",Rightface);
        
        
        if(Input.GetButtonDown("Fire1") && ( Time.time > fireNext))
        {
            idleTime = 0.0f;
            fireNext = Time.time+FireRate;
            anime.SetTrigger("Shoot");
        }
        
        if(Time.time > skipFrameNext)
        {
            sr.color = Global.normalColor;
        }

        HPBar.localScale = new Vector3(1.0f,HP/MaxHP,1.0f);
    }
    
     public void takeDmg(float dmg)
    {
        if(Time.time > skipFrameNext)
        {
            skipFrameNext = Time.time + skipFrameTime;
            this.HP -= dmg;

            if(this.HP <= 0.0f)
            {
                this.HP = 0.0f;
                GameOverObj.active = true;
                GameObject.Destroy(this.gameObject,0.2f);

            }else
            {
                //Time.timeScale = 0.2f;
                StartCoroutine("blink");
            }
        }
    }

    IEnumerator blink() 
    {
        
        sr.color = Global.takeDMGColor;
        yield return new WaitForSeconds(.05f);
        //Time.timeScale = 1.0f;

    }


    public void shoot()
    {
        if(Rightface)
        {
            buletObj = Instantiate(bullet,this.firePoint.position,Quaternion.identity);
        }else
        {
            buletObj = Instantiate(bullet,this.firePoint.position,Quaternion.identity);
            bulletScript = buletObj.GetComponent<Bullet>();
            bulletScript.initVelocity = bulletScript.initVelocity *-1.0f;
        }
        audios.pitch = (Random.Range(0.6f, .9f));
        audios.Play();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("WinPoint"))
        {
            Debug.Log("You Win");
            this.GameWinObj.active = true;
        }
    }
}
