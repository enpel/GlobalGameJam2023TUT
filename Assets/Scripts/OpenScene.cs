#if UNITY_EDITOR
using UnityEditor;

namespace Gekkou
{
    public class OpenScene
    { 
        
        [MenuItem("Tools/Gekkou/OpenScene/saltn Scenes")]
        private static void OpensaltnScenesScene()
        {
            // シーンの変更があった場合にどうするか聞く
            if (UnityEditor.SceneManagement.EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/saltn Scenes.unity");
            }
        }


        [MenuItem("Tools/Gekkou/OpenScene/SampleScene")]
        private static void OpenSampleSceneScene()
        {
            // シーンの変更があった場合にどうするか聞く
            if (UnityEditor.SceneManagement.EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/SampleScene.unity");
            }
        }


        [MenuItem("Tools/Gekkou/OpenScene/SampleScene2")]
        private static void OpenSampleScene2Scene()
        {
            // シーンの変更があった場合にどうするか聞く
            if (UnityEditor.SceneManagement.EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Gekkou/Commons/SampleScenes/SampleScene2.unity");
            }
        }


    }
}
#endif
