using UnityEngine;

namespace Custom.Logic
{
    public class PlayerCenterMassControl : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _playerRigidbody;
        [SerializeField] private Vector3 _centerOfMass;
        private void Update()
        {
            _playerRigidbody.centerOfMass = _centerOfMass;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color=Color.red;
            Gizmos.DrawSphere(transform.position+transform.rotation*_centerOfMass,0.3f);
        }
    }
}