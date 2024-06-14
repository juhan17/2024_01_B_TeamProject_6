using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneBeforeStory : MonoBehaviour
{
    public Text textUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textUI.text = "이게 뭐야?! 빙하가 다 녹았잖아? 이대로는 집으로 갈 수 없어... 방법을 찾아야해";
    }
}
