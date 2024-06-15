using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoBeforeStory : MonoBehaviour
{
    public Text textUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textUI.text = "빙하들이 녹으면서 거대한 절벽이 생긴것 같네. 어쩔 수 없지 그냥 올라가는 수 밖에.";
    }
}
