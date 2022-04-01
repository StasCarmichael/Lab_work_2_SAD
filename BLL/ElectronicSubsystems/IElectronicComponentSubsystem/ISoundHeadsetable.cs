using BLL.SoundHeadsetComponent.Interface;

namespace BLL.IElectronicComponentSubsystem
{
    public interface ISoundHeadsetable
    {
        bool SetSoundHeadset(ISoundHeadset soundHeadset);
        ISoundHeadset GetSoundHeadsetInfo();

        bool ListenMusic(int time);
    }
}
