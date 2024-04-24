
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
    private GameObject _mud;
    [SerializeField]
    private float _spawnRadius = 5;
    // Start is called before the first frame update
    void Awake()
    {
        SpawnMud();
    }

    // Update is called once per frame
    void Update()
    {
        if(_orderComplete)
        {
            SpawnMud();
        }

    }

    private void SpawnMud()
    {
        Instantiate(_mud, transform.position, Quaternion.identity);
        SpawnBeans();
        
    }

    private void SpawnBeans()
    {
        
        for (int i=0; i< _beanCount; i++)
        {
            Vector3 pos = Random.insideUnitSphere * _spawnRadius;
            int x = Random.Range(0, _beanCount -1);
            Instantiate(_beans[x], transform.position + pos, Quaternion.identity);   
        }
    }
}
