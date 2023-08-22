﻿namespace CollegeApp.Dtos;

public class WorkshopCreateDto
{
    public string Name { get; set; }
    
    // Foreign Keys
    public Guid DirectorId { get; set; }
    public Guid SectorId { get; set; }
}