using BLL.VideoCardComponent.Interface;

namespace BLL.IElectronicComponentSubsystem
{
    public interface IVideoCardable
    {
        IVideoCard GetVideoCardInfo();

        bool InstallNewVideoCard(IVideoCard videoCard);
    }
}
