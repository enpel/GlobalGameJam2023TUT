
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
            SaveSystemPlayerPrefs<SaveData>.SavingGameData(data);
        }

        public void Saving(string playername)
        {
            var data = new SaveData(saveData);

            data.playerName = playername;

            Saving(data);

            saveData = new SaveData(data);
        }

        public void SavingWindowSize(int windowsize)
        {
            var data = new SaveData(saveData);

            data.windowSize = windowsize;

            Saving(data);

            saveData = new SaveData(data);
        }


        public void SavingMouseSensitivity(float sensi)
        {
            var data = new SaveData(saveData);

            data.mouseDouble = sensi;

            Saving(data);

            saveData = new SaveData(data);
        }

        public void Loading()
        {
            saveData = SaveSystemPlayerPrefs<SaveData>.LoadingGameData();
        }

        protected override void Awake()
        {
            base.Awake();
            Loading();
        }
    }

}
