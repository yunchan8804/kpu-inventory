using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    private RectTransform _rectTransform;

    public int index;
    public Action<int> onRemoveItem;
    public Action<int, string> onAddItem;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        var itemIcon = eventData.pointerDrag.GetComponent<ItemIcon>();

        if (itemIcon == null) return;

        if (itemIcon.parentItemSlot != null)
            itemIcon.parentItemSlot.onRemoveItem?.Invoke(itemIcon.parentItemSlot.index);
        
        itemIcon.parentItemSlot = this;
        onAddItem?.Invoke(index, itemIcon.itemData.itemName);

        var targetRect = itemIcon.GetComponent<RectTransform>();
        targetRect.position = _rectTransform.position;
        targetRect.parent = transform;
    }
}