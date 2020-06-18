using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBlock : MonoBehaviour
{

    public Transform otherBlock;
    public float blockLength = 200f;
    private Transform player;
    private float endOffset = 10f;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        MoveGround();
    }

    void MoveGround()
    {
        if(transform.position.z + (blockLength / 2) < player.transform.position.z - endOffset)
        {
            transform.position = new Vector3(otherBlock.position.x, otherBlock.position.y, otherBlock.position.z + blockLength);
        }
    }
}
