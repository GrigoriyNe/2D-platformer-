using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    private readonly string Horizontal = "Horizontal";
    readonly Vector2 force = new Vector2(0, 30);

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private int _degreeTurn = 180;
    private int _speed;
    private int _deafultSpeed = 2;
    private bool isGround = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Run();
        Move();
        Jump();
    }

    private void Move()
    {
        Vector2 direction = new Vector2(Input.GetAxis(Horizontal), 0);
        Vector2 startRotate = transform.eulerAngles;
        Vector2 rotate = transform.eulerAngles;
        startRotate.y = 0;
        rotate.y = _degreeTurn;   

        if (Input.GetAxis(Horizontal) != 0)
        {
            transform.rotation = (Quaternion.Euler(startRotate));
            transform.Translate(_speed * Time.deltaTime * direction);

            if (Input.GetAxis(Horizontal) < 0)
            {
                int factorNegative = -2;
                transform.rotation = (Quaternion.Euler(rotate));
                transform.Translate(_speed * Time.deltaTime * direction / factorNegative);
            }
        }
        else
        {
            _animator.SetInteger("States", 0);
        }
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isGround)
        {
            _speed = 5;
            _animator.SetInteger("States", 2);
        }
        else
        {
            _speed = _deafultSpeed;
            _animator.SetInteger("States", 1);
        }
    }

    private void Jump()
    {
        int rayDistance = 1;
        RaycastHit2D hit = Physics2D.Raycast(_rigidbody.position, Vector2.down, rayDistance, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
            isGround = true;

        else
            isGround = false;

        if (Input.GetKey(KeyCode.Space) && isGround)
            _rigidbody.AddForce(force);
    }
}
