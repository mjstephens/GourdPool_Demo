using System;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class PooledObjectInstance : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private Renderer _renderer;

    public Color instantiatedColor;
    public Color pooledColor;

    private int _enabledCount;
    
    #endregion Variables
    
    
    #region Initialization

    private void Awake()
    {
        PoolUtility.totalSpawnedCount++;
        _renderer.material.color = instantiatedColor;
        ObjectCollection.collection.Add(this);
    }

    private void OnEnable()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = new Vector3(
            Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50));
        PoolUtility.currentObjectsCount++;

        _enabledCount++;
        if (PoolUtility.isPooled && _enabledCount > 2)
        {
            _renderer.material.color = pooledColor;
        }
    }

    private void OnDisable()
    {
        PoolUtility.currentObjectsCount--;
    }

    private void OnDestroy()
    {
        if (!PoolUtility.isPooled)
            PoolUtility.currentObjectsCount--;
        ObjectCollection.collection.Remove(this);
    }

    #endregion Initialization
    
    
    #region Despawn

    private void OnTriggerEnter(Collider other)
    {
        if (PoolUtility.isPooled)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DestroyFromCollection()
    {
        Destroy(gameObject);
    }

    #endregion Despawn
}
