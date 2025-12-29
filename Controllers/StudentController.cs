using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mypcot.Models.Dto;
using Mypcot.Services;

namespace Mypcot.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;
    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _studentService.GetAll();
        if (!result.Item1)
            return BadRequest(result.Item2);

        return Ok(result.Item2);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _studentService.GetById(id);
        if (!result.Item1)
            return BadRequest(result.Item2);

        return Ok(result.Item2);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddStudentDto request)
    {
        var result = await _studentService.Add(request);
        if (!result.Item1)
            return BadRequest(result.Item2);

        return Ok(result.Item2);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateStudentDto request)
    {
        var result = await _studentService.Update(request);
        if (!result.Item1)
            return BadRequest(result.Item2);

        return Ok(result.Item2);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _studentService.Delete(id);
        if (!result.Item1)
            return BadRequest(result.Item2);

        return Ok(result.Item2);
    }
}
