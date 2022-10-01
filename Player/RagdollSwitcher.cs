using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollSwitcher : MonoBehaviour
{

    [SerializeField] private Collider _mainCollider;
    [SerializeField] private Rigidbody _mainRigidbody;

    private Rigidbody[] _rigidbodies;

    private Vector3[] _bonesPositions;

    private Quaternion[] _bonesRotations;

    private float _mainBoneY;

    private Vector3 _charPosition;

    private void Awake()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();

        _mainBoneY = _rigidbodies[0].position.y;
    }


    public void SwitchRB(bool kinematic, Vector3 force)
    {
        StartCoroutine(_SwitchRB(kinematic, force));
    }


    private IEnumerator _SwitchRB(bool kinematic, Vector3 force)
    {

        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();


        if (_rigidbodies != null && _rigidbodies.Length > 0)
        {

            if (kinematic && _bonesPositions != null)
            {
                float t = 0;

                //_mainRigidbody.MovePosition(new Vector3(_rigidbodies[0].position.x, _mainRigidbody.position.y, _rigidbodies[0].position.z));

                //Vector3 charNeedPosition = new Vector3(_rigidbodies[0].position.x, _mainRigidbody.position.y, _rigidbodies[0].position.z);

                //Vector3 charStartPosition = _mainRigidbody.position;

                Vector3[] startPositions = new Vector3[_rigidbodies.Length];

                Quaternion[] startRotations = new Quaternion[_rigidbodies.Length];

                Vector3 offset = _rigidbodies[0].position - _bonesPositions[0];

                offset.y = 0;




                //Vector3 charNeedPosition = charStartPosition + offset;



                //Vector3 needPosition = startPosition;

                //Quaternion startRotation = _rigidbodies[0].rotation;

                //needPosition.y = _mainBoneY;

                //_rigidbodies[0].isKinematic = true;


                for (int i = 0; i < _rigidbodies.Length; i++)
                {
                    _rigidbodies[i].isKinematic = true;

                    _rigidbodies[i].interpolation = RigidbodyInterpolation.Interpolate;

                    startPositions[i] = _rigidbodies[i].position;

                    startRotations[i] = _rigidbodies[i].rotation;
                }




                while (t < 1.0f)
                {
                    float smooth = Mathf.SmoothStep(0.0f, 1.0f, t);

                    _mainRigidbody.MovePosition(Vector3.Lerp(_charPosition, _charPosition + offset, smooth));

                    //_rigidbodies[0].MovePosition(Vector3.Lerp(startPosition, needPosition, t));

                    //_rigidbodies[0].MoveRotation(Quaternion.Lerp(startRotation, _mainRigidbody.rotation, t));

                    for (int i = 0; i < _rigidbodies.Length; i++)
                    {
                        _rigidbodies[i].MovePosition(Vector3.Lerp(startPositions[i], _bonesPositions[i] + offset, smooth));

                        _rigidbodies[i].MoveRotation(Quaternion.Lerp(startRotations[i], _bonesRotations[i], smooth));
                    }


                        t += Time.deltaTime * 2.0f;

                    yield return waitForEndOfFrame;
                }
            }

            for (int i = 0; i < _rigidbodies.Length; i++)
            {
                _rigidbodies[i].isKinematic = kinematic;

                if (kinematic == false) _rigidbodies[i].AddForce(force, ForceMode.Impulse);

                //if (!kinematic) _rigidbodies[i].interpolation = RigidbodyInterpolation.Interpolate;
                //if (!kinematic) _rigidbodies[i].collisionDetectionMode = CollisionDetectionMode.Continuous;
            }
        }

        if (!kinematic)
        {
            SaveBones();

            _charPosition = _mainRigidbody.position;

            GetComponent<Animator>().enabled = false;
            _mainRigidbody.isKinematic = true;
            _mainCollider.enabled = false;

            //_mainBoneY = _rigidbodies[0].position.y;
        }
        else
        {          
            GetComponent<Animator>().enabled = true;
            _mainRigidbody.isKinematic = false;
            _mainCollider.enabled = true;
        }
    }


    private void SaveBones()
    {
        _bonesPositions = new Vector3[_rigidbodies.Length];

        _bonesRotations = new Quaternion[_rigidbodies.Length];

        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _bonesPositions[i] = _rigidbodies[i].position;

            _bonesRotations[i] = _rigidbodies[i].rotation;
        }
    }


}
