using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataSourceService; //temp
using DataSourceService.DBModels; //temp
using System.Data.Entity;

namespace WorkFlowSpy
{
    public class WorkFlowDBInitializer : DropCreateDatabaseAlways<WFlowContext> //DropCreateDatabaseIfModelChanges<WFlowContext>
    {
        protected override void Seed(WFlowContext context)
        {
            List<TaskDB> tasks = new List<TaskDB>()
            {
                new TaskDB() { TaskId = 1, Text = "Project #2", StartDate = DateTime.Now.AddHours(-3),
                    Duration = 18, SortOrder = 10, Progress = 0.4m, ParentId = null, Holder = "Mike" },
                new TaskDB() { TaskId = 2, Text = "Task #1", StartDate = DateTime.Now.AddHours(-2),
                    Duration = 8, SortOrder = 10, Progress = 0.6m, ParentId = 1, Holder = "Mike" },
                new TaskDB() { TaskId = 3, Text = "Task #2", StartDate = DateTime.Now.AddHours(-1),
                    Duration = 8, SortOrder = 20, Progress = 0.6m, ParentId = 1, Holder = "Peter" }
            };

            tasks.ForEach(s => context.Tasks.Add(s));
            context.SaveChanges();

            List<LinkDB> links = new List<LinkDB>()
            {
                new LinkDB() { LinkId = 1, SourceTaskId = 1, TargetTaskId = 2, Type = "1" },
                new LinkDB() { LinkId = 2, SourceTaskId = 2, TargetTaskId = 3, Type = "0" }
            };

            links.ForEach(s => context.Links.Add(s));
            context.SaveChanges();
            base.Seed(context);
        }
    }
}