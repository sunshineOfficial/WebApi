using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ProjectController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    // GET: api/project
    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        var projects = await _unitOfWork.Projects.GetAll();
        return Ok(projects);
    }
    
    // GET: api/project/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject(int id)
    {
        var project = await _unitOfWork.Projects.GetById(id);
        
        if (project is null)
        {
            return NotFound();
        }

        return Ok(project);
    }

    // POST: api/project
    [HttpPost]
    public async Task<IActionResult> PostProject(Project project)
    {
        _unitOfWork.Projects.Add(project);
        await _unitOfWork.Complete();

        return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project.Id);
    }
    
    // PUT: api/project/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProject(int id, Project project)
    {
        if (id != project.Id)
        {
            return BadRequest();
        }
        
        _unitOfWork.Projects.Update(project);
        await _unitOfWork.Complete();

        return Ok(project);
    }
    
    // DELETE: api/project
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _unitOfWork.Projects.GetById(id);

        if (project is null)
        {
            return NotFound();
        }
        
        _unitOfWork.Projects.Remove(project);
        await _unitOfWork.Complete();

        return Ok(project);
    }
    
    // POST: api/project/range
    [HttpPost("range")]
    public async Task<IActionResult> PostProjects(IEnumerable<Project> projects)
    {
        _unitOfWork.Projects.AddRange(projects);
        await _unitOfWork.Complete();

        var ids = projects.Select(p => p.Id);

        return CreatedAtAction(nameof(GetProjects), ids, ids);
    }
    
    // PUT: api/project/range
    [HttpPut("range")]
    public async Task<IActionResult> PutProjects(IEnumerable<Project> projects)
    {
        _unitOfWork.Projects.UpdateRange(projects);
        await _unitOfWork.Complete();

        return Ok(projects);
    }
    
    // DELETE: api/project/range
    [HttpDelete("range")]
    public async Task<IActionResult> DeleteProjects(IEnumerable<Project> projects)
    {
        _unitOfWork.Projects.RemoveRange(projects);
        await _unitOfWork.Complete();

        return Ok(projects);
    }
}