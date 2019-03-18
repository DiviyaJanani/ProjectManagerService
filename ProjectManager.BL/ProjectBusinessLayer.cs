
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
    public class ProjectBusinessLayer
    {
        private DataContext db = new DataContext();
        
        public DbSet<ProjectTable> GetAllProjects()
        {
            return db.Projects;
        }

        public List<ProjectSummary> GetProjectSummary()
        {
            IQueryable<ProjectSummary> query = from projects in db.Projects
                                               join tasks in db.Tasks
                  on projects.ProjectId equals tasks.ProjectId
                    //where tasks.Status.Equals("Completed")
                                               group projects by new
                                               {
                                                   projects.ProjectName,
                                                   projects.Priority,
                                                   projects.StartDate,
                                                   projects.EndDate,
                                                   projects.ProjectId,
                                                   projects.Deleted
                                               } into lists
                                               select new ProjectSummary
                                               {
                                                   ProjectName = lists.Key.ProjectName,
                                                   Priority = lists.Key.Priority,
                                                   StartDate = lists.Key.StartDate,
                                                   EndDate = lists.Key.EndDate,
                                                   ProjectId = lists.Key.ProjectId,
                                                   Deleted = lists.Key.Deleted,
                                                   Count = lists.Count(),
                                                   CompletedCount = lists.Count()
                                               };
            return query.ToList();
        }

        public bool Add(ProjectTable projects)
        {
            try
            {
                db.Projects.Add(projects);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }
        public bool Delete(int projectId)
        {
            try
            {
                var query = (from update in db.Projects.Where(x => x.ProjectId == projectId)
                             select update).SingleOrDefault();
                if (query != null)
                {
                    query.ProjectId = projectId;
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

        public bool Update(int projectId, ProjectTable projects)
        {
            try
            {
                var query = (from update in db.Projects.Where(x => x.ProjectId== projectId)
                             select update).SingleOrDefault();
                if (query != null)
                {
                    query.ProjectId = projectId;
                    query.ProjectName = projects.ProjectName;
                    query.StartDate = projects.StartDate;
                    query.EndDate = projects.EndDate;
                    query.Priority = projects.Priority;
                    query.UserId = projects.UserId;

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

    public class ProjectSummary
    {
        public string ProjectName;

        public int Priority;

        public DateTime? StartDate;

        public DateTime? EndDate;

        public int Count;

        public int CompletedCount;

        public string ManagerName;

        public int ProjectId;

        public bool Deleted;

    }
}

