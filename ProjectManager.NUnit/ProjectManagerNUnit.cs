using System;

using ProjectManager.BL;
using ProjectManagerServiceWebApi;
using ProjectManager.Entities;
using System.Collections.Generic;
using Moq;
using ProjectManager.DL;
using System.Linq;
using NUnit.Framework;

namespace ProjectManager.NUnit
{
    [TestFixture]
    public class ProjectManagerNUnit
    {
        /// <summary>
        /// Testcases for TASKS
        /// </summary>
        /// 

        private TaskBusinessLayer task = new TaskBusinessLayer();

        [TestCase]
        public void GetTasks()
        {
            List<TaskTable> tasks = task.GetAllTasks().Where(i => i.TaskId.Equals(2002)).ToList();
        }

        [TestCase]
        public void SaveTasks()
        {
            var tasks = new TaskTable { 
                ProjectName = "NUNIT", ProjectId = 100, TaskName = "NUNIT",
                Priority = 10,  ParentTask = "TEST",  IsParent = true,
                StartDate = Convert.ToDateTime("2018-10-18"), EndDate = Convert.ToDateTime("2018-10-18"),
                UserId = 1, UserName = "NUNIT",  Deleted = false, Status = "Completed"};

            bool added = task.Add(tasks);
            Assert.AreEqual(true, added);
        }

        [TestCase]
        public void UpdateTask()
        {
            var tasks = new TaskTable
            {
                TaskId = 1003,
                ProjectName = "NUNIT",
                ProjectId = 100,
                TaskName = "NUNIT",
                Priority = 10,
                ParentTask = "TEST",
                IsParent = true,
                StartDate = Convert.ToDateTime("2018-10-18"),
                EndDate = Convert.ToDateTime("2018-10-18"),
                UserId = 1,
                UserName = "NUNIT",
                Deleted = false,
                Status = "Completed"
            };

            bool updated = task.Update(tasks.TaskId, tasks);
            Assert.AreEqual(true, updated);
        }

        [TestCase]
        public void DeleteTask()
        {
            var tasks = new TaskTable { ParentTask = "", TaskName = "to be deleted", StartDate = Convert.ToDateTime("2018-10-18"), EndDate = Convert.ToDateTime("2018-10-18"), Priority = 20, Deleted = false };
            task.Add(tasks);

            List<TaskTable> toBeDeleteTask = task.GetAllTasks().Where(i => i.TaskName.Equals("to be deleted") && i.Deleted.Equals(false)).ToList();

            bool deleted = task.Delete(toBeDeleteTask[0].TaskId);
            Assert.AreEqual(true, deleted);
        }

        /// <summary>
        /// Testcases for PROJECTS
        /// </summary>
        private ProjectBusinessLayer project = new ProjectBusinessLayer();

        [TestCase]
        public void GetProjects()
        {
            List<ProjectTable> projects = project.GetAllProjects().Where(i => i.ProjectId.Equals(2005)).ToList();
        }

        [TestCase]
        public void SaveProjects()
        {
            var projects = new ProjectTable
            {
                ProjectName = "NUNIT",
                Priority = 10,
                StartDate = Convert.ToDateTime("2018-10-18"),
                EndDate = Convert.ToDateTime("2018-10-18"),
                UserId = 1,
                ManagerName = "User 2",
                Deleted = false
            };

            bool added = project.Add(projects);
            Assert.AreEqual(true, added);
        }

        [TestCase]
        public void UpdateProject()
        {
            var projects = new ProjectTable
            {ProjectId = 1,
                ProjectName = "NUNIT",
                Priority = 10,
                StartDate = Convert.ToDateTime("2018-10-18"),
                EndDate = Convert.ToDateTime("2018-10-18"),
                UserId = 1,
                ManagerName = "User 2",
                Deleted = false
            };

            bool updated = project.Update(projects.ProjectId, projects);
            Assert.AreEqual(true, updated);
        }

        /// <summary>
        /// Testcases for USERS
        /// </summary>
        private UserBusinessLayer user = new UserBusinessLayer();

        [TestCase]
        public void GetUsers()
        {
            List<UserTable> users = user.GetAllUsers().Where(i => i.UserId.Equals(1002)).ToList();
        }

        [TestCase]
        public void SaveUsers()
        {
            var users = new UserTable
            {
FirstName = "NUNIT",
LastName = "NUNIT",
EmployeeId = 3,
Deleted = false
            };

            bool added = user.Add(users);
            Assert.AreEqual(true, added);
        }

        [TestCase]
        public void UpdateUser()
        {
            var users = new UserTable
            {
                UserId = 1002,
                FirstName = "NUNIT",
                LastName = "NUNIT",
                EmployeeId = 3,
                Deleted = false
            };

            bool updated = user.Update(users.UserId, users);
            Assert.AreEqual(true, updated);
        }
    }
}
