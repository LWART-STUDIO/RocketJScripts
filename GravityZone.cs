using System;
using UnityEngine;

namespace Custom.Logic
{
    public class GravityZone : MonoBehaviour
    {
        private Stabilizator _stabilizator;
        private float _force = -10f;

        /*private void OnTriggerStay2D(Collider2D col)
        {
            if (col.TryGetComponent(out Stabilizator stabilizator))
            {
                if(!stabilizator.UseCustomGravity)
                    return;
                if(stabilizator.GravityZone != this && stabilizator.GravityZone != null)
                    return;
                stabilizator.GravityZone = this;
                _stabilizator = stabilizator;
                Vector2 direction = col.transform.position - transform.position;
                _stabilizator.AddForce(direction.normalized,_force);
                _stabilizator.RotateTo(direction.normalized);
            }
        }*/

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Stabilizator stabilizator))
            {
                if (!stabilizator.UseCustomGravity)
                    return;
                if(stabilizator.GravityZone==this)
                    stabilizator.GravityZone = null;
            }
        }
    }
}
