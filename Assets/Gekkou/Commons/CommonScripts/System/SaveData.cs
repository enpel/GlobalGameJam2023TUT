
namespace Gekkou
{
    [System.Serializable]
    public struct SaveData
    {
        public float masterVolume;
        public float bgmVolume;
        public float seVolume;
        public int[] seedParameters;

        public SaveData(int num)
        {
            masterVolume = 1.0f;
            bgmVolume = 0.1f;
            seVolume = 1.0f;
            seedParameters = new int[4];
        }

        public SaveData(float mas, float bgm, float se, int[] seed)
        {
            masterVolume = mas;
            bgmVolume = bgm;
            seVolume = se;
            seedParameters = seed;
        }

        public SaveData(SaveData data)
        {
            this.masterVolume = data.masterVolume;
            this.bgmVolume = data.bgmVolume;
            this.seVolume = data.seVolume;
            this.seedParameters = data.seedParameters;
        }
    }
}
