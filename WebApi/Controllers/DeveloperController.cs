using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeveloperController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeveloperController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    // GET: api/developer
    [HttpGet]
    public async Task<IActionResult> GetDevelopers()
    {
        var developers = await _unitOfWork.Developers.GetAll();
        return Ok(developers);
    }
    
    // GET: api/developer/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDeveloper(int id)
    {
        var developer = await _unitOfWork.Developers.GetById(id);
        
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
        _unitOfWork.Developers.Add(developer);
        await _unitOfWork.Complete();

        return CreatedAtAction(nameof(GetDeveloper), new { id = developer.Id }, developer.Id);
    }
    
    // PUT: api/developer/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDeveloper(int id, Developer developer)
    {
        if (id != developer.Id)
        {
            return BadRequest();
        }
        
        _unitOfWork.Developers.Update(developer);
        await _unitOfWork.Complete();

        return Ok(developer);
    }
    
    // DELETE: api/developer
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDeveloper(int id)
    {
        var developer = await _unitOfWork.Developers.GetById(id);

        if (developer is null)
        {
            return NotFound();
        }
        
        _unitOfWork.Developers.Remove(developer);
        await _unitOfWork.Complete();

        return Ok(developer);
    }
    
    // POST: api/developer/range
    [HttpPost("range")]
    public async Task<IActionResult> PostDevelopers(IEnumerable<Developer> developers)
    {
        _unitOfWork.Developers.AddRange(developers);
        await _unitOfWork.Complete();

        var ids = developers.Select(d => d.Id);

        return CreatedAtAction(nameof(GetDevelopers), ids, ids);
    }
    
    // PUT: api/developer/range
    [HttpPut("range")]
    public async Task<IActionResult> PutDevelopers(IEnumerable<Developer> developers)
    {
        _unitOfWork.Developers.UpdateRange(developers);
        await _unitOfWork.Complete();

        return Ok(developers);
    }
    
    // DELETE: api/developer/range
    [HttpDelete("range")]
    public async Task<IActionResult> DeleteDevelopers(IEnumerable<Developer> developers)
    {
        _unitOfWork.Developers.RemoveRange(developers);
        await _unitOfWork.Complete();

        return Ok(developers);
    }
    
    // GET: api/developer/popular/5
    [HttpGet("popular/{count}")]
    public async Task<IActionResult> GetPopularDevelopers(int count)
    {
        var developers = await _unitOfWork.Developers.GetPopularDevelopers(count);
        return Ok(developers);
    }
}