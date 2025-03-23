using System;
using API.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace API.Controllers;

public class BuggyController:BaseApiController
{
    [HttpGet("unauthorized")]
    public IActionResult GetUnauthorized()
    {
        return Unauthorized();
    }  
    [HttpGet("badrequest")]
    public IActionResult GetBadRequest()
    {
        return BadRequest("Not a good request");
    }  
    [HttpGet("not found")]
    public IActionResult GetNotFound()
    {
        return NotFound();
    }  
    [HttpGet("internalerror")]
    public IActionResult GetInternalError()
    {
       throw new Exception("This is a test exception");
    }  
    [HttpPost("validationerror")]
    public IActionResult GetValidationError(CreateProductDTO product)
    {
        return Ok();
    }   
}
