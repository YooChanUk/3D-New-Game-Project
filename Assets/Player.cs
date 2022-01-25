using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;

    public float speed;
    public float Aimspeed;

    float hAxis;
    float vAxis;
    float Aim;

    //Animator anim;
    Animator animator;

    Vector3 moveVec;

    void Awake()
    {
        animator = characterBody.GetComponent<Animator>();
        //anim = GetComponent<Animator>();
    }

    void Update()
    {
        LookAround();
        GetInput();
        Move();
        
    }

    void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        Aim = Input.GetAxis("Fire2");
    }

    void Move()
    {
        Debug.DrawRay(cameraArm.position,new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red);

        Vector2 moveInput = new Vector2(hAxis,vAxis);
        bool isMove = moveInput.magnitude != 0;
        bool isAim = Aim > 0;

        Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
        Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

        if (isAim)
        {
            characterBody.forward = lookForward;
            transform.position += moveDir * Aimspeed * Time.deltaTime;
        }
        else if (isMove)
        {
            characterBody.forward = moveDir;
            transform.position += moveDir * speed * Time.deltaTime;
        }

        animator.SetBool("Aim", isAim);

        if (isAim)
        {

            animator.SetBool("Front", vAxis > 0);
            animator.SetBool("Back", vAxis < 0);
            animator.SetBool("Left", hAxis < 0);
            animator.SetBool("Right", hAxis > 0);
        }
        else
        {
            animator.SetBool("Idle", !isMove);
            animator.SetBool("Run", isMove);
        }




    }

}
