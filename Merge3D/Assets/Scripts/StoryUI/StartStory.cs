using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartStory : MonoBehaviour
{
    public Text textUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textUI.text = "펭눈은 오늘도 여느때나 다름없이 물고기를 한가득 사냥한 다음 집으로 돌아가고 있었다.";
    }
}
