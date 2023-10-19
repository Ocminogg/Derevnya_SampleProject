using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class SpawnerRing : MonoBehaviour
    {
        public GameObject[] prefabs;
        private Vector3 position;
        public GameObject SpawnRing()
        {
            var prefab = GetRandomPrefab();

            if (prefab == null)
            {
                Debug.LogError("Spawner - prefab == null");
                return null;
            }
            
            int x = Random.Range(37, 47);
            int y = Random.Range(33, 41);
            int z = Random.Range(31, 52);
            position = new Vector3(x,y,z);
            return Instantiate(prefab, position, Quaternion.Euler(0,0,90));
        }

        private GameObject GetRandomPrefab()
        {
            if (prefabs.Length == 0)
            {
                Debug.LogError("Spawner - prefabs is empty!");
                return null;
            }

            int index = Random.Range(0, prefabs.Length);
            return prefabs[index];
        }
    }
}
