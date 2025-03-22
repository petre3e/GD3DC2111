using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _speed;

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityScale;

    private bool _isGround;
    private bool _isJump;

    private Vector3 _movement; //смещение в мировых кординатах

    private float _velosity; 

    private void Start()
    {
        _isJump = false;
        _rb = GetComponent<Rigidbody>();
        _movement = Vector3.forward;  //настройка смещения для движения вперед
    }

    
    private void FixedUpdate()
    {
        //установить позицию героя  т.е. текущее положение + смещение умноженное на скорость и на изменение времени
        _rb.MovePosition(transform.position + _movement * _speed * Time.fixedDeltaTime); 

        if (_isJump)
        {
            _isJump = false;
            //_rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _movement.y = _jumpForce;
        }

        if (!_isGround)
        {
            _movement.y -= _gravityScale * Time.fixedDeltaTime;
        }
        
        if (_isGround)
        {
            _movement.y = 0;
        }
          
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _isGround)
        {
            _isGround = false;
            _isJump = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            _isGround = true;
        }

        if (collision.gameObject.tag == "kill")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


}
