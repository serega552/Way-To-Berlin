using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] private Transform _character;
    [SerializeField] private Game _game;

    private float _sensitivity = 1f;
    private float _smoothing = 0.1f;
    private Vector2 _velocity;
    private Vector2 _frameVelocity;

    void Reset()
    {
        _character = GetComponentInParent<PlayerController>().transform;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (_game.IsMobile == false)
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * _sensitivity);
            _frameVelocity = Vector2.Lerp(_frameVelocity, rawFrameVelocity, 1 / _smoothing);
            _velocity += _frameVelocity;
            _velocity.y = Mathf.Clamp(_velocity.y, -90, 90);

            transform.localRotation = Quaternion.AngleAxis(-_velocity.y, Vector3.right);
            _character.localRotation = Quaternion.AngleAxis(_velocity.x, Vector3.up);
        }
    }

    public void ChangeSensivity(bool isChange)
    {
        if (isChange)
        {
            
        }
        else if (isChange == false)
        {
            
        }
    }
}