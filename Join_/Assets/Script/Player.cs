using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rigid;
    Animator anim;
    [SerializeField] float speed;
    [SerializeField] Vector3 inputVec;
    [SerializeField] GameObject rotate;
    Vector3 mousePostion;

    bool mouseButton = false;
    public bool isMoved = false;
    [HideInInspector] public bool isStart = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Move();
    }

    private void Update() 
    {
        CheckStart();
        GetInput();
        Move();
    }

    private void CheckStart()
    {
        if (isStart)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            isStart = true;
            GameManager.instance.uIManager.readyUIOff();
        }
    }

    private void GetInput()
    {
        mouseButton = Input.GetMouseButton(0);
        if (Input.GetMouseButtonDown(0))
            mousePostion = Input.mousePosition;
    }

    private void Move()
    {
        if (!mouseButton)
        {
            isMoved = false;
            anim.SetBool("isMoved", isMoved);
            return;
        }
        
        isMoved = true;
        var drag = Input.mousePosition - mousePostion;
        if (drag.x > 80)
        {
            drag.x = 3;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 45, 0), 10 * Time.deltaTime);
        }
        else if (drag.x < -80)
        {
            drag.x = -3;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -45, 0), 10 * Time.deltaTime);
        }
        else
        {
            drag.x = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 10 * Time.deltaTime);
        }
        inputVec.x = drag.x;
        var nextVec = inputVec.normalized * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVec);

        anim.SetBool("isMoved", isMoved);
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.collider.gameObject.CompareTag("Man"))
        {
            other.gameObject.SetActive(false);
            GameObject _player = GameManager.instance.players.Get(0);
            _player.transform.position = other.transform.position;
        }
    }
}
