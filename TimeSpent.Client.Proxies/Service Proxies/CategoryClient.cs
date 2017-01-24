using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using TimeSpent.Client.Contracts;
using TimeSpent.Client.Entities;
using System.ServiceModel;
using System.Runtime.Serialization;
using TimeSpent.Core.ServiceModel;

namespace TimeSpent.Client.Proxies
{
    [Export(typeof(ICategoryService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CategoryClient : UserClientBase<ICategoryService>, ICategoryService
    {
        public void DeleteCategory(int id)
        {
            Channel.DeleteCategory(id);
        }

        public Category[] GetAllCategories()
        {
            return Channel.GetAllCategories();
        }

        public Category GetCategory(int id)
        {
            return Channel.GetCategory(id);
        }

        public Category UpdateCategory(Category category)
        {
            return Channel.UpdateCategory(category);
        }
    }
}
