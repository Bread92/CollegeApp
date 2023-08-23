using CollegeApp.Entities;

namespace CollegeApp.Dtos;

public class MoldPurposeDto
{
    public Guid MoldPurposeId { get; set; }

    public string PurposeName { get; set; }
}

public static class MoldPurposeDtoExtensions
{
    public static MoldPurposeDto ToDto(this MoldPurpose moldPurpose)
    {
        return new MoldPurposeDto()
        {
            MoldPurposeId = moldPurpose.MoldPurposeId,
            PurposeName = moldPurpose.PurposeName
        };
    }
}