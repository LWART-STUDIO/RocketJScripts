using System;
using Unity.Mathematics;
using UnityEngine;
using Math = Engine.Math.Math;

namespace Custom.Logic
{
    public class Stabilizator : MonoBehaviour
    {
        private Rigidbody2D rigidbody2D;
        public float stability = 0.3f;
        public float speed = 2.0f;
        [SerializeField] private bool _useCustomGravity;
        [SerializeField] private bool _useStabilization;

        public bool UseCustomGravity => _useCustomGravity;
        public bool UseStabilization => _useStabilization;
        public GravityZone GravityZone;


        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void AddForce(Vector2 direction, float force)
        {
            rigidbody2D.AddForce(direction*force);
        }

        public void RotateTo(Vector2 direction)
        {
            float lookAngel = Mathf.Atan2(direction.y, direction.x)* Mathf.Rad2Deg;
            rigidbody2D.MoveRotation(Quaternion.AngleAxis(lookAngel+90,Vector3.back));

        }
            
        
        void FixedUpdate ()
        {
            if (!_useStabilization)
                return;
            float currentAngle = Mathf.Atan2(transform.up.x, transform.up.y);
            float predictedAngle = currentAngle - rigidbody2D.angularVelocity * stability / speed;
            float torque = Mathf.Sin(predictedAngle);
            rigidbody2D.AddTorque(torque * speed);
        }
    }
}
