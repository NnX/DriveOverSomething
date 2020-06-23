using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerContoller : BaseController
{
    public Transform bullet_StartPoint;
    public GameObject bullet_Prefab;
    public ParticleSystem shootFX;

    private Rigidbody someBody;
    private Animator shootSliderAnim;
    [HideInInspector]
    public bool canShoot;

    void Start()
    {
        someBody = GetComponent<Rigidbody>();

        shootSliderAnim = GameObject.Find("Fire Bar").GetComponent<Animator>();
        GameObject.Find("ShootButton").GetComponent<Button>().onClick.AddListener(ShootingControl); ;
        canShoot = true;
    }

    void Update()
    {
        ChangeRotation();
        ControlMovementWithKeyboard();
    }

    void FixedUpdate()
    {
        MoveTank();
    }

    void MoveTank ()
    {
        someBody.MovePosition(someBody.position + speed * Time.deltaTime);
    }

    void ControlMovementWithKeyboard()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        } else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        } else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            MoveFast();
        } else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            MoveSlow();
        }

        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            MoveStraight();
        } else if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            MoveStraight();
        } else if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            MoveNormal();
        } else if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            MoveNormal();
        }
    }

    void ChangeRotation()
    {
        if(speed.x > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, maxAngle, 0f), Time.deltaTime * rotationSpeed);
        } else if (speed.x < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, -maxAngle, 0f), Time.deltaTime * rotationSpeed);
        } else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * rotationSpeed);
        }
    }

    public void ShootingControl()
    {
        if(Time.timeScale != 0)
        {
            if(canShoot)
            {
                GameObject bullet = Instantiate(bullet_Prefab, bullet_StartPoint.position, Quaternion.identity);
                bullet.GetComponent<BulletScript>().Move(2000f);
                shootFX.Play();

                canShoot = false;

                shootSliderAnim.Play("Fill");
            }
        }
    }
}
