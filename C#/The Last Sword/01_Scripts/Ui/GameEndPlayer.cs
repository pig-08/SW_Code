using Unity.Cinemachine;
using UnityEngine;

namespace SW.Code.Ui
{
    public class GameEndPlayer : MonoBehaviour
    {
        private Rigidbody2D _rigid;
        private Animator _animator;
        private float _moveSpeed = 8f;
        private bool isRun;
        private void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            new CameraTarget();
        }

        public void RunStart()
        {
            isRun = true;
            _animator.Play("PlayerRun");
        }

        private void Update()
        {
            if (isRun)
                _rigid.linearVelocityX = _moveSpeed * 1;
        }
    }
}