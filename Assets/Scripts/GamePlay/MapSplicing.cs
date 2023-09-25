using EnumData;
using MyTool;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MapSplicing : MonoBehaviour
{
    Transform cam;
    GameObject mapPrefab;
    Vector3 size;
    public Transform player;
    Dictionary<Vector3,GameObject> MapData = new ();

   
    public MapAddState Ad;

    public static MapSplicing ins;
    private void Awake()
    {
        ins = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    public void InitMap(string name,Vector3 pos=new Vector3(), MapAddState ma=MapAddState.Default)
    { 
        mapPrefab = GameRoot.Load<GameObject>($"Prefabs/Map/{name}");

        size= mapPrefab.GetComponent<Renderer>().bounds.size;
        //StartCoroutine(addMaps(pos));
        map(pos,ma);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void map(Vector3 v= new Vector3(),MapAddState ma = MapAddState.Default)
    {

        //if (MapData.ContainsKey(v)) return null;
        List<Vector3> list = new List<Vector3> ();

        Vector3[] pos = {new Vector3(-1,0,1), new Vector3(0,0,1), new Vector3(1,0,1),
                         new Vector3(-1,0,0),new Vector3(0,0,0),new Vector3(1,0,0),
                         new Vector3(-1,0,-1),new Vector3(0,0,-1),new Vector3(1,0,-1)};

        Vector3[] UD = {new Vector3(0,0,1), new Vector3(0,0,0), new Vector3(0,0,-1) };
        Vector3[] LF = { new Vector3(-1, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 0, 0) };

        switch (Ad)
        {
            case MapAddState.Default:
                list=pos.v3_list();
                break;
            case MapAddState.about:
                list= UD.v3_list();
                break;
            case MapAddState.Up_down:
                list= LF.v3_list();
                break;
            default:
                return;
        }

        Vector3[] pos1 = new Vector3[list.Count];
        for (int i = 0; i < pos1.Length; i++)
        {
            pos1[i] = v+V3multiplication(list[i],size);
            //print(pos1[i]);
        }
        //if (MapData.ContainsKey(pos1[pos1.Length / 2 + 1])) return;

        StartCoroutine(addMaps(pos1));
    }



    public IEnumerator addMaps(Vector3 pos)
    {
        if (!MapData.ContainsKey(pos)) {
            GameObject maps;
            yield return maps = Instantiate(mapPrefab, transform);
            maps.transform.position = pos;
            MapData.Add(pos, maps);
        }
    }
    public IEnumerator addMaps(Vector3[] pos)
    {
        for (int i = 0; i < pos.Length; i++)
        {
            //print($"{MapData.ContainsKey(pos[i])},{pos[i]}");
            if (MapData.ContainsKey(pos[i])) continue;
            if (!MapData.ContainsKey(pos[i]))
            {
                if (MapData.ContainsKey(pos[i])) continue;
                GameObject maps;
                yield return maps = Instantiate(mapPrefab, transform);
                maps.transform.position = pos[i];
                try
                {
                    MapData.Add(pos[i], maps);
                }
                catch (System.Exception)
                {
                    //print($"已存在{pos[i]}");
                    DestroyImmediate(maps.gameObject);
                }
            }
        }
    }
    public  Vector3 V3multiplication(Vector3 a,Vector3 b)
    { 
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
}
