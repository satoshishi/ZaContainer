using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZaCo.Helper;
using ZaCo.Core;

public class SampleBooter : MonoBehaviour
{
    [SerializeField]
    private List<string> m_sceneNames;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return SceneManager.LoadSceneAsync("Main",LoadSceneMode.Additive);
        var mainScene = SceneManager.GetSceneByName("Main");
        var installerParent = mainScene.GetRootGameObjects()[0].GetComponent<InstallHelper>();

        foreach(string sceneName in m_sceneNames)
        {
            yield return SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
            var subScene = SceneManager.GetSceneByName(sceneName);
            var installerChild = subScene.GetRootGameObjects()[0].GetComponent<InstallDecorator>();

            if(installerChild != null)
                installerParent.AddInstaller(installerChild);
        }
        
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Main"));
        installerParent.Handle(OnInstalled);
    }

    public void OnInstalled(ReadonlyZaContainer container)
    {
        SceneManager.UnloadSceneAsync("Boot");
    }
}
