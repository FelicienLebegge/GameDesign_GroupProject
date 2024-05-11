using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{

    [SerializeField]
    private int _beanCount = 20;

    [SerializeField]
    private GameObject[] _beans;

    [SerializeField]
    private GameObject _dirt;
    [SerializeField]
    Transform _dirttransform;

    private GameObject _dirtInstance;

    [SerializeField]
    private float _spawnRadius = 5;

    [SerializeField]
    private GameObject _panParent;

    private float _dirtPivotAdjustment = 1.5f;

    void Start()
    {
        SpawnDirt();
    }

    // Update is called once per frame
    void Update()
    {

       if(KitchenStates.KitchenState == KitchenStates.CookingStation.Cutting && KitchenStates.AreBeansWashed)
        {
            Debug.Log("true");
            Destroy(_dirtInstance);
        }
       if(KitchenStates.IsOrderCompleted || KitchenStates.IsTrashed)
        {
            SpawnDirt();
        }
    }

    
    private void SpawnDirt()
    {
        
        Vector3 targetDirtPostion = new Vector3(_dirttransform.position.x, _dirtPivotAdjustment, _dirttransform.position.z);
        _dirtInstance =  Instantiate(_dirt, targetDirtPostion, Quaternion.identity);
        SpawnBeans();
        KitchenStates.IsOrderCompleted = false;
    }
     
    private void SpawnBeans()
    {
        for (int i=0; i< _beanCount; i++)
        {
            Vector3 pos = Random.insideUnitSphere * _spawnRadius;
            int x = Random.Range(0, _beanCount);
            GameObject bean = Instantiate(_beans[x], _dirttransform.position + pos, Quaternion.identity);
            //bean.transform.SetParent(_panParent.transform); //keeps it clean but give replay issues
        }
    }
}
