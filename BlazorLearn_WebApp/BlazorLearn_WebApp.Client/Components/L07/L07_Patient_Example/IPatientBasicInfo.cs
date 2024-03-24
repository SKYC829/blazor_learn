namespace BlazorLearn_WebApp.Client.Components.L07.L07_Patient_Example
{
    public interface IPatientBasicInfo
    {
        string? Name { get; set; }

        int Age { get; set; }

        string? Category { get; set; }

        DateOnly? InhospitalDate { get; set; }

        TimeOnly? InhospitalTime { get; set; }

        IPatientAdditionalInfo GetAdditionInfo();
    }

    public interface IPatientAdditionalInfo
    {

    }
}
