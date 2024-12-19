using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisableManager : MonoBehaviour
{

    [SerializeField] private int distanceFromPlayer;
    
    private GameObject _player;
    
    public List<ActivatorItem> _activatorItems;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        
        _activatorItems = new List<ActivatorItem>();
        
        StartCoroutine("CheckActivation");
    }

    private void Update()
    {
        foreach (ActivatorItem item in _activatorItems)
        {
            item.itemPos = item.item.transform.position;
        }
    }

    public IEnumerator CheckActivation()
    {
        List<ActivatorItem> removeList = new List<ActivatorItem>();

        if (_activatorItems.Count > 0)
        {
            foreach (ActivatorItem item in _activatorItems)
            {
                if (Vector3.Distance(_player.transform.position, item.itemPos) > distanceFromPlayer)
                {
                    if (item.item == null)
                    {
                        removeList.Add(item);
                    }
                    else
                    {
                        item.item.SetActive(false);
                    }
                }
                else
                {
                    if (item.item == null)
                    {
                        removeList.Add(item);
                    }
                    else
                    {
                        item.item.SetActive(true);
                    }
                }
            }
        }

        yield return new WaitForSeconds(0.01f);
        
        
        // Prevent the object destroyed incorrectly reactivate
        if (removeList.Count > 0)
        {
            foreach (ActivatorItem item in removeList)
            {
                _activatorItems.Remove(item);
            }
        }
        
        yield return new WaitForSeconds(0.01f);

        StartCoroutine("CheckActivation");
    }
}

public class ActivatorItem
{
    public GameObject item;
    public Vector3 itemPos;
}