using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DanwUI : MonoBehaviour
{
    public Toggle[] toggles ;
    public GameObject[] objs;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            var num = i;
            toggles[num].onValueChanged.AddListener((key) => {
                objs[num].SetActive(key);
            });
        }
        toggles[2].isOn=true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
