using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSpent.Core.Contracts;
using System.ServiceModel;
using TimeSpent.Common;
using TimeSpent.Core.Exceptions;
using TimeSpent.Client.Entities;

namespace TimeSpent.Client.Contracts
{
    [ServiceContract]
    public interface ICategoryService: IServiceContract
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Category GetCategory(int id);

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
