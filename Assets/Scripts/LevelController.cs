using System;
using System.Collections;
using System.Collections.Generic;
using TestXlab;
using UnityEngine;

namespace Golf
{

    public class LevelController : MonoBehaviour
    {
        public Spawner spawner;
		public SpawnerRing spawnerRing;
		private float m_lastSpawnedTime = 0;

		public float delayMax = 2f;
		public float delayMin = 0.5f;
		public float delayStep = 0.1f;

		public int score = 0;
        public int inexRing = 0;
        public int hightScore = 0;
        public int power = 40;

		private Vector3 positionRing = new Vector3(10, 1, -2);

        private float m_delay = 0.5f;

		private List<GameObject> m_stones = new List<GameObject>(16);
        private List<GameObject> m_rings = new List<GameObject>(16);

        public void ClearCloneObject()
		{
			foreach (var stone in m_stones)
			{
				Destroy(stone);
			}
            m_stones.Clear();
            foreach (var ring in m_rings)
            {
                Destroy(ring);
            }
            m_rings.Clear();
		}

		private void Awake()
		{
            
        }

		private void Start()
		{
            
            //m_lastSpawnedTime = Time.time;
            //RefreshDelay();
        }

		private void OnEnable()
		{
			//GameEvents.onCollisionRing += OnStickHit;
			GameEvents.onCollisionRing += OnRingHit;
            GameEvents.onStickHit += OnStickHit;
            score = 0;
			inexRing = 0;
            SpawnStone();
            SpawnRing();
        }

		private void OnDisable()
		{
			//GameEvents.onCollisionRing -= OnStickHit;
			GameEvents.onCollisionRing -= OnRingHit;
            GameEvents.onStickHit -= OnStickHit;
        }
        private void OnRingHit()
        {
            score++;
            hightScore = Mathf.Max(hightScore, score);            
            SpawnRing();			
            Debug.Log($"score: {score} - hightScore: {hightScore}");
        }
        private void OnStickHit()
		{			
			SpawnStone();			
		}

		public void SpawnStone()
		{
            var stone = spawner.Spawn();
            m_stones.Add(stone);
        }

        public void SpawnRing()
        {
			var ring = spawnerRing.SpawnRing();
            m_rings.Add(ring);
        }

        private void Update()
		{
			//if (Time.time >= m_lastSpawnedTime + m_delay)
			//{
			//	var stone = spawner.Spawn();
			//	m_stones.Add(stone);

			//	m_lastSpawnedTime = Time.time;

			//	RefreshDelay();
			//}
		}

		//public void RefreshDelay()
		//{
		//	m_delay = UnityEngine.Random.Range(delayMin, delayMax);
		//	delayMax = Mathf.Max(delayMin, delayMax - delayStep);
		//}


		IEnumerator WaitEvent(System.Action callBack)
		{
			yield return new WaitForSeconds(delayStep);
			callBack?.Invoke();
		}
	}
}