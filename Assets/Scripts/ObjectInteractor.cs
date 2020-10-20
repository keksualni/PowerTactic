using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractor : MonoBehaviour
{
    public RectTransform SelectionBox;
    public List<GameObject> SelectedUnits = new List<GameObject>();

    private Camera _camera;
    private Vector2 _startSelectPos;
    private Vector2 _endSelectPos;
    private PlayerInfo _playerInfo;

    void Start()
    {
        _camera = GetComponent<Camera>();
        _playerInfo = GetComponent<PlayerInfo>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            DisableSelection();
            _startSelectPos = _endSelectPos = Input.mousePosition;
            ToggleSelectionBox(true);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            _endSelectPos = Input.mousePosition;
            TransformSelectionBox();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ToggleSelectionBox(false);
            ReleaseSelection();
        }
    }

    void ToggleSelectionBox(bool toggleMode)
    {
        SelectionBox.gameObject.SetActive(toggleMode);
    }

    void ReleaseSelection()
    {
        Vector2 minPos = SelectionBox.anchoredPosition - (SelectionBox.sizeDelta / 2);
        Vector2 maxPos = SelectionBox.anchoredPosition + (SelectionBox.sizeDelta / 2);

        foreach (GameObject unit in _playerInfo.Units)
        {
            Vector3 unitScreenPos = _camera.WorldToScreenPoint(unit.transform.position);

            if (
                unitScreenPos.x >= minPos.x
                && unitScreenPos.x <= maxPos.x
                && unitScreenPos.y >= minPos.y
                && unitScreenPos.y <= maxPos.y
            )
            {
                ToggleUnitSelection(unit);
                SelectedUnits.Add(unit);
            }
        }
    }

    void DisableSelection()
    {
        foreach (GameObject unit in SelectedUnits)
        {
            ToggleUnitSelection(unit);
        }

        SelectedUnits = new List<GameObject>();
    }

    void TransformSelectionBox()
    {
        float width = Math.Abs(_startSelectPos.x - _endSelectPos.x);
        float height = Math.Abs(_startSelectPos.y - _endSelectPos.y);

        var newPosition = new Vector2(
            (_startSelectPos.x + _endSelectPos.x) / 2,
            (_startSelectPos.y + _endSelectPos.y) / 2
        );

        SelectionBox.sizeDelta = new Vector2(width, height);
        SelectionBox.anchoredPosition = newPosition;
    }

    void ToggleUnitSelection(GameObject selectedObject)
    {
        var selectComponent = selectedObject.GetComponent<SelectableObject>();

        if (selectComponent)
        {
            selectComponent.SelectObject();
        }
    }
}
