using UnityEngine;
using UnityEngine.Events;

namespace UI.MainMenu
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] private UnityEvent _startGameEvent;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private Vector2 _moveDirection;
        [SerializeField] private Transform _spawnPos;
        
        private bool _startGame = false;

        private void FixedUpdate()
        {
            Move();
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            _moveDirection = GetMoveDirection();
        }

        public void StartGame()
        {
            _speed = 10;
            _startGame = true;
            _rigidbody.MovePosition(_spawnPos.position);

            // StartCoroutine(MoveToSpawnPoint());
        }

        private Vector3 GetMoveDirection()
        {
            return Random.insideUnitCircle;
        }

        private void Move()
        {
            if (!_startGame)
            {
                var newPos = _rigidbody.position + _moveDirection.normalized * ((_speed) * Time.fixedDeltaTime);
                _rigidbody.MovePosition(newPos);
            }
            else
            {
                if (transform.position == _spawnPos.position)
                {
                    StartAnimation();
                }
            }
        }

        private void StartAnimation()
        {
            _startGameEvent.Invoke();
        }
    }
}