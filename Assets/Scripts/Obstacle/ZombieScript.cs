using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{

    public GameObject bloodFx;
    private float speed = 1f;

    private Rigidbody zombieBody;
    private bool isAlive;

    void Start()
    {
        zombieBody = GetComponent<Rigidbody>();

        speed = Random.Range(1, 5);
        isAlive = true;
    }

    void Update()
    {
        if(isAlive)
        {
            zombieBody.velocity = new Vector3(0f, 0f, -speed);
        }

        if (transform.position.y < -10f) {
            gameObject.SetActive(false);
        }        
    }

    void Die()
    {
        isAlive = false;
        zombieBody.velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        GetComponentInChildren<Animator>().Play("Idle");
        transform.rotation = Quaternion.Euler(90f, 0, 0f);
        transform.localScale = new Vector3(1f, 1f, 0.2f);
        transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
    }
    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet")
        {
            Instantiate(bloodFx, transform.position, Quaternion.identity);
            Invoke("DeactivateGameObject", 3f);

            GameplayController.instance.IncreaseScore();
            Die();
        }
    }
}
