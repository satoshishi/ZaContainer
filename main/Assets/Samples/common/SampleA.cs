using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZaCo.Core;
using ZaCo.Helper;

/// <summary>
/// ZaContainerに登録したいインスタンスは下記のアトリビュートをつけ、依存元を引数に指定する
/// </summary>
[InstallGameObjectToZaContainer(typeof(ISample))]
public class SampleA : MonoBehaviour,ISample
{
    /// <summary>
    /// ZaContainerへの登録処理が完了した後にコールバックが欲しい場合は、下記のアトリビュートをつけたメソッドを定義する
    /// ※コールバックが呼ばれるのは、ZaContainerに登録されているインスタスだけ
    /// </summary>
    /// <param name="container"></param>
    [ReceiveZaContainer]
    public void OnReceived(ReadonlyZaContainer container)
    {
        Debug.Log($"i({this.GetType()}) received container");
    }

    public void Say()
    {
        Debug.Log("my name is SampleA");
    }
}

public interface ISample
{
    void Say();
}
