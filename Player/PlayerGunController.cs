
using System.Collections;
using Custom.Logic.Player;
using Engine;
using Engine.DI;
using Engine.Input;
using Joystick_Pack.Scripts.Base;
using Main;
using NaughtyAttributes;
using UnityEngine;

namespace Custom.Logic
{
    [RequireComponent(typeof(PlayerMover),typeof(SlowmoControl))]
    public class PlayerGunController : MonoBehaviour,IEndDrag,IDrag,ILevelStarted,IBeginDrag
    {
        [SerializeField] private GameObject _gun;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private float _bulletForce=10f;
        [SerializeField] private bool _infinityBullets;
        [SerializeField] private TrajectoryRenderer _trajectoryRenderer;
        [SerializeField,HideIf("_infinityBullets")] private int _numberOfShots;
        private int _currentNumberOfShots;
        public int CurrentNumberOfShots
        {
            get => _currentNumberOfShots;

        }

        public bool InfinityBullets
        {
            get => _infinityBullets;

        }

        private SlowmoControl _slowmoControl;
        private Vector2 _lookDirection;
        private PlayerMover _playerMover;
        private Joystick _joystick;
        private bool _blockInput=true;
        private bool _drag;
        private float _waitTime = 0.2f;


        #region UNITY_METODS

        private void Awake()
        {
            LevelBulletSetUp levelBulletSetUp = GetComponentInParent<LevelBulletSetUp>();
            _numberOfShots = levelBulletSetUp.NumberOfBullets;
            _infinityBullets = levelBulletSetUp.InfinityBullets;
            _joystick = DIContainer.AsSingle<Joystick>();
            _playerMover = GetComponent<PlayerMover>();
            _currentNumberOfShots = _numberOfShots;
            _slowmoControl=GetComponent<SlowmoControl>();

        }

        private void OnEnable()
        {
            InputEvents.EndDrag.Subscribe(this);
            InputEvents.Drag.Subscribe(this);
            InputEvents.BeginDrag.Subscribe(this);
            LevelStatueStarted.Subscribe(this);

        }

        public void SetShootDirection(Vector2 direction)
        {
            _lookDirection = direction;
        }
        private void Update()
        {
            if (_currentNumberOfShots <= 0)
            {
                _blockInput = true;
            }
            _lookDirection = _gun.transform.up;
            _gun.transform.up -= new Vector3(_joystick.Direction.x,_joystick.Direction.y,0);
            if (_drag)
            {
                _trajectoryRenderer.ShowTrajectory(_shootPoint.position,-_lookDirection*_bulletForce);
            }
            //   angle = Mathf.Atan2(_gun.transform.up.y, _gun.transform.up.x) * Mathf.Rad2Deg;
        }

        private void OnDisable()
        {
            InputEvents.EndDrag.Unsubscribe(this);
            InputEvents.Drag.Unsubscribe(this);
            InputEvents.BeginDrag.Unsubscribe(this);
            LevelStatueStarted.Unsubscribe(this);
    
        }

        #endregion
        

        public void OnEndDrag(InputInfo data)
        {
            if(_blockInput)
                return;
            _drag = false;
            Shoot();
        }
        
        public void Shoot()
        {
            
            if(_blockInput)
                return;
            _slowmoControl.UnSetSlowMo();
            _trajectoryRenderer.HideTrajectory();
            if (!_infinityBullets)
            {
                if(_currentNumberOfShots<=0)
                    return;
                _currentNumberOfShots--;
            }
            GameObject bullet = Instantiate(_bulletPrefab,_shootPoint.position,Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce(-_lookDirection*_bulletForce,ForceMode2D.Impulse);
            _playerMover.Move(_lookDirection);

        }
        public void OnDrag(InputInfo data)
        {
            if(_blockInput)
                return;
            _drag = true;
           
        }

        public void LevelStarted()
        {
            StartCoroutine(WaitUnBlockInput());
        }

        public void OnBeginDrag(InputInfo data)
        {
            if(_blockInput)
                return;
            _slowmoControl.SetSlowMo();
            _drag = true;
        }

        private IEnumerator WaitUnBlockInput()
        {
            yield return new WaitForSeconds(_waitTime);
            _blockInput = false;
        }
    }
}
