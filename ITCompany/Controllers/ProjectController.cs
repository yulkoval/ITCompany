using ITCompany.Models;
using ITCompany.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITCompany.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMongoService<Project> _projectService;

        public ProjectController(IMongoService<Project> projectService)
        {
            _projectService = projectService;
        }

        //// GET: api/<ProjectController>
        [HttpGet]
        public IEnumerable<Project> Get()
        {
            return _projectService.AsQueryable();
        }

        // GET api/<ProjectController>/5
        [HttpGet("{id}")]
        public Project Get(string id)
        {
            var project = _projectService.FindById(id);

            if (project == null)
            {
                return null;
            }

            return project;
        }

        // GET api/<ProjectController>/getProjectNames?name=Test
        [HttpGet("getProjectNames")]
        public IEnumerable<string> GetProjectNames([FromQuery] string name)
        {
            var projects = _projectService.FilterBy(
                filter => filter.Name == name,
                projection => projection.Id.ToString()
            );
            return projects;
        }

        //POST api/<ProjectController>/registerProject
        //Body:
        //{
        //   "Name": "PROJECT", 
        //   "Description": "DESCRIPTION", 
        //   "Developers": [
        //      {
        //         "Name":"DEVELOPER_NAME1",
        //         "Age":23
        //      },
        //      {
        //         "Name": "DEVELOPER_NAME2",
        //         "Age":29
        //      }
        //   ]
        //}
        [HttpPost("registerProject")]
        public async Task CreateProject([FromBody] Project project)
        {
            project.Id = ObjectId.GenerateNewId();
            await _projectService.InsertOneAsync(project);
        }

        // PUT api/<ProjectController>/5
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Project project)
        {
            if (project == null)
            {
                return BadRequest();
            }

            var projectItem = _projectService.FindById(id.ToString());
            if (projectItem == null)
            {
                return NotFound();
            }
            project.Id = ObjectId.Parse(id);
            _projectService.ReplaceOne(project);
            return new NoContentResult();
        }

        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteById(string id)
        {
            var project = _projectService.FindById(id);

            if (project == null)
            {
                return NotFound();
            }

            _projectService.DeleteById(id);

            return NoContent();
        }

        //POST api/<DeveloperController>/createIndex?field=Test
        [HttpPost("createIndex")]
        public async Task CreateIndex([FromQuery] string field)
        {
            await _projectService.CreateIndex(field);
        }

        //GET api/<DeveloperController>/aggregation?field=Name&fieldValue=Test
        [HttpGet("aggregation")]
        public IEnumerable<Project> CreateAggregation([FromQuery] string field, [FromQuery] string fieldValue)
        {
            var project = _projectService.EntitiesAggregate(field, fieldValue);
            return project;
        }

    }
}
