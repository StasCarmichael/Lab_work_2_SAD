namespace BLL.SoundHeadsetComponent.Interface
{
    public interface ISoundHeadset
    {
        string Type { get; }
        string ModelName { get; }


        bool IsUserOnly();
    }
}
