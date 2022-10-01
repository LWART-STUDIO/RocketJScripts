using UnityEngine;

namespace Custom.Logic
{
    [RequireComponent(typeof(Rigidbody2D) )]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _moveForce=10f;
        [SerializeField] private float _rotateForce=5f;
        private Rigidbody2D _playerRigidbody;
        private float angle;

        public void Awake()
        {
            _playerRigidbody = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 lookDirection)
        {
            angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            /*if (angle < 50 || angle > 130)
        { */
            _playerRigidbody.AddForce(lookDirection * _moveForce,ForceMode2D.Force);
            _playerRigidbody.AddTorque(-lookDirection.x* _rotateForce,ForceMode2D.Force);
            //float x = lookDirection.x / Mathf.Abs(lookDirection.x);
            //  _playerRigidbody.AddForce(Vector2.left * 12 * -x,ForceMode2D.Impulse);
            //}
        }
    }
}
