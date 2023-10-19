using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{

    public class PlayerController : MonoBehaviour
    {
		[SerializeField] private Player player;

		private void Start()
		{
			if (player == null)
			{
				Debug.Log("Player is NULL!!!");
			}
		}

		private void Update()
		{
			//if (player != null)
			//{
			//	player.SetDown(Input.GetMouseButton(0));
			//}
		}
        /// <summary>
        /// ���� �������
        /// </summary>
        public void OnDown()
		{
			player.SetHitDown(true);
		}

		public void OnUp()
		{
			player.SetHitDown(false);
		}
		/// <summary>
		/// ���� �����
		/// </summary>
        public void OnLeftDown()
        {
            player.SetLeftDown(true);
            
        }

        public void OnLeftUp()
        {
            player.SetLeftDown(false);
            
        }
        /// <summary>
        /// ���� ������
        /// </summary>
        public void OnRightDown()
        {
            player.SetRightDown(true);
            
        }

        public void OnRightUp()
        {
            player.SetRightDown(false);
            
        }
        /// <summary>
		/// ���� �����
		/// </summary>
        public void OnUpAngleDown()
        {
            player.SetUpAngleDown(true);
        }

        public void OnUpAngleUp()
        {
            player.SetUpAngleDown(false);
        }
        /// <summary>
		/// ���� ����
		/// </summary>
        public void OnLowAngleDown()
        {
            player.SetLowAngleDown(true);
        }

        public void OnLowAngleUp()
        {
            player.SetLowAngleDown(false);
        }

        /// <summary>
		/// ���� �������
		/// </summary>
        public void OnPowerUpDown()
        {
            player.SetPowerUp(true);
        }

        public void OnPowerUpUp()
        {
            player.SetPowerUp(false);
        }

        /// <summary>
		/// ���� ������
		/// </summary>
        public void OnPowerDownDown()
        {
            player.SetPowerDown(true);
        }

        public void OnPowerDownUp()
        {
            player.SetPowerDown(false);
        }
    }
}