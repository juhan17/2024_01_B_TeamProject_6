using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CircleObject : MonoBehaviour
{

    public int index;                   //���� ��ȣ ���� 

    public float EndTime = 0.0f;                //���� �� �ð� üũ ����(float)
    public SpriteRenderer spriteRenderer;           //����� ��������Ʈ ���� ��ȯ ��Ű�� ���ؼ� ���� ����

    public GameManager gameManager;                     //���� �Ŵ��� ������
    public bool inCheck;

    void Awake()
    {
       
        spriteRenderer = GetComponent<SpriteRenderer>();    //������Ʈ�� �پ��ִ� ������Ʈ�� ����
        inCheck = false;
    }

    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();  //���� �Ŵ����� ���´�. 
    }

    void Update()
    {

    }

    public void OnTriggerStay(Collider collision)           //Trigger�� ������
    {
        if (collision.tag == "EndLine")                          //�浹���� ��ü�� Tag �� EndLine �� ���
        {
            EndTime += Time.deltaTime;                                      //������ �ð���ŭ ���� ���Ѽ� �ʸ� ī��� �Ѵ�. 

            if (EndTime > 1)                                                 //1�� �̻� �� ���
            {
                spriteRenderer.color = new Color(0.9f, 0.2f, 0.2f);         //������ ó�� 
            }
            if (EndTime > 3)                                                 //3�� �̻� �� ���
            {
                //Debug.Log("���� ����");                                     //�켱 ���� ���� ó�� 
                gameManager.EndGame();
            }
        }
    }

    public void OnCollisionEnter(Collision collision)                   //�ش� ������Ʈ�� �浹 ���� �� OnCollisionEnter
    {
        if (index >= 6)
            return;

        if (collision.gameObject.tag == "Fruit")                             //�浹�� ���� ������ ���
        {
            CircleObject temp = collision.gameObject.GetComponent<CircleObject>();      //�⵿�� ��ü���� ���� Class�� �޾ƿ´�. 

            if (temp.index == index)                                         //�浹 index�� �� index �� ����. 
            {
                if (gameObject.GetInstanceID() > collision.gameObject.GetInstanceID()) //2�� ���ļ� 1���� ����� ���ؼ� ID �˻� �� ū�͸� 
                {
                    //GameManager���� ��ģ ������Ʈ�� ���� 
                    gameManager.MergeObject(index, gameObject.transform.position);

                    Destroy(temp.gameObject);                                   //�浹�� ��ü ����
                    Destroy(gameObject);                                        //�ڽŵ� ���� 

                }
            }
        }
        
    }
}