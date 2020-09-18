using System.Collections;
using GourdPool;
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
    
    [SerializeField]
    [Tooltip("The transform from where the spawnedObject will be spawned when burst.")]
    private Transform _burstSpawnPoint;

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
        PoolUtility.pool = Pool.GetPoolForObject(_spawnObject);
        PoolUtility.isPooled = true;
        
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
            Pool.Pooled(obj, spawnPos.position, Quaternion.identity);
        }
        else
        {
            Instantiate(obj, spawnPos.position, Quaternion.identity);
        }
    }

    public void Burst()
    {
        for (int i = 0; i < 50; i++)
        {
            SpawnObject(_spawnObject, _burstSpawnPoint, true);
        }
    }

    #endregion Spawning


    #region Pool Options

    public void SetPoolCapacity(int min, int max)
    {
        Pool.SetObjectPoolCapacity(_spawnObject, min, max);
    }

    public void SetPoolSpilloverAllowance(int allowance)
    {
        Pool.SetObjectPoolSpilloverAllowance(_spawnObject, allowance);
    }

    #endregion Pool Options
}
