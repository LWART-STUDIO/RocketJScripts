using System;
using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

namespace Custom.Logic.Player
{
    public class PlayerRagdollControl : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody[] _rigidbodies;
        [SerializeField] private bool _grounded = false;
        [SerializeField] private LayerMask _layerMask;
        private bool _stand;
        private Coroutine _rutine;


        private Vector3[] _bonesPositions;

        private Quaternion[] _bonesRotations;

        private float _mainBoneY;

        private Vector3 _charPosition;

        private void Awake()
        {
            /*_mainBoneY = _rigidbodies[0].position.y;
            if(_rutine==null)
                 SwitchRB(true, Vector3.zero);
            SaveBones();*/
        }

        private void Update()
        {
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, -transform.up, 1.2f, _layerMask);

            _grounded = hit2D.collider != null;

            if (_rigidbody2D.velocity.magnitude < 1 && _grounded)
            {
                _animator.SetBool("Fly",false);
              //  _animator.enabled = true;
                /*foreach (Rigidbody rigidbody in _rigidbodies)
                {
                    
                    rigidbody.isKinematic = true;
                }
                if (_rutine == null)
                {
                    SwitchRB(true, Vector3.zero);
                    _stand = true;
                }*/
                    
            }
            else if(_rigidbody2D.velocity.magnitude > 1 && !_grounded)
            {
                _stand = false;
                _animator.SetBool("Fly",true);
              // _animator.enabled = false;
                /*foreach (Rigidbody rigidbody in _rigidbodies)
                {
                    
                    rigidbody.isKinematic = false;
                }*/
                /*if(_rutine==null)
                    SwitchRB(false, Vector3.zero);*/
            }
        }

        /*public void SwitchRB(bool kinematic, Vector3 force)
        {
            
            _rutine= StartCoroutine(_SwitchRB(kinematic, force));
        }*/

        /*private IEnumerator _SwitchRB(bool kinematic, Vector3 force)
        {
            WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();


            if (_rigidbodies != null && _rigidbodies.Length > 0)
            {
                if (kinematic && _bonesPositions != null)
                {
                    float t = 0;
                    
                    Vector3[] startPositions = new Vector3[_rigidbodies.Length];

                    Quaternion[] startRotations = new Quaternion[_rigidbodies.Length];

                    Vector3 offset = _rigidbodies[0].transform.localPosition - _bonesPositions[0];

                    offset.y = 0;

                    for (int i = 0; i < _rigidbodies.Length; i++)
                    {
                        _rigidbodies[i].isKinematic = true;

                        _rigidbodies[i].interpolation = RigidbodyInterpolation.Interpolate;

                        startPositions[i] = _rigidbodies[i].transform.localPosition;
                        

                        startRotations[i] = _rigidbodies[i].transform.localRotation;
                    }


                    while (t < 1.0f)
                    {
                        float smooth = Mathf.SmoothStep(0.0f, 1.0f, t);
                       // Debug.Log(t);
                     // _rigidbody2D.MovePosition(Vector3.Lerp(_charPosition, _charPosition + offset, smooth));

                        for (int i = 0; i < _rigidbodies.Length; i++)
                        {
                            _rigidbodies[i]
                                .transform.Translate(Vector3.Lerp(startPositions[i], _bonesPositions[i] + offset, smooth));

                            _rigidbodies[i]
                                .MoveRotation(Quaternion.Lerp(startRotations[i], _bonesRotations[i], smooth));
                        }


                        t += Time.deltaTime * 1.0f;

                        yield return waitForEndOfFrame;
                    }
                }

                for (int i = 0; i < _rigidbodies.Length; i++)
                {
                    _rigidbodies[i].isKinematic = kinematic;

                    if (kinematic == false && force != Vector3.zero)
                        _rigidbodies[i].AddForce(force, ForceMode.Impulse);
                    
                }
            }

            if (!kinematic)
            {
                SaveBones();

                _charPosition = _rigidbody2D.position;

                _animator.enabled = false;
               // _rigidbody2D.isKinematic = true;
                // _mainCollider.enabled = false;

                //_mainBoneY = _rigidbodies[0].position.y;
            }
            else
            {
                _stand = true;
                _animator.enabled = true;
               // _rigidbody2D.isKinematic = false;
                //  _mainCollider.enabled = true;
            }

            _rutine = null;
        }*/


        /*private void SaveBones()
        {
            Debug.Log("Save Bones");
            _bonesPositions = new Vector3[_rigidbodies.Length];

            _bonesRotations = new Quaternion[_rigidbodies.Length];

            for (int i = 0; i < _rigidbodies.Length; i++)
            {
                _bonesPositions[i] = _rigidbodies[i].position;

                _bonesRotations[i] = _rigidbodies[i].rotation;
            }
        }*/

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position,
                new Vector3(transform.position.x, transform.position.y - 1.2f, transform.position.z));
        }

        [NaughtyAttributes.Button()]
        private void GetRigidbodys()
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
        }
    }
}