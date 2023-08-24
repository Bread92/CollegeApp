namespace CollegeApp.Dtos.MoldPurpose;

public class MoldPurposeDto
{
    public Guid MoldPurposeId { get; set; }

    public string PurposeName { get; set; }
}

public static class MoldPurposeDtoExtensions
{
    public static MoldPurposeDto ToDto(this Entities.MoldPurpose moldPurpose)
    {
        return new MoldPurposeDto()
        {
            MoldPurposeId = moldPurpose.MoldPurposeId,
            PurposeName = moldPurpose.PurposeName
        };
    }
}