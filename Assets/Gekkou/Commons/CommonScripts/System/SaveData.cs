
namespace Gekkou
{
    [System.Serializable]
    public struct SaveData
    {
        public int windowSize;
        public float masterVolume;
        public float bgmVolume;
        public float seVolume;
        public string playerName;
        public float mouseDouble;

        public SaveData(int num)
        {
            windowSize = 0;
            masterVolume = 1.0f;
            bgmVolume = 0.1f;
            seVolume = 1.0f;
            playerName = "";
            mouseDouble = 1.0f;
        }

        public SaveData(int wsize, float mas, float bgm, float se, string pname, float mdouble)
        {
            windowSize = wsize;
            masterVolume = mas;
            bgmVolume = bgm;
            seVolume = se;
            playerName = pname;
            mouseDouble = mdouble;
        }

        public SaveData(SaveData data)
        {
            this.windowSize = data.windowSize;
            this.masterVolume = data.masterVolume;
            this.bgmVolume = data.bgmVolume;
            this.seVolume = data.seVolume;
            this.playerName = data.playerName;
            this.mouseDouble = data.mouseDouble;
        }
    }
}
