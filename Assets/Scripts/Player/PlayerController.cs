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
    [SerializeField] private Point _path;

    private Transform[] _points;
    private AudioSource _audioSourse;
    private bool _isShelter = false;
    private bool _isHide = false;
    private bool _isStop = false;
    private Transform _target;
    private Animator _animator;
    private int _currentPoint = -1;

    public bool IsHide => _isHide;

    private void Start()
    {
        _audioSourse = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();

        _points = new Transform[_path.transform.childCount];

        for (int i = 0; i < _path.transform.childCount; i++)
        {
            _points[i] = _path.transform.GetChild(i);
        }
        TakePointPosition();
    }

    private void Update()
    {
        if (_isStop == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        
            if(transform.position == _target.position)
            {
                Stop();
                _camera.transform.rotation = Quaternion.identity;
                AttackPlayer();
            }

            if(_points[_currentPoint].GetComponent<Point>().CountEnemy == 0 && transform.position == _target.position)
            {
                Continue();
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

    public void AttackPlayer()
    {
        _points[_currentPoint].GetComponent<Point>().LetEnemiesAttack();
    }

    public void TakePointPosition()
    {
        if (TryMoving())
        {
            _currentPoint++;    
            _target = _points[_currentPoint];
        }
        else
        {
            _game.WinLevel();
        }
    }

    private bool TryMoving()
    {
        if (_currentPoint + 1 < _points.Length)
            return true;
        else
            return false;
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

    private void Stop()
    {
        _audioSourse.Stop();
        _isStop = true;
    }

    public void Continue()
    {
        _audioSourse.PlayOneShot(_runClip);
        TakePointPosition();
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
