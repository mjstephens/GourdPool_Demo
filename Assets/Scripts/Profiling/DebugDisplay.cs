using System.Text;
using TMPro;
using UnityEngine;

public class DebugDisplay : MonoBehaviour
{
    public TMP_Text objectsCount;
    public TMP_Text spawnedTotal;
    public TMP_Text recycles;
    public TMP_Text pooledUsed;
    public TMP_Text poolSize;
    
    
    
    // Update is called once per frame
    void Update()
    {
        if (PoolUtility.isPooled)
        {
            objectsCount.text = PoolUtility.currentObjectsCount.ToString();
            spawnedTotal.text = PoolUtility.totalSpawnedCount.ToString();
            recycles.text = PoolUtility.pool.recyclesCount.ToString();
            pooledUsed.text = PoolUtility.pool.pooledUseCount.ToString();
            poolSize.text = PoolUtility.pool.instanceCount.ToString();
        }
    }
}
