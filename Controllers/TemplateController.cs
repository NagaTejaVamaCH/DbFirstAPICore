using dbhealthcare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace dbhealthcare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly DbfsthelathCareContext _dbcontext;
        public TemplateController(DbfsthelathCareContext context)
        {
            _dbcontext = context;
        }
        [HttpGet("GetTemplates")]
        public async Task<IActionResult> GetTemplates()
        {
            var templates =await _dbcontext.Template.ToListAsync();
            if(templates == null)
            {
                return NotFound();
            }
            return Ok(templates);
        }
        [HttpDelete("DeleteTemplate")]
        public async Task<IActionResult> DeleteTemplate(int TemplateId)
        {
            var Templates = await _dbcontext.Template.FindAsync(TemplateId);
            if (Templates == null)
            {
                return NotFound();
            }
            _dbcontext.Entry(Templates).State = EntityState.Deleted;
            _dbcontext.SaveChanges();
            return Ok();
        }
        [HttpGet("GetTemplatebyId")]
        public async Task<IActionResult> GetTemplatesById(int templateId)
        {
            var template = await _dbcontext.Template.FindAsync(templateId);
            if (template == null)
            {
                return NotFound();
            }
            Log.Information($"template name is .{template.TempName}");
            
            return Ok(template);
        }
        [HttpPost("InsertTemplate")]
        public async Task<IActionResult> InsertTemplate(string templatename)
        {
            if (string.IsNullOrEmpty(templatename))
            {
                return BadRequest();

            }
            Template temp = new Template() { TempName = templatename };
            _dbcontext.Template.Add(temp);
            _dbcontext.SaveChanges();
            Log.Information("template name inserted successfully.");

            return Ok();
        }
        [HttpPut("UpdateTemplate")]
        public async Task<IActionResult> UpdateTemplate(int TemplateId, string TemplateName)
        {
            if (string.IsNullOrEmpty(TemplateName))
            {
                return BadRequest();
            }
            var tempdata = await _dbcontext.Template.FindAsync(TemplateId);
            if (tempdata == null)
            {
                return NotFound();
            }
            tempdata.TempName = TemplateName;
            _dbcontext.Entry(tempdata).State = EntityState.Modified;
            _dbcontext.SaveChanges();
            return Ok();


        }
    }
}
