namespace education_domain
{
    public interface ITrainingProgramEfRepository
    {
        Task<List<TrainingProgram>> GetAllAsync();
        Task<bool> CreateAsync(TrainingProgram trainingProgram);
    }
}
