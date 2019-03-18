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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TaskController : ApiController
    {
        private TaskBusinessLayer bl = new TaskBusinessLayer();

        // GET: api/GetTasks
        [HttpGet]
        [Route("api/GetAllTasks")]
        public IQueryable<TaskTable> GetAllTasks()
        {
            return bl.GetAllTasks();
        }

        // GET: api/GetTask/5
        [HttpGet]
        [ResponseType(typeof(TaskTable))]
        [Route("api/GetTask/{id}")]
        public IHttpActionResult GetTask(int id)
        {
            List<TaskTable> tasks = bl.GetAllTasks().Where(i => i.TaskId.Equals(id)).ToList();
            if (tasks == null)
            {
                return NotFound();
            }

            return Ok(tasks[0]);
        }

        // PUT: api/UpdateTask
        [HttpPut]
        [Route("api/UpdateTask")]
        public IHttpActionResult UpdateTask(TaskTable tasks)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bl.Update(tasks.TaskId, tasks);
                }
                catch (Exception)
                {
                    return Ok("Error is occured during updated !");
                }
                return Ok("Record is updated Sucessfully !");

            }
            return Ok();
        }

        // POST: api/AddTask
        [HttpPost]
        [Route("api/AddTask")]
        public IHttpActionResult AddTask(TaskTable tasks)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bl.Add(tasks);
                }
                catch (Exception)
                {
                    return Ok("Error is occured during inserted !");
                }
                return Ok("Record is added Sucessfully !");

            }
            return Ok();
        }

        // Delete: api/AddTask
        [HttpDelete]
        [Route("api/DeleteTask/{id}")]
        public IHttpActionResult DeleteTask(int id)
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
