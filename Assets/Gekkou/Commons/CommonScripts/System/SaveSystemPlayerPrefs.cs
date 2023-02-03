using UnityEngine;

namespace Gekkou
{

    /// <summary>
    /// ゲームデータの書き込み、読み込みを行う
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SaveSystemPlayerPrefs<T> where T : struct
    {
        private static readonly string SAVEKEY = "SAVEDATA";

        public static void SavingGameData(T argData)
        {
            // json化
            var json = JsonUtility.ToJson(argData);

            // 保存
            PlayerPrefs.SetString(SAVEKEY, json);

            // 強制保存
            PlayerPrefs.Save();

            Log.Info("Save Data Saving.");
        }

        public static T LoadingGameData()
        {
            if (PlayerPrefs.HasKey(SAVEKEY))
            {
                // セーブデータが存在する
                var json = PlayerPrefs.GetString(SAVEKEY);

                Log.Info("Save Data Loading.");

                // T化
                return JsonUtility.FromJson<T>(json);
            }
            else
            {
                Log.Info("Save Data Creating.");

                // セーブデータが存在しない
                return new T();
            }
        }
    }

}
