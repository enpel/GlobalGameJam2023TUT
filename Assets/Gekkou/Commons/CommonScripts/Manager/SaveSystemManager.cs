
namespace Gekkou
{
    /// <summary>
    /// ゲームデータのやりとり、実態を持つ
    /// </summary>
    public class SaveSystemManager : SingletonMonobehavior<SaveSystemManager>
    {
        private SaveData saveData;

        public SaveData SaveData { get { return saveData; } }

        /* テンプレート
        public void Saving()
        {
            var data = new SaveData(saveData);

            //　セーブデータの上書きを行う

            Saving(data);

            saveData = new SaveData(data);
        }
        */

        public void Saving(float master, float bgm, float se)
        {
            var data = new SaveData(saveData);

            data.masterVolume = master;
            data.bgmVolume = bgm;
            data.seVolume = bgm;

            Saving(data);

            saveData = new SaveData(data);
        }

        public void Saving(int[] seed)
        {
            var data = new SaveData(saveData);

            for (int i = 0; i < data.seedParameters.Length; i++)
            {
                data.seedParameters[i] = seed[i];
            }

            Saving(data);

            saveData = new SaveData(data);
        }

        public void Saving(SaveData data)
        {
            SaveSystem<SaveData>.SavingGameData(data);
        }

        public void Loading()
        {
            if(!SaveSystem<SaveData>.LoadingGameData(ref saveData))
            {
                saveData = new SaveData(1.0f, 1.0f, 1.0f, new int[4]);
                SaveSystem<SaveData>.SavingGameData(saveData);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Loading();
        }
    }

}
