
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

        public void Saving(SaveData data)
        {
            SaveSystem<SaveData>.SavingGameData(data);
        }

        public void Loading()
        {
             if(!SaveSystem<SaveData>.LoadingGameData(ref saveData))
            {
                saveData = new SaveData(1.0f, 1.0f, 1.0f);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Loading();
        }
    }

}
