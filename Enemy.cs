using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _waypoints;

    private int _finishValueWaypoint = 1;
    private int _currentWaypoint = 0;
    private int _degreeTurn = 180;
    private Animator _animator;
    private Collider2D _collider;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _animator.SetInteger("States", 1);

        float step = _speed * Time.deltaTime;
        Vector2 rotate = transform.eulerAngles;
        Vector2 startRotate = transform.eulerAngles;
        rotate.y = _degreeTurn;
        startRotate.y = 0;

        if (transform.position == _waypoints[_currentWaypoint].position)
            _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Length;

        Vector2 direction = _waypoints[_currentWaypoint].position;

        if (_currentWaypoint == _finishValueWaypoint)
            transform.rotation = (Quaternion.Euler(rotate));
        else
            transform.rotation = (Quaternion.Euler(startRotate));

        transform.position = Vector2.MoveTowards(transform.position, direction, step);
    }
}
