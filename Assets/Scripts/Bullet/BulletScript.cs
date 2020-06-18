using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    [SerializeField]
    private Rigidbody someBody;


    public void Move(float speed)
    {
        someBody.AddForce(transform.forward.normalized * speed);
        Invoke("DeactivateGameObject", 5f);
    }

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            gameObject.SetActive(false);
        }
    }
}
