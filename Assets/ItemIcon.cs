using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Canvas _parentCanvas;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    [NonSerialized] public ItemData itemData;
    [NonSerialized] public ItemSlot parentItemSlot;

    private bool _isDragging;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _parentCanvas = GetComponentInParent<Canvas>();
    }

    private void Update()
    {
        if (_isDragging || parentItemSlot == null) return;

        transform.parent = parentItemSlot.transform;
        var dist = Vector3.Distance(transform.position, parentItemSlot.transform.position);

        if (!Mathf.Approximately(dist, 0f))
            transform.position =
                Vector3.Lerp(transform.position, parentItemSlot.transform.position, Time.deltaTime * 10);
    }

    public void SetItemIcon(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;
        _canvasGroup.blocksRaycasts = false;
        _rectTransform.parent = _parentCanvas.transform;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
        _canvasGroup.blocksRaycasts = true;
    }
}