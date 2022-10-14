using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeveloperController : ControllerBase
{
    private readonly IGenericRepository<Developer> _developers;

    public DeveloperController(IGenericRepository<Developer> developers)
    {
        _developers = developers;
    }
    
    // GET: api/developer
    [HttpGet]
    public async Task<IActionResult> GetDevelopers()
    {
        return Ok(await _developers.GetAll());
    }
    
    // GET: api/developer/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDeveloper(int id)
    {
        var developer = await _developers.GetById(id);
        
        if (developer is null)
        {
            return NotFound();
        }

        return Ok(developer);
    }

    // POST: api/developer
    [HttpPost]
    public async Task<IActionResult> PostDeveloper(Developer developer)
    {
        int developerId = await _developers.Add(developer);

        return CreatedAtAction(nameof(GetDeveloper), new { id = developerId }, developerId);
    }
    
    // PUT: api/developer/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDeveloper(int id, Developer developer)
    {
        if (id != developer.Id)
        {
            return BadRequest();
        }

        return Ok(await _developers.Update(developer));
    }
    
    // DELETE: api/developer
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDeveloper(int id)
    {
        var developer = await _developers.GetById(id);

        if (developer is null)
        {
            return NotFound();
        }

        return Ok(await _developers.Remove(developer));
    }
    
    // POST: api/developer/range
    [HttpPost("range")]
    public async Task<IActionResult> PostDevelopers(IEnumerable<Developer> developers)
    {
        var ids = await _developers.AddRange(developers);

        return CreatedAtAction(nameof(GetDevelopers), ids, ids);
    }
    
    // PUT: api/developer/range
    [HttpPut("range")]
    public async Task<IActionResult> PutDevelopers(IEnumerable<Developer> developers)
    {
        return Ok(await _developers.UpdateRange(developers));
    }
    
    // DELETE: api/developer/range
    [HttpDelete("range")]
    public async Task<IActionResult> DeleteDevelopers(IEnumerable<Developer> developers)
    {
        return Ok(await _developers.RemoveRange(developers));
    }
}