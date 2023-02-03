using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gekkou
{

    public class SceneSystemManager : SingletonMonobehavior<SceneSystemManager>
    {
        [SerializeField]
        private float fadeTime = 1.0f;

        [SerializeField]
        private float sceneLoadDelay = 1.0f;

        private GameUIManager gameUI;

        public void SceneLoading(string sceneName)
        {
            if (gameUI == null)
                gameUI = GameUIManager.Instance;

            StartCoroutine(ISceneLoading(sceneName));
        }

        private IEnumerator ISceneLoading(string sceneName)
        {
            yield return StartCoroutine(gameUI.FadeIn(fadeTime));
            gameUI.ChangeEnableLoadingMenu(true);

            var async = SceneManager.LoadSceneAsync(sceneName);

            while (!async.isDone)
            {
                gameUI.ChangeSlider(async.progress);
                yield return null;
            }
            gameUI.ChangeSlider(1.0f);

            yield return new WaitForSeconds(sceneLoadDelay);

            gameUI.ChangeEnableLoadingMenu(false);
            yield return StartCoroutine(gameUI.FadeOut(fadeTime));
        }
    }

}
