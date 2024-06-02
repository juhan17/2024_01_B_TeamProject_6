using UnityEngine;
using UnityEngine.UI;
public class ParabolicTrajectory : MonoBehaviour
{
   
    public LineRenderer lineRenderer;                   // Line Renderer 컴포넌트를 할당할 변수   
    public int resolution = 30;                         // 궤적을 그릴 때 사용할 점의 개수 (해상도)   
    public float timeStep = 0.1f;                       // 시간 간격 (0.1초마다 점을 찍음)
    public Transform launchPoint;                       // 발사 위치를 나타내는 트랜스폼
    public Transform pivotPoint;
    public float myRotation;
    public float launchPower;                           // 발사 속도
    public float launchAngle;                           // 발사 각도 (도 단위)
    public float launchDirection;                       // 발사 방향 (XZ 평면에서의 각도, 도 단위)
    public float gravity = -9.8f;                       // 중력 값
    public GameObject projectilePrefab;                 // 발사할 물체의 프리팹

    public Slider angleSlider;
    public Slider directionSlider;
    public Slider myRotationSlider;
    public Slider launchPowerSlider;

    void Start()
    {        
        lineRenderer.positionCount = resolution;         // Line Renderer의 점 개수를 설정   
        angleSlider.onValueChanged.AddListener(angleSliderValue);
        directionSlider.onValueChanged.AddListener(directionSliderValue);
        myRotationSlider.onValueChanged.AddListener(myRotationSliderValue);
        launchPowerSlider.onValueChanged.AddListener(launchPowerSliderValue);
    }

    private void Update()
    {
        RenderTrajectory();
        myRotationValue();

       
    }

    void myRotationValue()
    {
        pivotPoint.localEulerAngles = new Vector3(0.0f, myRotation, 0.0f);
    }

    void angleSliderValue(float angle)
    {
        launchAngle = angle;
    }

    void directionSliderValue(float angle)
    {
        launchDirection = angle;
    }

    void myRotationSliderValue(float angle)
    {
        myRotation = angle;
    }

    void launchPowerSliderValue(float angle)
    {
        launchPower = angle;
    }


    void RenderTrajectory()                              // 궤적을 계산하고 Line Renderer에 설정하는 함수
    {        
        Vector3[] points = new Vector3[resolution];      // 궤적 점들을 저장할 배열       
        for (int i = 0; i < resolution; i++)             // 각 시간 간격마다 점의 위치를 계산
        {            
            float t = i * timeStep;                      // 현재 시간 계산           
            points[i] = CalculatePositionAtTime(t);      // 현재 시간에서의 위치 계산
        }
        lineRenderer.SetPositions(points);               // 계산된 점들을 Line Renderer에 설정
    }
   
    Vector3 CalculatePositionAtTime(float time)          // 주어진 시간에서 물체의 위치를 계산하는 함수
    {
        
        float launchAngleRad = Mathf.Deg2Rad * launchAngle;             // 발사 각도를 라디안으로 변환
        float launchDirectionRad = Mathf.Deg2Rad * launchDirection;
        // 시간 t에서의 x, y, z 좌표 계산
        float x = launchPower * time * Mathf.Cos(launchAngleRad) * Mathf.Cos(launchDirectionRad);
        float z = launchPower * time * Mathf.Cos(launchAngleRad) * Mathf.Sin(launchDirectionRad);
        float y = launchPower * time * Mathf.Sin(launchAngleRad) + 0.5f * gravity * time * time;

        // 발사 위치를 기준으로 계산된 위치 반환
        return launchPoint.position + new Vector3(x, y, z);
    }

    // 물체를 발사하는 함수
    public void LaunchProjectile(GameObject temp)
    {       
        temp.transform.position = launchPoint.position;
        temp.transform.rotation = launchPoint.rotation;
        temp.transform.SetParent(null);

        // Rigidbody 컴포넌트를 가져옴
        Rigidbody rb = temp.GetComponent<Rigidbody>();
        if(rb == null)
        {
            rb = temp.AddComponent<Rigidbody>();   
        }

        if (rb != null)
        {
            rb.isKinematic = false;
            // 발사 각도와 방향을 라디안으로 변환
            float launchAngleRad = Mathf.Deg2Rad * launchAngle;
            float launchDirectionRad = Mathf.Deg2Rad * launchDirection;

            // 초기 속도를 계산
            float initialVelocityX = launchPower * Mathf.Cos(launchAngleRad) * Mathf.Cos(launchDirectionRad);
            float initialVelocityZ = launchPower * Mathf.Cos(launchAngleRad) * Mathf.Sin(launchDirectionRad);
            float initialVelocityY = launchPower * Mathf.Sin(launchAngleRad);

            Vector3 initialVelocity = new Vector3(initialVelocityX, initialVelocityY, initialVelocityZ);

            // Rigidbody에 초기 속도를 적용
            rb.velocity = initialVelocity;
        }
    }
}