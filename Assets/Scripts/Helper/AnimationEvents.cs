using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEvents : MonoBehaviour
{
    private PlayerContoller playerContoller;
    private Animator anim;

    void Start()
    {
        playerContoller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerContoller>();
        anim = GetComponent<Animator>();

    }

    void ResetShooting()
    {
        playerContoller.canShoot = true;
        anim.Play("Idle");
    }

    void CameraStartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
