using UnityEngine;

public class Ship : MonoBehaviour
{
    public float Speed = 18;

    private InputMaster inputMaster;
    private Vector2 direction;

    protected void Awake()
    {
        inputMaster = new InputMaster();
        inputMaster.Player.Move.performed += ctx => UpdateDirection(ctx.ReadValue<Vector2>());
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
        direction = dir;
    }

    private void Update()
    {
        Move();
        ClampPosition();
    }

    private void Move()
    {
        transform.Translate(direction * Speed * Time.deltaTime);
    }

    private void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}
