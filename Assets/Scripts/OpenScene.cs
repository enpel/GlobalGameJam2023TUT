#if UNITY_EDITOR
using UnityEditor;

namespace Gekkou
{
    public class OpenScene
    { 
        [MenuItem("Tools/Gekkou/OpenScene/SampleScene")]
        private static void OpenSampleSceneScene()
        {
            // シーンの変更があった場合にどうするか聞く
            if (UnityEditor.SceneManagement.EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Commons/SampleScenes/SampleScene.unity");
            }
        }


    }
}
#endif
