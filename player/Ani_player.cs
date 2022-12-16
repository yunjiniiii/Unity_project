using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ani_player : MonoBehaviour
{
    private Animator ani;
    float h, v;
    
    public Rigidbody rb;  //컴포넌트에서 리지드바디를 받아올 변수
    Vector3 moveVec;

    // Start is called before the first frame update
  
    private void Awake()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ani.SetTrigger("attack1");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ani.SetTrigger("attack2");
        }
        
        GetInput();
        Turn();
        Walk();
        Run();

        Vector3 dir = Vector3.right * h + Vector3.forward * v;

        dir = Camera.main.transform.TransformDirection(dir);

        dir.Normalize();

        // 3. 그 방향으로 이동한다.
      //  transform.position += dir * 10 * Time.deltaTime;
    }
   void GetInput()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
       
    }
    private void Walk()
    {
        moveVec = new Vector3(h, 0, v).normalized;
        transform.position += moveVec * 3 * Time.deltaTime;
        ani.SetBool("is Walk", moveVec != Vector3.zero);
    }
    private void Turn()
    {
        transform.LookAt(transform.position + moveVec); // 자연스럽게 회전
    }
    private void Run()
    {
        moveVec = new Vector3(h, 0, v).normalized;
        transform.position += moveVec * 3 * Time.deltaTime;
        ani.SetBool("is Run", moveVec != Vector3.zero);                         
    }
    
    public void PlaySE(AudioClip clip)
    {
        Manager.instance.manager_SE.GetComponent<AudioSource>().PlayOneShot(clip);
    }
    
}

