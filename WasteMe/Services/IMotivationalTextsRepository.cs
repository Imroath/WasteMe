namespace WasteMe.Services
{
    public interface IMotivationalTextsRepository
    {
        string GetRandomTextForWasteType(string wasteType);
    }
}
