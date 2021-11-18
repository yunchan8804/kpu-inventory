using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public ItemDatabase itemDatabase;
    public ItemIcon itemIconPrefab;
    public Canvas uiCanvas;
    public Canvas infoCanvas;
    public Transform chestUiTransform;

    public List<string> items;

    private ItemSlot[] _itemSlots;

    private bool _isOpen;
    private bool _isPlayerNearby;
    private List<ItemIcon> _itemIcons;

    private void Awake()
    {
        _itemSlots = chestUiTransform.GetComponentsInChildren<ItemSlot>();
        for (var i = 0; i < _itemSlots.Length; i++)
        {
            var itemSlot = _itemSlots[i];
            itemSlot.index = i;
            itemSlot.onAddItem = OnAddItem;
            itemSlot.onRemoveItem = OnRemoveItem;
        }

        _itemIcons = new List<ItemIcon>();
    }

    void Start()
    {
        infoCanvas.gameObject.SetActive(false);
        uiCanvas.gameObject.SetActive(false);

        for (var i = 0; i < items.Count; i++)
        {
            if (string.IsNullOrEmpty(items[i]) || itemDatabase.itemDatas.All(data => data.itemName != items[i]))
                continue;

            var itemData = itemDatabase.itemDatas.FirstOrDefault(data => data.itemName == items[i]);

            var icon = Instantiate(itemIconPrefab, uiCanvas.transform);
            icon.SetItemIcon(itemData.itemImage);
            icon.itemData = itemData;
            icon.parentItemSlot = _itemSlots[i];
            icon.transform.parent = _itemSlots[i].transform;
            icon.transform.localPosition = new Vector3(0, 0, -1);

            _itemIcons.Add(icon);
        }
    }

    void Update()
    {
        if (!_isPlayerNearby) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;
        _isOpen = !_isOpen;
        uiCanvas.gameObject.SetActive(_isOpen);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();
        if (player == null) return;
        _isPlayerNearby = true;
        infoCanvas.gameObject.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();
        if (player == null) return;
        _isPlayerNearby = false;
        infoCanvas.gameObject.SetActive(false);
        uiCanvas.gameObject.SetActive(false);
    }

    void OnAddItem(int index, string itemName)
    {
        items[index] = itemName;
    }

    void OnRemoveItem(int index)
    {
        items[index] = string.Empty;
    }
}