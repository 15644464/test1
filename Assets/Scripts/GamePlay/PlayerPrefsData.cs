using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("user_name"))
        {
            print(PlayerPrefs.HasKey("user_name"));
        }
    }

    
}
