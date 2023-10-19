using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestXlab
{

	public class CloudController : MonoBehaviour
	{
		public Transform[] targets;
		public float moveSpeed = 10f;
		public Cloud cloud;

		private int m_targetIndex = 0;
		private bool m_moved = false;

		public void Action()
		{
			Debug.Log("CloudController - Try action!", this);
			
			if (m_moved)
			{
				return;
			}

			m_moved = true;
			cloud.StopFx();

			m_targetIndex++;
			if (m_targetIndex >= targets.Length)
			{
				m_targetIndex = 0;
			}
		}

		private void Update()
		{
			if (!m_moved)
			{
				return;
			}

			Transform target = targets[m_targetIndex];
			Vector3 targetPosition = new Vector3(target.position.x, cloud.transform.position.y, target.position.z);
			Vector3 offset = (targetPosition - cloud.transform.position).normalized * moveSpeed * Time.deltaTime;

			if (Vector3.Distance(cloud.transform.position, targetPosition) < offset.magnitude)
			{
				cloud.transform.position = targetPosition;
				m_moved = false;
				cloud.PlayFX();
			}
			else
			{
				cloud.transform.Translate(offset);
			}
		}
	}
}