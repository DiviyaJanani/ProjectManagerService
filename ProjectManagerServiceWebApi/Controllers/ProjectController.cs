using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ProjectManager.BL;
using ProjectManager.Entities;
using System.Web.Http.Cors;

namespace ProjectManagerServiceWebApi.Controllers
{
    [EnableCors(origins:"*",headers: "*", methods: "*")]
    public class ProjectController : ApiController
    {
        private ProjectBusinessLayer bl = new ProjectBusinessLayer();

        // GET: api/GetProjecs
        [HttpGet]
        [Route("api/GetAllProjects")]
        public IQueryable<ProjectTable> GetAllProjects()
        {
            return bl.GetAllProjects();
        }

        // GET: api/GetProjectSummary
        [HttpGet]
        [Route("api/GetProjectSummary")]
        public List<ProjectSummary> GetProjectSummary()
        {
            return bl.GetProjectSummary();
        }

        // GET: api/GetProject/5
        [HttpGet]
        [ResponseType(typeof(ProjectTable))]
        [Route("api/GetProject/{id}")]
        public IHttpActionResult GetProject(int id)
        {
            List<ProjectTable> projects = bl.GetAllProjects().Where(i => i.ProjectId.Equals(id)).ToList();
            if (projects == null)
            {
                return NotFound();
            }

            return Ok(projects[0]);
        }

        // PUT: api/UpdateProject
        [HttpPut]
        [Route("api/UpdateProject")]
        public IHttpActionResult UpdateProject(ProjectTable projects)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bl.Update(projects.ProjectId, projects);
                }
                catch (Exception)
                {
                    return Ok("Error is occured during updated !");
                }
                return Ok("Record is updated Sucessfully !");

            }
            return Ok();
        }

        // POST: api/AddProject
        [HttpPost]
        [Route("api/AddProject")]
        public IHttpActionResult AddProject(ProjectTable projects)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bl.Add(projects);
                }
                catch (Exception)
                {
                    return Ok("Error is occured during inserted !");
                }
                return Ok("Record is added Sucessfully !");

            }
            return Ok();
        }

        // Delete: api/DeleteProject
        [HttpDelete]
        [Route("api/DeleteProject/{id}")]
        public IHttpActionResult DeleteProject(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bl.Delete(id);
                }
                catch (Exception)
                {
                    return Ok("Error is occured during deleted !");
                }
                return Ok("Record is deleted Sucessfully !");

            }
            return Ok();
        }
    }
}
