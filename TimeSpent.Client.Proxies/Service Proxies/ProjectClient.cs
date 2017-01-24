using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TimeSpent.Client.Contracts;
using TimeSpent.Client.Entities;
using TimeSpent.Core.Exceptions;
using TimeSpent.Core.ServiceModel;

namespace TimeSpent.Client.Proxies.Service_Proxies
{
    [Export(typeof(IProjectService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectClient : UserClientBase<IProjectService>, IProjectService
    {
        public void DeleteProject(int id)
        {
            Channel.DeleteProject(id);

        }

        public Project[] GetAllProjects()
        {
            return Channel.GetAllProjects();
        }

        public Project GetProject(int id)
        {
            return Channel.GetProject(id);
        }

        public Project UpdateProject(Project project)
        {
            return Channel.UpdateProject(project);
        }
    }
}
