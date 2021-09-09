using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZaCo.Helper;
using ZaCo.Core;

public class SimplySample : MonoBehaviour
{
    [SerializeField]
    private InstallHelper installHelper;

    // Start is called before the first frame update
    void Start()
    {
        //InstallHelperからHandleを呼び出すと設定されているInstallerからContainerへの注入処理が開始する
        installHelper.Handle(OnReceived);
    }

    /// <summary>
    /// Containerへの注入が完了したタイミングでコールバックされる
    /// このメソッドがコールされる前に、Containerにキャッシュされている中でReceiveZaContainerアトリビュートが付いているスクリプトに対してのコールバックが完了している
    /// </summary>
    /// <param name="container"></param>
    private void OnReceived(ReadonlyZaContainer container)
    {
        //Containerに共通の依存先のインスタンスが複数存在する場合、idから指定することができる
        container.Get<ISample>("a").Say();

        //またインデックスからも指定できる
        container.Get<ISample>(1).Say();        
    }
}
