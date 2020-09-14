using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class DefaultObjectSpawner : MonoBehaviour
    {
        #region Variables
        
            [Header("References")]
            [SerializeField]
            [Tooltip("The prefab to spawn.")]
            private GameObject _spawnObject;
        
            [SerializeField]
            [Tooltip("The transform from where the spawnedObject will be spawned.")]
            private Transform _spawnPoint;

            private readonly float _spawnFrequency = 0.05f;
            
            /// <summary>
            /// Used to pace the spawning interval.
            /// </summary>
            private float _timeRef;
        
            #endregion Variables
        
        
            #region Initialization
        
            private void OnEnable()
            {
                PoolUtility.isPooled = false;
                StartCoroutine(nameof(SpawnLoop));
            }
        
            private void OnDisable()
            {
                StopCoroutine(nameof(SpawnLoop));
            }
        
            #endregion Initialization
        
        
            #region Spawning
        
            private IEnumerator SpawnLoop()
            {
                while (true)
                {
                    if (Time.time - _timeRef > _spawnFrequency)
                    {
                        _timeRef = Time.time;
                        SpawnObject(_spawnObject, _spawnPoint);
                    }
                    yield return null;
                }
            }
        
            /// <summary>
            /// Spawns a new instance of @obj
            /// </summary>
            private static void SpawnObject(GameObject obj, Transform spawnPos)
            {
                Instantiate(obj, spawnPos.position, Quaternion.identity);
            }
        
            #endregion Spawning
    }
}