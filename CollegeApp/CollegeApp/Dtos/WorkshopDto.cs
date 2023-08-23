﻿using CollegeApp.Entities;

namespace CollegeApp.Dtos;

public class WorkshopDto
{
    public Guid WorkshopId { get; set; }

    public string Name { get; set; }
    
    // Foreign Keys
    public Guid DirectorId { get; set; }
    public Guid SectorId { get; set; }
}

public static class WorkshopDtoExtensions
{
    public static WorkshopDto ToDto(this Workshop workshop)
    {
        return new WorkshopDto()
        {
            WorkshopId = workshop.WorkshopId,
            Name = workshop.Name,
            DirectorId = workshop.DirectorId,
            SectorId = workshop.SectorId
        };
    }
}