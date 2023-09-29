using EnumData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent<bool> isplay;
    public static PlayGame ins;
    public MapSplicing map;
    public Button but_play, but_gameSpeed;
    public Text time_text, text_gameSpeed;
    int time;
    int gameSpeed=1;
    int maxSpeed = 3;

    private void Awake()
    {
        ins = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        isplay.AddListener((key) =>{
            if (map != null && key)
                map.InitMap("Plane");
            StartCoroutine(Timer());
        });
        but_play.onClick.AddListener(() =>{
            isplay.Invoke(true);
        });
        but_gameSpeed.onClick.AddListener(() =>
        {
            gameSpeed++;
            text_gameSpeed.text= $"X{gameSpeed}";
        });
    }

    public IEnumerator Timer()
    { 

        while (true)
        {
            yield return new WaitForSeconds(1f/ gameSpeed);
            print(gameSpeed);
            time_text.text = timeText(time++);
        }
    }

    public string timeText(int num)
    {
        var a = time % 60 < 10 ? '0' + (time % 60).ToString() : (time % 60).ToString();
        var b = time / 60 < 10 ? '0' + (time / 60).ToString() : (time / 60).ToString();
        string str =$"{b}:{a}";
        return str;
    }
}
