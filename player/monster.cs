using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class monster : MonoBehaviour
{
    //목적지
    public Transform target;
    //요원
    NavMeshAgent agent;

    public Animator anim;
    public AudioClip a;
    //파티클
    public ParticleSystem ps;

    enum State
    {
        Move,
        Moving
    }
    //상태 처리
    State state;

    private void Awake()
    {
        //네비변수 초기화
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false; //네비끄기
    }

    // Start is called before the first frame update
    void Start()
    {
        
        agent.enabled = true; //네비켜기

        //생성시 상태를 Move로 한다.
        state = State.Move;
        //요원을 정의해줘서
        agent = GetComponent<NavMeshAgent>();
        //생성될때 목적지(Player)를 찿는다.
        if (gameObject != null)
        {
            target = GameObject.Find("player").transform;
        }

        //요원에게 목적지를 알려준다.
        agent.destination = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Move)
        {
            UpdateMove();
        }
        else if (state == State.Moving)
        {
            UpdateMoving();
        }
        /*
        if (gameObject != null)
        {
            if (nav.destination != target.transform.position)
            {
                nav.SetDestination(target.transform.position);
            }
            else
            {
                nav.SetDestination(transform.position);
            }
        }
        */
    }
    private void UpdateMove()
    {
        agent.speed = 0;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > 2)
        {
            state = State.Moving;
            anim.SetTrigger("Moving");
        }

    }
    private void UpdateMoving()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 2)
        {
            state = State.Move;
            anim.SetTrigger("Move");
        }
        //타겟 방향으로 이동하다가
        agent.speed = 1.5f;
        //요원에게 목적지를 알려준다.
        agent.destination = target.transform.position;
    }
    private void OnCollisionEnter(Collision c)
    {
        if (gameObject != null)
        {
            if (c.gameObject.name == "player")
            {
                Destroy(this.gameObject, 1.0f);
                GetComponent<AudioSource>().PlayOneShot(a);
                ps.Play();
            }
        }
    }
}

