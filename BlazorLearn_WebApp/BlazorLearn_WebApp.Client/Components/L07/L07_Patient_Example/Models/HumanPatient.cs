
namespace BlazorLearn_WebApp.Client.Components.L07.L07_Patient_Example.Models
{
    public class HumanPatient : IPatientBasicInfo
    {
        public string? Name { get; set; } = "姓名";
        public int Age { get;set; }
        public string? Category { get;set; }
        public DateOnly? InhospitalDate { get;set; }
        public TimeOnly? InhospitalTime { get;set; }

        public IPatientAdditionalInfo GetAdditionInfo()
        {
            return new HumanAdditionInfo();
        }
    }

    public class HumanAdditionInfo : IPatientAdditionalInfo
    {
        public string? Description { get; set; }
    }

    public class AnimalPatient : IPatientBasicInfo
    {
        public string? Name { get; set; } = "一只小鸟";
        public int Age { get; set; } = 3;
        public string? Category { get;set; }
        public DateOnly? InhospitalDate { get;set; }
        public TimeOnly? InhospitalTime { get;set; }

        public IPatientAdditionalInfo GetAdditionInfo()
        {
            return new AnimalAdditionInfo()
            {
                IsEndangered = Random.Shared.NextDouble() >= 0.75
            };
        }
    }
    public class AnimalAdditionInfo:IPatientAdditionalInfo
    {
        public string? Description { get; set; }

        public bool IsEndangered { get; set; }
    }
}
