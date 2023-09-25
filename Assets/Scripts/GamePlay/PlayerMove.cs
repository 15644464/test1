using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Vector3 v1, v2;
    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    public void Move() {
        if (Input.GetMouseButtonDown(0))
        {
            if (v1!=Input.mousePosition)
            {
                v1= Input.mousePosition;
            }
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 vec = (Input.mousePosition - v1).normalized;
            transform.position = Vector3.Lerp(transform.position,transform.position+new Vector3(vec.x,0,vec.y),Time.deltaTime * 8);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.position != new Vector3(0, 0, 0)&&collision.transform.tag=="Respawn")
        MapSplicing.ins.map(new Vector3(collision.transform.position.x,0, collision.transform.position.z));
    }
    //private void OnCollisionStay(Collision collision)
    //{
    //    print(collision.transform.position);
    //}
}
