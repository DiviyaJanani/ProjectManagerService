using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Entities;
using ProjectManager.DL;
using System.Data.Entity;

namespace ProjectManager.BL
{
    public class TaskBusinessLayer
    {
        private DataContext db = new DataContext();        

        public DbSet<TaskTable> GetAllTasks()
        {
            return db.Tasks;
        }
        
        public bool Add(TaskTable tasks)
        {
            try
            {
                tasks.ParentTask = !string.IsNullOrEmpty(tasks.ParentTask) ? tasks.ParentTask : string.Empty;
                tasks.TaskName = !string.IsNullOrEmpty(tasks.TaskName) ? tasks.TaskName : string.Empty;
                db.Tasks.Add(tasks);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;

        }
        public bool Delete(int taskId)
        {
            try
            {
                var query = (from update in db.Tasks.Where(x => x.TaskId == taskId)
                             select update).SingleOrDefault();
                if (query != null)
                {
                    query.TaskId = taskId;
                    query.Deleted = true;

                    db.Entry(query).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Update(int taskId, TaskTable tasks)
        {
            try
            {
                var query = (from update in db.Tasks.Where(x => x.TaskId == taskId)
                             select update).SingleOrDefault();
                if (query != null)
                {
                    query.TaskId = taskId;
                    query.ParentTask = !string.IsNullOrEmpty(tasks.ParentTask)
                        ? tasks.ParentTask : string.Empty;
                    query.StartDate = tasks.StartDate;
                    query.EndDate = tasks.EndDate;
                    query.Priority = tasks.Priority;
                    query.TaskName = !string.IsNullOrEmpty(tasks.TaskName)
                        ? tasks.TaskName : string.Empty;
                    query.ProjectId = tasks.ProjectId;
                    query.UserId = tasks.UserId;

                    db.Entry(query).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}

