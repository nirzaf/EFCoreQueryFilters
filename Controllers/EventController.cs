using AutoMapper;

using EFCoreQueryFilters.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreQueryFilters.Controllers;

[ApiController]
[Route("[controller]/api/[action]")]
public class EventController : ControllerBase
{
    private readonly EventDbContext _context;

    public EventController(EventDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
        var events = await _context.Events.ToListAsync();
        //Response with EventResponseObject and Map it to EventResponseObject
        var mapper = new MapperConfiguration(cfg => 
            cfg.CreateMap<Event, EventResponseObject>()).CreateMapper();
        
        var eventsResponse = mapper.Map<List<EventResponseObject>>(events);
        return Ok(eventsResponse);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetEventsById(int id)
    {
        var events = await _context.Events.Where(x => x.Id == id).ToListAsync();
        //Response with EventResponseObject and Map it to EventResponseObject
        var mapper = new MapperConfiguration(cfg => 
            cfg.CreateMap<Event, EventResponseObject>()).CreateMapper();
        
        var eventsResponse = mapper.Map<List<EventResponseObject>>(events);
        return Ok(eventsResponse);
    }
    
    //Post method to add new event
    [HttpPost]
    public async Task<IActionResult> AddEvent(Event newEvent)
    {
        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();
        return Ok();
    }
    
    //Put method to update event
    [HttpPut]
    public async Task<IActionResult> UpdateEvent(Event updatedEvent)
    {
        _context.Events.Update(updatedEvent);
        await _context.SaveChangesAsync();
        return Ok();
    }
    
    //Put method to soft delete event
    [HttpPut]
    public async Task<IActionResult> DeleteEvent(Event deletedEvent)
    {
        deletedEvent.IsDeleted = true;
        _context.Events.Update(deletedEvent);
        await _context.SaveChangesAsync();
        return Ok();
    }
    
    //get method to soft delete event by id 
    [HttpGet]
    public async Task<IActionResult> DeleteEventById(int id)
    {
        var deletedEvent = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);
        deletedEvent!.IsDeleted = true;
        _context.Events.Update(deletedEvent);
        await _context.SaveChangesAsync();
        return Ok();
    }

    //Get method to soft deleted events
    [HttpGet]
    public async Task<IActionResult> GetDeletedEvents()
    {
        var events = await _context.Events.Where(x => x.IsDeleted == true).ToListAsync();
        //Response with EventResponseObject and Map it to EventResponseObject
        var mapper = new MapperConfiguration(cfg => 
            cfg.CreateMap<Event, EventResponseObject>()).CreateMapper();
        
        var eventsResponse = mapper.Map<List<EventResponseObject>>(events);
        return Ok(eventsResponse);
    }
}
