using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZaCo.Core;
using ZaCo.Helper;

public interface ISample
{
    void hoge(string name);
}

public class SampleA : ISample
{
    [ReceiveZaContainer]
    public void OnInstalled(ReadonlyZaContainer container)
    {
        hoge(container.Get<SamplePrefab>().name);
    }

    public void hoge(string name)=> Debug.Log(name);
}

public class SampleInstallerA : InstallDecorator
{
    public override ZaContainer Install(ZaContainer container)
    {
        container.Register<ISample>(new SampleA());
        return container;
    }
}
