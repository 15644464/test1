using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData : MonoBehaviour
{
    public Transform player;
    float cameraSize;
    void Start()
    {
        PlayGame.ins.isplay.AddListener((key) =>
        {
            if(key)
            StartCoroutine(MonsterGenerate(0.3f, 10));
        });
    }

    // Update is called once per frame
    void Update()
    {
    }
    /// <summary>
    /// 怪物生成
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    public IEnumerator MonsterGenerate(float speed,int num)
    {
        //while (PlayGame.ins.isplay)
        {
            for (int i = 0; i < num; i++)
            {
                var most = Instantiate(GameRoot.Load<GameObject>("Prefabs/0"), transform);
                most.transform.position = player.transform.position + randomPos(1);
                yield return new WaitForSeconds(speed);
            }
        }
        
    }

    public Vector3 randomPos(float pos)
    {
        float cam = Camera.main.orthographicSize / 2;
        float numx = Random.Range(pos, cam);
        float numy = Random.Range(pos, cam);
        float numx_ = Random.Range(0, 1f) > 0.5 ? 1 : -1;
        float numy_ = Random.Range(0, 1f) > 0.5 ? 1 : -1;

        Vector3 vector3 = new Vector3(numx* numx_, 0.5f,numy* numy_);
        return vector3;
    }
}
