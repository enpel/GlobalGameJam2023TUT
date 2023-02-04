
namespace Gekkou
{
    [System.Serializable]
    public struct SaveData
    {
        public float masterVolume;
        public float bgmVolume;
        public float seVolume;

        public SaveData(int num)
        {
            masterVolume = 1.0f;
            bgmVolume = 0.1f;
            seVolume = 1.0f;
        }

        public SaveData(float mas, float bgm, float se)
        {
            masterVolume = mas;
            bgmVolume = bgm;
            seVolume = se;
        }

        public SaveData(SaveData data)
        {
            this.masterVolume = data.masterVolume;
            this.bgmVolume = data.bgmVolume;
            this.seVolume = data.seVolume;
        }
    }
}
