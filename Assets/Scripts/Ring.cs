using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class Ring : MonoBehaviour
    {        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out Stone other))
            {
                GameEvents.CollisionRingInvoke();
            }
        }
    }
}
