using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    [SerializeField]
    private bool _orderComplete;

    [SerializeField]
    private int _beanCount = 20;

    [SerializeField]
    private GameObject[] _beans;

    [SerializeField]
    private GameObject _dirt;

    [SerializeField]
    private float _spawnRadius = 5;

    private float _dirtPivotAdjustment = 1.5f;

    // Start is called before the first frame update
    void Awake()
    {
        SpawnDirt();
    }

    // Update is called once per frame
    void Update()
    {
        if(_orderComplete)
        {
            SpawnDirt();
        }

    }

    private void SpawnDirt()
    {
        Vector3 targetDirtPostion = new Vector3(transform.position.x, _dirtPivotAdjustment, transform.position.z);
        Instantiate(_dirt, targetDirtPostion, Quaternion.identity);
        SpawnBeans();
        
    }

    private void SpawnBeans()
    {
        
        for (int i=0; i< _beanCount; i++)
        {
            Vector3 pos = Random.insideUnitSphere * _spawnRadius;
            int x = Random.Range(0, _beanCount -1);
            GameObject bean = Instantiate(_beans[x], transform.position + pos, Quaternion.identity);
            bean.transform.SetParent(transform); //keeps it clean
        }
    }
}
