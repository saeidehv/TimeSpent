using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TimeSpent.Business.Entities;
using TimeSpent.Core.Exceptions;
using TimeSpent.Common;

namespace TimeSpent.Business.Contracts
{
    [ServiceContract]
    public interface ICategoryService
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Category GetCategory(int id);

        // returning array instead of list or IEnumerable to make the service useable for non .net clinets
        [OperationContract]
        Category[] GetAllCategories();
   

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        //   [FaultContract(typeof(AuthorizationValidationException))]
        Category UpdateCategory(Category category);


        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        //  [FaultContract(typeof(AuthorizationValidationException))]
        [FaultContract(typeof(CascadeDeleteException))]
        void DeleteCategory(int id);



        //[OperationContract]
        //Task DeleteCategoryAsync(int id);


    }
}
