using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class Ring : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent(out Stone stone))
            {
                GameEvents.CollisionRingInvoke();
                Destroy(gameObject);
            }
        }
        
    }
}
