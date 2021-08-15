using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZaCo.Helper
{
    [CreateAssetMenu(menuName = "ZaCo/PrefabObjects")]
    public class ZaCoInstallPrefabObjects : ScriptableObject
    {
        public List<GameObject> prefabs;
    }
}