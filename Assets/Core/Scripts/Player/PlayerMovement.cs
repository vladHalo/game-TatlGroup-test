using System.Collections;
using Core.Scripts.Views;
using UnityEngine;

namespace Core.Scripts.Player
{
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private int _jumpForce = 8;
        [SerializeField] private float _speed = 5;
        [SerializeField] private float _dashDelay = 1, _dashDuration = .3f, _dashDistance = 10;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private ActionButtonManager _actionButtonManager;
        [SerializeField] private BarView _barView;

        private PlayerStatus _playerStatus;
        private float _dashTime;

        private void Start()
        {
            _dashTime = _dashDelay;
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void Update()
        {
            if (_dashTime < _dashDelay)
            {
                _dashTime += Time.deltaTime;
                if (_dashTime >= _dashDelay)
                {
                    _playerStatus = PlayerStatus.None;
                    _actionButtonManager.ChangeButton(0);
                }

                _barView.SetValue(_dashTime / _dashDelay);
            }

            if (_playerStatus != PlayerStatus.None) return;

            if (Input.GetKey(KeyCode.Space))
            {
                _actionButtonManager.ChangeButton(0);
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Dash();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == 3)
            {
                _actionButtonManager.ChangeButton(0);
                _playerStatus = PlayerStatus.None;
            }
        }

        private void Movement()
        {
            Vector3 moveVector = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
            _rigidbody.velocity = new Vector3(moveVector.x * _speed, _rigidbody.velocity.y, moveVector.z * _speed);

            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                Vector3 direct =
                    Vector3.RotateTowards(transform.forward, moveVector, _speed * 4f * Time.fixedDeltaTime, 0.0f);
                _rigidbody.rotation = Quaternion.LookRotation(direct);
            }

            var floatSpeed = _rigidbody.velocity.magnitude / _speed;
            _animator.SetFloat(Str.SpeedMove, floatSpeed);
        }

        private void Jump()
        {
            _playerStatus = PlayerStatus.Jump;
            _animator.SetTrigger(Str.Jump);
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        private void Dash()
        {
            _playerStatus = PlayerStatus.Dash;
            _actionButtonManager.ChangeButton(0);

            _animator.SetTrigger(Str.Dash);
            StartCoroutine(LerpDash());
        }

        private IEnumerator LerpDash()
        {
            float elapsedTime = 0f;
            Vector3 initialPosition = transform.position;
            Vector3 targetPosition = transform.position + transform.forward * _dashDistance;

            while (elapsedTime < _dashDuration)
            {
                transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / _dashDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;
            _barView.SetValue(0);
            _dashTime = 0;
        }
    }
}