using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptControl : MonoBehaviour
{    
    private CharacterController charc;
    public Vector3 moveVec;

    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        charc = GetComponent<CharacterController>();
        moveVec = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate() {
        moveVec.x = Input.GetAxis("Horizontal");
        moveVec.y = Input.GetAxis("Vertical");
        if(moveVec.magnitude>1.0)
        {
            moveVec.Normalize();
        }
        charc.SimpleMove(moveVec*speed*Time.deltaTime);
        //charc.Move(moveVec*speed*Time.deltaTime);
        
        

        if(Input.GetButtonDown("Jump"))
        {
            
        }
    }
}
