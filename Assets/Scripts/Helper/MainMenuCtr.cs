using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCtr : MonoBehaviour
{

    public Animator cameraAnim;

    public void PlayGame()
    {
        cameraAnim.Play("Slide");
    }
}
