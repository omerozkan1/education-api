namespace education_domain
{
    public interface ITrainingEfRepository
    {
        Task<List<Training>> GetAllAsync();
        Task<bool> CreateAsync(Training training);
    }
}
