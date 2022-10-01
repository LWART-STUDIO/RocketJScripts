using UnityEngine;

namespace Custom.Logic.Player
{
    public class SlowmoControl : MonoBehaviour
    {
        private float _slowmoValue=0.2f;
        public void SetSlowMo()
        {
            Time.timeScale = _slowmoValue;
            Time.fixedDeltaTime = 0.02f * _slowmoValue;
        }

        public void UnSetSlowMo()
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
        }
        
    }
}
