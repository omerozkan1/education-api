using education_domain;

namespace education_application
{
    public static class Mapper
    {
        public static GetAllTrainingDto ToDto(this Training training)
        {
            if (training != null)
            {
                return new GetAllTrainingDto
                {
                    Id = training.Id,
                    Description = training.Description,
                    Link = training.Link,
                    Name = training.Name,
                    TrainingProgram = new GetAllTrainingProgramDto 
                    {
                        Id = training.TrainingProgram.Id, 
                        Name = training.TrainingProgram.Name,
                        EndDate = training.TrainingProgram.EndDate,
                        StartDate = training.TrainingProgram.StartDate,
                        Status = training.TrainingProgram.Status
                    },

                };
            }
            return null;
        }

        public static GetAllTrainingProgramDto ToDto(this TrainingProgram trainingProgram)
        {
            if (trainingProgram != null)
            {
                return new GetAllTrainingProgramDto
                {
                    Name = trainingProgram.Name,
                    Id = trainingProgram.Id,
                    StartDate = trainingProgram.StartDate,
                    EndDate = trainingProgram.EndDate,
                    Status = trainingProgram.Status,
                    Trainings = trainingProgram.Trainings.Select(t => new GetAllTrainingDto
                    {
                        Id = t.Id,
                        Description = t.Description,
                        Link = t.Link,
                        Name = t.Name
                    }).ToList()
                };
            }
            return null;
        }

        public static Training ToEntity(this CreateTrainingDto training)
        {
            if (training != null)
            {
                return new Training
                {
                    Description = training.Description,
                    Link = training.Link,
                    Name = training.Name,
                    TrainingProgramId = training.TrainingProgramId
                };
            }
            return null;
        }

        public static TrainingProgram ToEntity(this CreateTrainingProgramDto trainingProgram)
        {
            if (trainingProgram != null)
            {
                return new TrainingProgram
                {
                    Name = trainingProgram.Name,
                    StartDate = trainingProgram.StartDate,
                    EndDate = trainingProgram.EndDate,
                    Status = trainingProgram.Status
                };
            }
            return null;
        }
    }
}
