using UnityEngine;
using Cinemachine;

public class Ship : MonoBehaviour
{
    public float Speed;
    public float Sensitivity;
    public bool InvertAimY;

    private InputMaster inputMaster;
    private Vector3 aimLocation = Vector3.zero;
    private Vector3 aimPoint = Vector3.zero;
    private Ray shipAimRay;
    private Ray aimRay;

    private CinemachineSmoothPath path;
    private CinemachineDollyCart dolly;

    [SerializeField] private Transform target;
    [SerializeField] private Transform crosshairImage;
    [SerializeField] private Transform shipModel;
    [SerializeField] private Transform bulletSpawnPoint;

    [SerializeField] private GameObject bullet;

    bool shotDelay = false;

    protected void Awake()
    {
        inputMaster = new InputMaster();
        aimLocation = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        dolly = GetComponentInParent<CinemachineDollyCart>();
        path = (CinemachineSmoothPath)dolly.m_Path;
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }

    private void Update()
    {
        UpdateAimLocation(inputMaster.Player.Move.ReadValue<Vector2>());
        ClampAim();
        UpdateAimPoint();
        UpdateHitPoint();

        crosshairImage.position = Camera.main.WorldToScreenPoint(aimPoint);

        Move();
        Orient();

        Shoot();
    }

    private void UpdateAimLocation(Vector2 offset)
    {
        Vector3 offsetV3 = new Vector3(offset.x, offset.y, 0) * Sensitivity * Time.deltaTime;
        if (InvertAimY)
            offsetV3.y *= -1;

        aimLocation += offsetV3;
    }

    private void Move()
    {
        Vector3 toPoint = aimRay.GetPoint(20f);

        if (Vector3.Distance(transform.position, toPoint) < 1f)
            return;
        transform.position = Vector3.Lerp(transform.position, toPoint, Time.deltaTime * Speed);

        shipModel.LookAt(aimPoint);
    }

    private void Orient()
    {
        Quaternion orientation = path.EvaluateOrientationAtUnit(dolly.m_Position, dolly.m_PositionUnits);
        var diffZ = orientation.eulerAngles.z - shipModel.rotation.eulerAngles.z;
        Quaternion rot = Quaternion.AngleAxis(diffZ, shipModel.forward);
        shipModel.rotation = rot * shipModel.rotation;
    }

    private void ClampAim()
    {
        aimLocation.x = Mathf.Clamp(aimLocation.x, 10, Screen.width - 10);
        aimLocation.y = Mathf.Clamp(aimLocation.y, 10, Screen.height - 10);
    }

    private void UpdateAimPoint()
    {
        aimRay = Camera.main.ScreenPointToRay(aimLocation);
        aimPoint = aimRay.GetPoint(1000f);
        shipAimRay = new Ray(transform.position, aimPoint - transform.position);
    }

    private void UpdateHitPoint()
    {
        Debug.DrawLine(Camera.main.transform.position, aimPoint, Color.gray);
        var isHit = Physics.Raycast(aimRay, out RaycastHit hit, 1000f, LayerMask.GetMask("Targetable"));

        if (isHit && hit.distance > 100f)
        {
            aimPoint = hit.point;
            Debug.DrawLine(transform.position, aimPoint, Color.red);
        }
        else
        {
            Debug.DrawLine(transform.position, aimPoint, Color.blue);
        }
    }

    private void Shoot()
    {
        if (inputMaster.Player.Fire.ReadValue<float>() > 0f && !shotDelay)
        {
            var b = Instantiate(bullet, bulletSpawnPoint.position, new Quaternion());
            b.transform.LookAt(aimPoint);
            var rb = b.GetComponent<Rigidbody>();
            rb.AddForce(b.transform.forward * 5000f);
            shotDelay = true;
        }
        else if (inputMaster.Player.Fire.ReadValue<float>() < 0.01f)
        {
            shotDelay = false;
        }
    }
}
