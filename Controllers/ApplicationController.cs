using dbhealthcare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace dbhealthcare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly DbfsthelathCareContext _context;
        public ApplicationController(DbfsthelathCareContext context)
        {
            _context = context;
        }

        [HttpGet("GetApplications")]
        public async Task<IActionResult> GetApplications()
        {
            var Applications = await _context.Applications.ToListAsync();
            if (!Applications.Any())
            {
                return NotFound();
            }
            return Ok(Applications);

        }
        [HttpDelete("DeleteApplication")]
        public async Task<IActionResult> DeleteApplication(int applicatioId)
        {
            var application =await _context.Applications.FindAsync(applicatioId);
            if (application == null)
            {
                return NotFound();
            }
            _context.Entry(application).State = EntityState.Deleted;
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("GetApplicationbyId")]
        public async Task<IActionResult> GetApplicationById(int AplicationId)
        {
            var Application = await _context.Applications.FindAsync(AplicationId);
            if (Application == null)
            {
                return NotFound();
            }
            return Ok(Application);
        }
        [HttpPost("InsertApplication")]
        public async Task<IActionResult> InsertApplication(string appName)
        {
            if (string.IsNullOrEmpty(appName))
            {
                return BadRequest();

            }
            Application app = new Application() { AppName = appName, AppDesc = "TestApp" };
             _context.Applications.Add(app);
           await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("UpdateApplication")]
        public async Task<IActionResult> UpdateApplication(int Applicationid,string Appname)
        {
            if(string.IsNullOrEmpty(Appname))
            {
                return BadRequest();
            }
            var appdata= await _context.Applications .FindAsync(Applicationid);
            if (appdata==null)
            {
                return NotFound();
            }
            appdata.AppName = Appname;
            _context.Entry(appdata).State = EntityState.Modified;   
            _context.SaveChanges();
            return Ok();
        }
    }
}
