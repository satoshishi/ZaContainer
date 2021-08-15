using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZaCo.Helper;
using ZaCo.Core;

public class ScriptableSampleMain : MonoBehaviour
{
    [SerializeField]
    private InstallHelper installHelper;

    // Start is called before the first frame update
    void Start()
    {
        installHelper.Handle(null);   
    }
}
