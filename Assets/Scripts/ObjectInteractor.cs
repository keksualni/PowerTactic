using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractor : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                OnObjectSelect(hit.transform.gameObject);
            }
        }
    }

    void OnObjectSelect(GameObject selectedObject)
    {
        var selectComponent = selectedObject.GetComponent<SelectableObject>();

        if (selectComponent)
        {
            selectComponent.SelectObject();
        }
    }
}
