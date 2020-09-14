using System.Collections;
using UnityEngine;

public class PooledObjectSpawner : MonoBehaviour
{
    #region Variables

    [Header("References")]
    [Tooltip("The prefab to spawn.")]
    public GameObject _spawnObject;

    [SerializeField]
    [Tooltip("The transform from where the spawnedObject will be spawned.")]
    private Transform _spawnPoint;

    public float spawnFrequency { private get; set; }
    
    /// <summary>
    /// Used to pace the spawning interval.
    /// </summary>
    private float _timeRef;

    #endregion Variables


    #region Initialization

    private void Awake()
    {
        spawnFrequency = 0.05f;
        
    }

    private void OnEnable()
    {
        PoolUtility.pool = GourdPool.GourdPool.GetPoolForObject(_spawnObject);
        PoolUtility.isPooled = true;
        //GourdPool.GourdPool.SetObjectPoolCapacity(_spawnObject, poolMin, poolMax);
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
            if (Time.time - _timeRef > spawnFrequency)
            {
                _timeRef = Time.time;
                SpawnObject(_spawnObject, _spawnPoint, true);
            }
            yield return null;
        }
    }

    /// <summary>
    /// Spawns a new instance of @obj
    /// </summary>
    private static void SpawnObject(GameObject obj, Transform spawnPos, bool pooled)
    {
        if (pooled)
        {
            GourdPool.GourdPool.Pooled(obj, spawnPos.position, Quaternion.identity);
        }
        else
        {
            Instantiate(obj, spawnPos.position, Quaternion.identity);
        }
    }

    #endregion Spawning


    #region Pool Options

    public void SetPoolCapacity(int min, int max)
    {
        GourdPool.GourdPool.SetObjectPoolCapacity(_spawnObject, min, max);
    }

    #endregion Pool Options
}
