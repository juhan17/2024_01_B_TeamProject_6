using UnityEngine;
using UnityEngine.UI;
public class ParabolicTrajectory : MonoBehaviour
{
   
    public LineRenderer lineRenderer;                   // Line Renderer ������Ʈ�� �Ҵ��� ����   
    public int resolution = 30;                         // ������ �׸� �� ����� ���� ���� (�ػ�)   
    public float timeStep = 0.1f;                       // �ð� ���� (0.1�ʸ��� ���� ����)
    public Transform launchPoint;                       // �߻� ��ġ�� ��Ÿ���� Ʈ������
    public Transform pivotPoint;
    public float myRotation;
    public float launchPower;                           // �߻� �ӵ�
    public float launchAngle;                           // �߻� ���� (�� ����)
    public float launchDirection;                       // �߻� ���� (XZ ��鿡���� ����, �� ����)
    public float gravity = -9.8f;                       // �߷� ��
    public GameObject projectilePrefab;                 // �߻��� ��ü�� ������

    public Slider angleSlider;
    public Slider directionSlider;
    public Slider myRotationSlider;
    public Slider launchPowerSlider;

    void Start()
    {        
        lineRenderer.positionCount = resolution;         // Line Renderer�� �� ������ ����   
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


    void RenderTrajectory()                              // ������ ����ϰ� Line Renderer�� �����ϴ� �Լ�
    {        
        Vector3[] points = new Vector3[resolution];      // ���� ������ ������ �迭       
        for (int i = 0; i < resolution; i++)             // �� �ð� ���ݸ��� ���� ��ġ�� ���
        {            
            float t = i * timeStep;                      // ���� �ð� ���           
            points[i] = CalculatePositionAtTime(t);      // ���� �ð������� ��ġ ���
        }
        lineRenderer.SetPositions(points);               // ���� ������ Line Renderer�� ����
    }
   
    Vector3 CalculatePositionAtTime(float time)          // �־��� �ð����� ��ü�� ��ġ�� ����ϴ� �Լ�
    {
        
        float launchAngleRad = Mathf.Deg2Rad * launchAngle;             // �߻� ������ �������� ��ȯ
        float launchDirectionRad = Mathf.Deg2Rad * launchDirection;
        // �ð� t������ x, y, z ��ǥ ���
        float x = launchPower * time * Mathf.Cos(launchAngleRad) * Mathf.Cos(launchDirectionRad);
        float z = launchPower * time * Mathf.Cos(launchAngleRad) * Mathf.Sin(launchDirectionRad);
        float y = launchPower * time * Mathf.Sin(launchAngleRad) + 0.5f * gravity * time * time;

        // �߻� ��ġ�� �������� ���� ��ġ ��ȯ
        return launchPoint.position + new Vector3(x, y, z);
    }

    // ��ü�� �߻��ϴ� �Լ�
    public void LaunchProjectile(GameObject temp)
    {       
        temp.transform.position = launchPoint.position;
        temp.transform.rotation = launchPoint.rotation;
        temp.transform.SetParent(null);

        // Rigidbody ������Ʈ�� ������
        Rigidbody rb = temp.GetComponent<Rigidbody>();
        if(rb == null)
        {
            rb = temp.AddComponent<Rigidbody>();   
        }

        if (rb != null)
        {
            rb.isKinematic = false;
            // �߻� ������ ������ �������� ��ȯ
            float launchAngleRad = Mathf.Deg2Rad * launchAngle;
            float launchDirectionRad = Mathf.Deg2Rad * launchDirection;

            // �ʱ� �ӵ��� ���
            float initialVelocityX = launchPower * Mathf.Cos(launchAngleRad) * Mathf.Cos(launchDirectionRad);
            float initialVelocityZ = launchPower * Mathf.Cos(launchAngleRad) * Mathf.Sin(launchDirectionRad);
            float initialVelocityY = launchPower * Mathf.Sin(launchAngleRad);

            Vector3 initialVelocity = new Vector3(initialVelocityX, initialVelocityY, initialVelocityZ);

            // Rigidbody�� �ʱ� �ӵ��� ����
            rb.velocity = initialVelocity;
        }
    }
}