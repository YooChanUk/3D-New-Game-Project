using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator _animator;
    Camera _camera;
    CharacterController _controller;

    public float speed;
    public float runSpeed;
    public float finalSpeed;
    public bool run;

    public bool toggleCameraRotation;

    public float smoothness;

    void Start()
    {
        _animator = this.GetComponent<Animator>();
        _camera = Camera.main;
        _controller = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            toggleCameraRotation = true; // 둘러보기 활성화
        }
        else
        {
            toggleCameraRotation = false; //둘러보기 비활성화
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }
        else
        {
            run = false;
        }

        InputMovement();
    }
    void LateUpdate()
    {
        if (!toggleCameraRotation)
        {
            Vector3 PlayerRotate = Vector3.Scale(_camera.transform.forward ,new Vector3(1,0,1));
            transform.rotation = Quaternion.Slerp(transform.rotation , Quaternion.LookRotation(PlayerRotate), Time.deltaTime * smoothness);
        }

    }

    void InputMovement()
    {
        finalSpeed = (run) ? runSpeed : speed;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        Vector3 moveDirection = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");

        _controller.Move(moveDirection.normalized * finalSpeed * Time.deltaTime);

        float percent = ((run) ? 0.5f : 0.15f) * moveDirection.magnitude;
        _animator.SetFloat("MoveA", percent, 0.1f, Time.deltaTime);
    }
}
