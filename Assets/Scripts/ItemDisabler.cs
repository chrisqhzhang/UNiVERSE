using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisabler : MonoBehaviour
{
    private GameObject _gameObject;

    private ItemDisableManager _itemDisableManager;

    private void Start()
    {
        _gameObject = GameObject.FindWithTag("Collectable");
        
        _itemDisableManager = _gameObject.GetComponent<ItemDisableManager>();

        StartCoroutine("AddToList");
    }

    IEnumerator AddToList()
    {
        yield return new WaitForSeconds(0.1f);
        
        _itemDisableManager._activatorItems.Add(new ActivatorItem { item = _gameObject, itemPos = _gameObject.transform.position });
    }
}
