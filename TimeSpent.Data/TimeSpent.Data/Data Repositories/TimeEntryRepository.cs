using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using TimeSpent.Core.Data;
using TimeSpent.Business.Entities;
using TimeSpent.Data.Contracts.RepositoryInterfaces;
using TimeSpent.Business.Contracts;
using System.Data.SqlClient;
using TimeSpent.Data.Contracts;

namespace TimeSpent.Data
{
    [Export( typeof(ITimeEntryRepository)) ]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TimeEntryRepository : DataRepositoryBase<TimeEntry> , ITimeEntryRepository
    {
        public IEnumerable<TimeEntryData> GetTimeHistoryByAccount(Account account)
        {
            using (TimeSpentContext context = new TimeSpentContext())
            {
                var query = from entry in context.TimeEntrySet.Include("Category").Include("Project")
                            where entry.AccountId == account.AccountId
                            select new TimeEntryData
                            {
                                TimeEntryId = entry.TimeEntryId,
                                Date = entry.Date,
                                CategoryName = entry.Category.Name,
                                ProjectName = entry.Project.Name,
                                Duration = entry.Duration,
                                UserName = account.LoginName,
                                Description = entry.Description

                            };

                return query.ToList();

            }


        }

        protected override TimeEntry AddEntity(TimeSpentContext context, TimeEntry entity)
        {
            return context.TimeEntrySet.Add(entity);
        }

        protected override IEnumerable<TimeEntry> GetEntities(TimeSpentContext context)
        {
            return (from e in context.TimeEntrySet.Include("Project").Include("Category")
            select e);
        }

        protected override TimeEntry GetEntity(TimeSpentContext context, int id)
        {
            var query = from e in context.TimeEntrySet
                        where e.TimeEntryId == id
                        select e;

            return query.FirstOrDefault();
        }

        protected override TimeEntry UpdateEntity(TimeSpentContext context, TimeEntry entity)
        {
            var query = from e in context.TimeEntrySet
                        where e.TimeEntryId == entity.TimeEntryId
                        select e;

            return query.FirstOrDefault();
        }

        public IEnumerable<ProjectCategoryReport> GetProjectCategoryReport(int accountId)
        {
            

            using (TimeSpentContext context = new TimeSpentContext())
            {
                var userId = new SqlParameter("@userId", accountId);


                var result = context.Database
                             .SqlQuery<TimeEntryReportData>("GetProjectCategoryReport @userId", userId)
                             .ToList();


                var groupResult = result.GroupBy(p => p.ProjectName)
                                        .Select(g => new ProjectCategoryReport
                                        {
                                            x = g.Key,
                                            y = g.Select(c => c.Duration).ToArray()
                                        });


                return groupResult.ToList();

            }

            
        }

        public IEnumerable<ProjectCategoryReport> GetProjectReport(int accountId)
        {
            using (TimeSpentContext context = new TimeSpentContext())
            {
                var userId = new SqlParameter("@userId", accountId);


                var result = context.Database
                             .SqlQuery<TimeEntryReportData>("GetProjectReport @userId", userId)
                             .ToList();

               
                var groupResult = result.GroupBy(p => new { p.ProjectName, p.CategoryName })
                                        .Select(g => new ProjectCategoryReport
                                        {
                                            x = g.Key.ProjectName,
                                            y = g.Select(c => c.Duration).ToArray()
                                        });


                return groupResult;

            }
        }
    }
}
