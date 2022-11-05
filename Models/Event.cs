﻿namespace EFCoreQueryFilters.Models;

public class Event : BaseAuditableEntity
{
    public string? eventCode { get; set; }
    public string? eventName { get; set; }
    public string? eventDescription { get; set; }
    public string? eventLocation { get; set; }
    public DateTime? eventStartDate { get; set; }
    public DateTime? eventEndDate { get; set; }
    public string? eventStatus { get; set; }
}

public class EventPostObject 
{
    public string? eventCode { get; set; }
    public string? eventName { get; set; }
    public string? eventDescription { get; set; }
    public string? eventLocation { get; set; }
    public DateTime? eventStartDate { get; set; }
    public DateTime? eventEndDate { get; set; }
    public string? eventStatus { get; set; }
}