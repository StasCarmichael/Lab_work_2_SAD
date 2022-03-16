using BLL.SoundHeadsetComponent.Interface;

namespace BLL.SoundHeadsetComponent.Class
{
    public sealed class Headphones : ISoundHeadset
    {
        public Headphones(string modelName)
        {
            Type = "Headphones";
            ModelName = modelName;
        }


        public string Type { get; private set; }

        public string ModelName { get; private set; }

        public bool IsUserOnly() { return true; }

    }
}
