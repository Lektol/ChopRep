using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AxeController : MonoBehaviour
{
    [SerializeField] private float dirX, dirZ;
    private bool CanRotate;
    private bool CanMove = true;
    [SerializeField] private float speed;
    [SerializeField] private Joystick joystick;
    private Rigidbody rb;
    private Animator _anim;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        EventManager.OnChangeGameState += ChangeState;
        EventManager.OnStopMovingPlayer += StopPlayer;
    }

    void OnDisable()
    {
        EventManager.OnChangeGameState -= ChangeState;
        EventManager.OnStopMovingPlayer -= StopPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanRotate)
        {
            transform.Rotate(0, InventorySystem.Instance.SpeedRotation * Time.deltaTime * 360, 0);
        } 
    }
    void ChangeState(EventManager.GameState gameState)
    {
        if (gameState != EventManager.GameState.Game && CanRotate)
        {
            _anim.SetTrigger("idleMenu");
            CanRotate = false;
        }
        else if(gameState == EventManager.GameState.Game)
        {
            _anim.SetTrigger("idlePlay");
            CanRotate = true;
        }
    }
    void FixedUpdate()
    {
        if (CanMove)
        {
            dirX = -joystick.Vertical * speed;
            dirZ = joystick.Horizontal * speed;
        }
        else
        {
            dirX = 0;
            dirZ = 0;
        }
        
        //dirX = 1 * speed;
        rb.velocity = new Vector3(dirX, 0, dirZ);
    }

    // public void TESTMETHOD()
    // {
    //     dirX = 1;
    // }

    // public void TESTMETHOD2()
    // {
    //     dirX = 0;
    // }
    private void StopPlayer(float sec)
    {
       StartCoroutine(StopPlayerCoroutine(sec)); 
    }
    private IEnumerator StopPlayerCoroutine(float sec)
    {
        CanMove = false;
        yield return new WaitForSeconds(sec);
        CanMove = true;
    }
}

