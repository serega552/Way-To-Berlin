using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _sensitivityX = 0.5f;
    [SerializeField] private float _sensitivityY = 0.5f;
    [SerializeField] private Camera _camera;
    [SerializeField] private Game _game;

    private Vector2 _delta;
    private Vector3 _targetAngles;
    private Vector3 _followAngles;
    private bool _isTouch = false;

    void Start()
    {
        _targetAngles = transform.localRotation.eulerAngles;
        _followAngles = _targetAngles;
    }

    void Update()
    {
        if (_game.IsMobile)
        {
            if (_isTouch)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    _delta = touch.deltaPosition;
                    _targetAngles += new Vector3(-_delta.y * _sensitivityY, _delta.x * _sensitivityX, 0);

                    _targetAngles.x = Mathf.Clamp(_targetAngles.x, -90f, 90f);

                    _followAngles.x = _targetAngles.x;
                    _followAngles.y = _targetAngles.y;

                    _camera.transform.localRotation = Quaternion.AngleAxis(_followAngles.y, Vector3.up);
                    _camera.transform.localRotation *= Quaternion.AngleAxis(_followAngles.x, Vector3.right);
                }

            }
            _camera.transform.localRotation = Quaternion.AngleAxis(-_followAngles.y, Vector3.up);
            _camera.transform.localRotation *= Quaternion.AngleAxis(-_followAngles.x, Vector3.right);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isTouch = false;
    }
}
