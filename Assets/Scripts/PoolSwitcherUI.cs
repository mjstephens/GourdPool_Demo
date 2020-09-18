using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoolSwitcherUI : MonoBehaviour
{
    #region Variables

    [Header("References")]
    [SerializeField]
    private TMP_Text _poolActiveLabel;
    [SerializeField]
    private TMP_Text _togglePoolButtonText;
    [SerializeField]
    private GameObject _poolPanel;
    [SerializeField]
    private GameObject _notPoolPanel;
    [SerializeField]
    private PooledObjectSpawner _pooledSpawner;
    [SerializeField]
    private GameObject _defaultSpawner;
    [SerializeField]
    private CanvasGroup _statsCanvas;

    private bool _isUsingPool = true;
    
    #endregion Variables


    #region UI

    public void OnPoolToggled()
    {
        _isUsingPool = !_isUsingPool;
        
        _notPoolPanel.SetActive(!_isUsingPool);
        _poolPanel.SetActive(_isUsingPool);
        _pooledSpawner.gameObject.SetActive(_isUsingPool);
        _defaultSpawner.SetActive(!_isUsingPool);
        _togglePoolButtonText.text = _isUsingPool ? "TOGGLE POOL OFF" : "TOGGLE POOL ON";
        _poolActiveLabel.text = _isUsingPool ? "POOL ENABLED" : "POOL DISABLED";
        _poolActiveLabel.color = _isUsingPool ? Color.green : Color.red;
        _statsCanvas.alpha = _isUsingPool ? 1 : 0.25f;

        PoolUtility.totalSpawnedCount = 0;
        GourdPool.Pool.DeleteGameObjectPool(_pooledSpawner._spawnObject);
        PoolUtility.pool = GourdPool.Pool.GetPoolForObject(_pooledSpawner._spawnObject);
        
        ObjectCollection.OnToggle();
        PoolUtility.isPooled = _isUsingPool;
        PoolUtility.currentObjectsCount = 0;
    }

    public void OnResetButtonPressed()
    {
        PoolUtility.totalSpawnedCount = 0;
        GourdPool.Pool.DeleteGameObjectPool(_pooledSpawner._spawnObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion UI
}
