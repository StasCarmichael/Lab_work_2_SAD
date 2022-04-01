using BLL.SoundHeadsetComponent.Interface;

namespace BLL.SoundHeadsetComponent.Class
{
    public sealed class Speaker : ISoundHeadset
    {
        public Speaker(string modelName)
        {
            Type = "Speaker";
            ModelName = modelName;
        }


        public string Type { get; private set; }

        public string ModelName { get; private set; }

        public bool IsUserOnly() { return false; }

    }
}
