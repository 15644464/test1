using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform tran;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        tran = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move() {
        tran.position = new Vector3(player.position.x,tran.position.y,player.position.z);
    }
}
