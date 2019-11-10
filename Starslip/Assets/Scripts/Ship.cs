using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    public float Speed;
    public float Sensitivity;
    public bool InvertAimY;

    private InputMaster inputMaster;
    private Vector2 moveDirection;
    private Vector3 aimLocation = Vector3.zero;
    private Vector3 aimPoint = Vector3.zero;
    private Ray aimRay;

    [SerializeField] private Transform target;
    [SerializeField] private Transform crosshairImage;

    protected void Awake()
    {
        inputMaster = new InputMaster();
        aimLocation = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }

    private void UpdateDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void UpdateAimLocation(Vector2 offset, bool scale = true)
    {
        Vector3 offsetV3 = new Vector3(offset.x, offset.y, 0);
        if (scale)
            offsetV3 *= Sensitivity * Time.deltaTime;
        if (InvertAimY)
            offsetV3.y *= -1;

        aimLocation += offsetV3;
    }

    private void Update()
    {
        UpdateDirection(inputMaster.Player.Move.ReadValue<Vector2>());
        Move();

        UpdateAimLocation(inputMaster.Player.Look.ReadValue<Vector2>());

        UpdateAimPoint();
        ClampAim();
        UpdateHitPoint();

        crosshairImage.position = Camera.main.WorldToScreenPoint(aimPoint);
    }

    private void Move()
    {
        Vector3 before = Camera.main.WorldToScreenPoint(transform.position);
        transform.Translate(moveDirection * Speed * Time.deltaTime);
        ClampPosition();
        Vector3 after = Camera.main.WorldToScreenPoint(transform.position);
        UpdateAimLocation(after - before, false);
    }

    private void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
        pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
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
    }

    private void UpdateHitPoint()
    {
        Debug.DrawLine(Camera.main.transform.position, aimPoint, Color.gray);
        if (Physics.Raycast(aimRay, out RaycastHit hit, 1000f))
        {
            aimPoint = hit.point;
            Debug.DrawLine(transform.position, aimPoint, Color.red);
        }
        else
        {
            Debug.DrawLine(transform.position, aimPoint, Color.blue);
        }
    }
}
