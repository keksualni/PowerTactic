using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public GameObject SelectIndicator;

    public void SelectObject()
    {
        SelectIndicator.SetActive(!SelectIndicator.activeSelf);
    }
}
