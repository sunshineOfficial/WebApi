using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IGenericRepository<Project> _projects;

    public ProjectController(IGenericRepository<Project> projects)
    {
        _projects = projects;
    }
    
    // GET: api/project
    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        return Ok(await _projects.GetAll());
    }
    
    // GET: api/project/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject(int id)
    {
        var project = await _projects.GetById(id);
        
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
        int projectId = await _projects.Add(project);

        return CreatedAtAction(nameof(GetProject), new { id = projectId }, projectId);
    }
    
    // PUT: api/project/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProject(int id, Project project)
    {
        if (id != project.Id)
        {
            return BadRequest();
        }

        return Ok(await _projects.Update(project));
    }
    
    // DELETE: api/project
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _projects.GetById(id);

        if (project is null)
        {
            return NotFound();
        }

        return Ok(await _projects.Remove(project));
    }
    
    // POST: api/project/range
    [HttpPost("range")]
    public async Task<IActionResult> PostProjects(IEnumerable<Project> projects)
    {
        var ids = await _projects.AddRange(projects);

        return CreatedAtAction(nameof(GetProjects), ids, ids);
    }
    
    // PUT: api/project/range
    [HttpPut("range")]
    public async Task<IActionResult> PutProjects(IEnumerable<Project> projects)
    {
        return Ok(await _projects.UpdateRange(projects));
    }
    
    // DELETE: api/project/range
    [HttpDelete("range")]
    public async Task<IActionResult> DeleteProjects(IEnumerable<Project> projects)
    {
        return Ok(await _projects.RemoveRange(projects));
    }
}