﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickHandler : MonoBehaviour
{
    public UnityEvent downevent;

    private void OnMouseDown()
    {
        downevent?.Invoke();
    }
}
