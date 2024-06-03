using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CircleObject : MonoBehaviour
{

    public int index;                   //과일 번호 설정 

    public float EndTime = 0.0f;                //종료 선 시간 체크 변수(float)
    public SpriteRenderer spriteRenderer;           //종료시 스프라이트 색을 변환 시키기 위해서 접근 선언

    public GameManager gameManager;                     //게임 매니저 참조용
    public bool inCheck;

    void Awake()
    {
       
        spriteRenderer = GetComponent<SpriteRenderer>();    //오브젝트에 붙어있는 컴포넌트에 접근
        inCheck = false;
    }

    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();  //게임 매니저를 얻어온다. 
    }

    void Update()
    {

    }

    public void OnTriggerStay(Collider collision)           //Trigger에 있을때
    {
        if (collision.tag == "EndLine")                          //충돌중인 물체의 Tag 가 EndLine 일 경우
        {
            EndTime += Time.deltaTime;                                      //프레임 시간만큼 누적 시켜서 초를 카운드 한다. 

            if (EndTime > 1)                                                 //1초 이상 일 경우
            {
                spriteRenderer.color = new Color(0.9f, 0.2f, 0.2f);         //빨강색 처리 
            }
            if (EndTime > 3)                                                 //3초 이상 일 경우
            {
                //Debug.Log("게임 종료");                                     //우선 게임 종료 처리 
                gameManager.EndGame();
            }
        }
    }

    public void OnCollisionEnter(Collision collision)                   //해당 오브젝트가 충돌 했을 때 OnCollisionEnter
    {
        if (index >= 6)
            return;

        if (collision.gameObject.tag == "Fruit")                             //충돌이 같은 과일일 경우
        {
            CircleObject temp = collision.gameObject.GetComponent<CircleObject>();      //출동한 물체에서 같은 Class를 받아온다. 

            if (temp.index == index)                                         //충돌 index와 내 index 가 같다. 
            {
                if (gameObject.GetInstanceID() > collision.gameObject.GetInstanceID()) //2개 합쳐서 1개를 만들기 위해서 ID 검사 후 큰것만 
                {
                    //GameManager에서 합친 오브젝트를 생성 
                    gameManager.MergeObject(index, gameObject.transform.position);

                    Destroy(temp.gameObject);                                   //충돌한 물체 제거
                    Destroy(gameObject);                                        //자신도 제거 

                }
            }
        }
        
    }
}