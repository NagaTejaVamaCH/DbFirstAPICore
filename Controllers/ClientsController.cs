using dbhealthcare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dbhealthcare.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly DbfsthelathCareContext _dbfsthelathCareContext;
    public ClientsController(DbfsthelathCareContext dbfsthelathCareContext)
    {
        _dbfsthelathCareContext = dbfsthelathCareContext;
    }

    //GetClients
    [HttpGet("GetClients")]
    public async Task<IActionResult> GetClients()
    {
        var result = await _dbfsthelathCareContext.Clients.ToListAsync();
        if(!result.Any())
        {
            return NotFound();
        }
        return Ok(result);
    }


    //GetClient(Id)
    [HttpGet("GetClientById")]
    public async Task<IActionResult> GetClientById(int clientId)
    {
        var result = await _dbfsthelathCareContext.Clients.Where(c => c.Id == clientId).FirstOrDefaultAsync();
        if(result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("GetApplicationByClientId")]
    public async Task<IActionResult> GetApplicationByClientId(int clientId)
    {
        var result = await _dbfsthelathCareContext.Applications.Where(c => c.Clientid == clientId).ToListAsync();
        if(!result.Any())
        {
            return NotFound();
        }
        return Ok(result);
    }

    //PostClient
    [HttpPost("PostClient")]
    public async Task<IActionResult> PostClient(string name)
    {
        if(string.IsNullOrEmpty(name))
        {
            return BadRequest();
        }
        Client client = new Client() { Name = name };
        _dbfsthelathCareContext.Clients.Add(client);
        await _dbfsthelathCareContext.SaveChangesAsync();
        return Ok(client);
    }


    [HttpPost("PostClients")]
    public async Task<IActionResult> PostClients(List<string> names)
    {
        if (names.Count<=0)
        {
            return BadRequest();
        }
        foreach (string name in names)
        {
            Client client = new Client() { Name = name };
            _dbfsthelathCareContext.Clients.Add(client);
        }        
        await _dbfsthelathCareContext.SaveChangesAsync();
        return Ok("Saved Succsusfully");
    }

    //PutClient
    [HttpPut("PutClient")]
    public async Task<IActionResult> PutClient(int id, string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return BadRequest();
        }
        var client = await _dbfsthelathCareContext.Clients.FindAsync(id);
        if (client == null)
        {
            return NotFound();
        }
        client.Name = name;
        _dbfsthelathCareContext.Entry(client).State = EntityState.Modified;
        _dbfsthelathCareContext.SaveChanges();
        return Ok(client);
    }


    //DeleteClient
    [HttpDelete("DeleteClient")]
    public async Task<IActionResult> DeleteClient(int id)
    {

        var client = await _dbfsthelathCareContext.Clients.FindAsync(id);
        if (client == null)
        {
            return NotFound();
        }
        _dbfsthelathCareContext.Entry(client).State = EntityState.Deleted;
        await _dbfsthelathCareContext.SaveChangesAsync();
        return Ok();
    }


}
