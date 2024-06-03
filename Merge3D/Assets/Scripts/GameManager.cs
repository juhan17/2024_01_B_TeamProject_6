using System;                                   //Action �� ����ϱ����ؼ� 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] circleObject;           //��ü �������� �����´�. (�迭�� ����)
    public GameObject lanchobj;
    public Transform genTransform;              //���� ��ġ ����
    public float timeCheck;                     //���� �ð� ���� ����(float)
    public bool isGen;                          //���� üũ (bool)

    public int Point;                                           //����
    public int BestScore;                                       //�ְ� ����
    public ParabolicTrajectory parabolicTrajectory;


    public static event Action<int> OnPointChanged;             //������ ���� �Ǿ����� �̺�Ʈ�� �߻� ��Ų��. 
    public static event Action<int> OnBestScoreChanged;             //�ְ� ������ ���� �Ǿ����� �̺�Ʈ�� �߻� ��Ų��. 


    // Start is called before the first frame update
    void Start()
    {
        BestScore = PlayerPrefs.GetInt("BestScore");      
        OnPointChanged?.Invoke(Point);                          //������ �� ���� 1�� ����
        OnBestScoreChanged?.Invoke(BestScore);                  //������ �� �ְ� ���� 1�� ����
        timeCheck = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (lanchobj == null)
        {
            isGen = false;
        }

        if (timeCheck >= 0)
        {

            timeCheck -= Time.deltaTime;
        }

        if (isGen == false && timeCheck <= 0)                                      //isGen �÷��װ� false �� ���
        {
           
            int RandNumber = UnityEngine.Random.Range(0, 3);                // 0 ~ 2 �� ���� �ѹ� ����
            lanchobj = Instantiate(circleObject[RandNumber]);     //������ ������ Temp ������Ʈ�� �ִ´�. 
            lanchobj.transform.SetParent(genTransform);
            lanchobj.transform.position = genTransform.position;    //���� ��ġ�� ���� ��Ų��.
            isGen = true;            
        }
       
        if(Input.GetKeyUp(KeyCode.Space))
        {
            if (lanchobj == null)
            {
                return;
            }

            parabolicTrajectory.LaunchProjectile(lanchobj);

            lanchobj = null;
            isGen = false;
            timeCheck = 1.0f;
        }
    }

    public void MergeObject(int index, Vector3 position)            //�浹�� ��ü�� index ��ȣ�� ��ġ�� �����´�. 
    {
        GameObject Temp = Instantiate(circleObject[index]);         //������ ���� ������Ʈ�� Temp �� �ִ´�. 
        Temp.transform.position = position;                         //Temp ������Ʈ�� ��ġ�� �Լ��� �޾ƿ� ��ġ��        
        Temp.AddComponent<Rigidbody>();
        Point += (int)Mathf.Pow(index, 2) * 10;                     //index�� 2������ ����Ʈ ���� POW �Լ� Ȱ��
        OnPointChanged?.Invoke(Point);                              //����Ʈ�� ����Ǿ����� �̺�Ʈ�� ���� �Ǿ��ٰ� �˸�
    }

    public void EndGame()                                           //���� ���� �Ǿ��� �� �Լ�
    {
        int BestScore = PlayerPrefs.GetInt("BestScore");            //������ ����� ������ �ҷ��´�.

        if (Point > BestScore)                                       //���� ����Ʈ�� ���Ѵ�.
        {
            BestScore = Point;
            PlayerPrefs.SetInt("BestScore", BestScore);                 //����Ʈ�� �� Ŭ ��� �����Ѵ�.
            OnBestScoreChanged?.Invoke(BestScore);
        }

        //���� �� �ؾ� ���� ���߿� �߰� 
    }

}
