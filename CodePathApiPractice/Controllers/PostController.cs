using System.ComponentModel.Design;
using CodePathWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodePathWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController(ILogger<PostController> logger, NetCoreDbContext db) : ControllerBase
{
    private readonly ILogger<PostController> _logger = logger;
    private readonly NetCoreDbContext _db = db;
    
    [HttpGet(Name = "GetPosts")]
    public async Task<IResult> GetPosts()
    {
        return await _db.Posts.Where(x => x.Active).ToListAsync() is List<Post> posts ? TypedResults.Ok(posts) : TypedResults.NotFound();
    }

    [HttpGet("{id}", Name = "GetPost")]
    public async Task<IResult> Get(int id)
    {
        return await _db.Posts.FindAsync(id)
            is Post page
                ? TypedResults.Ok(page)
                : TypedResults.NotFound();
    }

    [HttpPost(Name = "Create")]
    public async Task<IActionResult> Create([FromBody] Post post)
    {
        if (post == null)
        {
            return BadRequest();
        }

        post.CreatedAt = DateTime.UtcNow;
        post.UpdatedAt = DateTime.UtcNow;

        _db.Posts.Add(post);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = post.ID }, post);
    }

    [HttpPatch(Name = "Update")]
    public async Task<IActionResult> Update([FromBody] Post post)
    {
        var db_post = await _db.Posts.FindAsync(post.ID);
        if (db_post == null)
        {
            return NotFound();
        }

        bool active = post.Active;

        db_post.Title = post.Title;
        db_post.Description = post.Description;
        db_post.Active = active;
        db_post.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        return Ok(db_post);
    }

    [HttpDelete("{id}", Name = "Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var post = await _db.Posts.FindAsync(id);
        if (post == null)
        {
            return NotFound();
        }

        _db.Posts.Remove(post);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}