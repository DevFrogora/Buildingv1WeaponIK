using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemPicker : MonoBehaviour
{
    [SerializeField] private Transform _pickerPoint;
    [SerializeField] private float _pickerPointRadius = 0.5f;
    [SerializeField] private LayerMask _pickerLayerMask;

    private readonly Collider[] _colliders = new Collider[50]; // 3 collider (item ) we are checking
    [SerializeField] private int _numFound; // number of collider(item found)

    //template For Picker UI // rename it later
    public Image itemUIPrefab;
    public GameObject pickerUIContainer;

    public Dictionary<int,Image> itemsUIlist = new Dictionary<int,Image>();

    int previousItemCount;
    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_pickerPoint.position, _pickerPointRadius,
            _colliders, _pickerLayerMask);

        if (_numFound > 0)
        {
            if(previousItemCount != _numFound)
            {
                clearItemFoundList();
                for (int i = 0; i < _numFound; i++)
                {
                    var pickerItem = _colliders[i].GetComponent<IInventoryItem>();
                    if (itemsUIlist.ContainsKey(pickerItem.ItemId))
                    {

                    }
                    else
                    {
                        if(itemsUIlist.Count < _numFound)
                        {
                            var itemUI = Instantiate(itemUIPrefab);
                            PickerItemUI pickedItemData= itemUI.GetComponent<PickerItemUI>();
                            itemUI.gameObject.transform.SetParent(pickerUIContainer.transform);
                            itemUI.rectTransform.localScale = itemUIPrefab.transform.localScale;
                            itemUI.rectTransform.rotation = Quaternion.identity;
                            pickedItemData.image.sprite = pickerItem.spriteImage;
                            pickedItemData.itemName.text = pickerItem.Name;
                            pickedItemData.itemPrefab = _colliders[i].gameObject;
                            pickedItemData.itemId = pickedItemData.itemPrefab.GetComponent<IInventoryItem>().ItemId;

                            itemsUIlist.Add(pickedItemData.itemId ,itemUI);

                            Debug.Log("From Interactor");
                        }

                    }
                }
                previousItemCount = _numFound;
            }

        }
        else
        {
            clearItemFoundList();
        }
    }


    void clearItemFoundList()
    {
        previousItemCount = 0;
        foreach (var item in itemsUIlist)
        {
            // Destroy object.
            Destroy(item.Value.gameObject);
        }
        itemsUIlist.Clear();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_pickerPoint.position, _pickerPointRadius);
    }
}

