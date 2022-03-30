namespace BLL.VideoCardComponent.Interface
{
    public interface IVideoCard
    {
        string ModelName { get; }


        int AmountVideoMemory { get; }
        int GPUPower { get; }


        bool CanPlayGame();
        bool CanWatchVideo();
    }
}
