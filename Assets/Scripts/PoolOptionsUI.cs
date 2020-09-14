using System.Collections;
using System.Collections.Generic;
using GourdPool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PoolOptionsUI : MonoBehaviour
{
    #region Variables

    [Header("References")]
    [SerializeField]
    private TMP_Text _spawnDelayValueText;
    [SerializeField]
    private TMP_Text _minCapacityValueText;
    [SerializeField]
    private TMP_Text _maxCapacityValueText;

    [SerializeField] 
    private Slider _minCapacitySlider;
    [SerializeField] 
    private Slider _maxCapacitySlider;

    [SerializeField]
    private PooledObjectSpawner _spawner;

    private int poolMinCapacity = 0;
    private int poolMaxCapacity = -1;

    #endregion Variables


    #region UI

    public void OnSpawnDelaySliderSet(float value)
    {
        _spawnDelayValueText.text = value.ToString("F2");
        _spawner.spawnFrequency = value;
    }

    public void OnPoolMinCapacitySliderSet(float value)
    {
        AdjustCapacityValues((int) value, (int) _maxCapacitySlider.value);
    }
    
    public void OnPoolMaxCapacitySliderSet(float value)
    {
        AdjustCapacityValues((int) _minCapacitySlider.value, (int) value);
    }

    public void OnCleanPoolSelected()
    {
        (PoolUtility.pool as IPool).Clean();
    }

    public void OnResetPoolSelected()
    {
        (PoolUtility.pool as IPool).Clear();
    }

    #endregion UI


    #region Capacity

    private void AdjustCapacityValues(int inMin, int inMax)
    {
        if (inMin > 0 && inMax > 0)
        {
            inMax = Mathf.Max(inMax, inMin + 1);
        }

        if (inMax == 0)
        {
            inMax = -1;
        }
        
        poolMinCapacity = inMin;
        poolMaxCapacity = inMax;
        _minCapacityValueText.text = poolMinCapacity.ToString();
        _maxCapacityValueText.text = poolMaxCapacity.ToString();
        _minCapacitySlider.value = poolMinCapacity;
        _maxCapacitySlider.value = poolMaxCapacity;
        
        _spawner.SetPoolCapacity(poolMinCapacity, poolMaxCapacity);
    }

    #endregion Capacity
}
