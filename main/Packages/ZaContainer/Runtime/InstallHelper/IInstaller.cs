using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZaCo.Core;

namespace ZaCo.Helper
{
    public interface IInstaller
    {
        void Handle(ZaContainer container);
    }
}