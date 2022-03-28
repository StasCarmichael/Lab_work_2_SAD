using BLL.SoundHeadsetComponent.Interface;

namespace BLL.IElectronicComponentSubsystem
{
    public interface ISoundHeadsetable
    {
        bool SetSoundHeadset(ISoundHeadset soundHeadset);

        bool ListenMusic(int time);
    }
}
