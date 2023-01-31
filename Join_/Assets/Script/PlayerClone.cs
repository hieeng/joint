using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClone : MonoBehaviour
{
    [SerializeField] Vector3 box;
    Rigidbody rigid;
    Animator anim;
    int layerMan;
    int layerWall;

    private void Awake() 
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable() 
    {
        layerMan = LayerMask.GetMask("Man");
        layerWall = LayerMask.GetMask("Wall");
    }

    private void Start() 
    {
        anim.SetBool("isMoved", true);
    }

    private void Update() 
    {
        rigid.velocity = Vector3.zero;
        Move();
        Check();
    }

    private void Move()
    {
        if (GameManager.instance.player.isMoved)
        {
            anim.SetBool("isMoved", true);
            gameObject.transform.rotation = GameManager.instance.player.transform.rotation;
        }
        else
            anim.SetBool("isMoved", false);
    }

    private void Check()
    {
        Collider[] cols = Physics.OverlapBox(transform.position, box * 0.5f, transform.rotation ,layerMan);

        for (int i = 0; i < cols.Length; i++)
        {
            cols[i].gameObject.SetActive(false);
            GameObject _player = GameManager.instance.players.Get(0);
            _player.transform.position = cols[i].transform.position;
        }

    }

    private void OnCollisionEnter(Collision other)
    {
 /*        if (other.gameObject.CompareTag("Man"))
        {
            other.gameObject.SetActive(false);
            GameObject _player = GameManager.instance.players.Get(0);
            _player.transform.position = other.transform.position;
        } */
        if (other.gameObject.CompareTag("Wall"))
            gameObject.SetActive(false);
    }
}
