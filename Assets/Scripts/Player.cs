using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Golf
{
    public class Player : MonoBehaviour
    {
        public Transform stick;
        public Transform helper;
        public Transform traektoryaFrom;
        public Transform traektoryaTo;
        public TMP_Text powerText;

        public float range = 30f;
		public float speed = 500f;
        private float power = 15f;

        public float x;//ближе дальше
        public float y;//вверх вниз
        public float z;//влево вправо

		private bool m_isHitDown = false;
        private bool m_isRight = false;
        private bool m_isLeft = false;

        private bool m_isUp = false;
        private bool m_isDown = false;

        private bool m_isPowerUp = false;
        private bool m_isPowerDown = false;

        private Vector3 m_lastPosition;
        private float m_value = 1f;
        private void Awake()
        {
            var pos = traektoryaTo.position;
            x = pos.x;
            y = pos.y;
            z = pos.z;
            powerText.text = $"power : {power}";
        }

        private void Update()
		{
            m_lastPosition = helper.position;

            Quaternion rot = stick.localRotation;
            Quaternion toRot = Quaternion.Euler(0, 0, m_isHitDown ? range : -range);
			stick.localRotation = Quaternion.RotateTowards(rot, toRot, speed * Time.deltaTime);
		}

        public void SetHitDown(bool value)
        {
            m_isHitDown = value;
        }
        public void SetLeftDown(bool value)
        {
            m_isLeft = value;
            if (m_isLeft)
            {
                z = z + m_value;
                traektoryaTo.position = new Vector3(x,y,z);
            }


        }
        public void SetRightDown(bool value)
        {
            m_isRight = value;
            if (m_isRight)
            {
                z = z - m_value;
                traektoryaTo.position = new Vector3(x, y, z);
            }
        }
        public void SetUpAngleDown(bool value)
        {
            m_isUp = value;
            if (m_isUp)
            {
                y = y + m_value;
                traektoryaTo.position = new Vector3(x, y, z);
            }
        }
        public void SetLowAngleDown(bool value)
        {
            m_isDown = value;
            if (m_isDown)
            {
                y = y - m_value;
                traektoryaTo.position = new Vector3(x, y, z);
            }
        }
        public void SetPowerUp(bool value)
        {
            m_isPowerUp = value;
            if (m_isPowerUp)
            {
                power = power + m_value; 
                powerText.text = $"power : {power}";                               
            }
        }
        public void SetPowerDown(bool value)
        {
            m_isPowerDown = value;
            if (m_isPowerDown)
            {
                power = power - m_value; 
                powerText.text = $"power : {power}";                               
            }
        }
        
        public void OnCollisonStick(Collider collider)
        {
            if (collider.TryGetComponent(out Rigidbody body))
            {
                var traek = (traektoryaTo.position - traektoryaFrom.position).normalized;
                //var traek = (traektoryaTo.position).normalized;
                body.AddForce(traek * power, ForceMode.Impulse);
                //var dir = (helper.position - m_lastPosition).normalized;
                //body.AddForce(dir * power, ForceMode.Impulse);
                Debug.Log(traek);
                if (collider.TryGetComponent(out Stone stone) && !stone.isAfect)
                {
                    GameEvents.StickHit();
                    stone.isAfect = true;                    
				}
            }
        }
    }
}