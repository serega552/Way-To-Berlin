using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private float _speed;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _guns;
    [SerializeField] private GameObject _hide;
    [SerializeField] private GameObject _hideButton;
    [SerializeField] private AudioClip _runClip;

    private AudioSource _audioSourse;
    private bool _isShelter = false;
    private bool _isHide = false;
    private bool _isStop = false;
    private Transform _target;
    private Animator _animator;

    public bool IsHide => _isHide;

    private void Start()
    {
        GetPointPosition();
        _audioSourse = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isStop == false && _game.TryMoving())
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        
            if(transform.position == _target.position)
            {
                Stop();
                _camera.transform.rotation = Quaternion.identity;
                _game.AttackPlayer();
            }
        }

        if (_isShelter)
        {
            if(Input.GetKeyDown(KeyCode.T))
            {
               Hide();
            }
        }
    }

    public void Hide()
    {
        if (_animator.GetBool("hide") == false)
        {
            _guns.SetActive(false);
            _hide.SetActive(true);
            _animator.SetBool("hide", true);
            _isHide = true;
        }
        else
        {
            _guns.SetActive(true);
            _hide.SetActive(false);
            _animator.SetBool("hide", false);
            _isHide = false;
        }
    }

    private void GetPointPosition()
    {
        _target = _game.TakePointPosition();
    }

    private void Stop()
    {
        _audioSourse.Stop();
        _isStop = true;
    }

    public void Continue()
    {
        _audioSourse.PlayOneShot(_runClip);
        GetPointPosition();
        _isStop = false;
    }

    public void UsingShelter()
    {
        _isShelter= true;
        _hideButton.SetActive(true);
    }

    public void UnUsingShelter()
    {
        _isShelter= false;
        _hideButton.SetActive(false);
    }
}
