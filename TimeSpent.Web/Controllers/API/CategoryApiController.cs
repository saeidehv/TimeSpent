using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeSpent.Web.Core;
using System.ComponentModel.Composition;
using TimeSpent.Client.Contracts;
using TimeSpent.Core.Contracts;
using TimeSpent.Web.Models;
using TimeSpent.Client.Entities;

namespace TimeSpent.Web.Controllers.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    [RoutePrefix("api/category")]
    [UsesDisposableService]
    public class CategoryApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CategoryApiController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_categoryService);
        }

        ICategoryService _categoryService;

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage AddCategory(HttpRequestMessage request, [FromBody]CategoryModel categoryModel)
        {
            return GetHttpResponse(request, () =>
           {
               HttpResponseMessage response = null;
               Category c = new Category {CategoryId = categoryModel.CategoryId , Name = categoryModel.Name, Description = categoryModel.Description };
               Category category = _categoryService.UpdateCategory(c);
               response = request.CreateResponse(HttpStatusCode.OK);

               return response;
           });
        }

        [HttpGet]
        [Route("list")]
        public HttpResponseMessage GetCategoryList(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
           {
               HttpResponseMessage response = null;
               Category[] list = _categoryService.GetAllCategories();
               
               response = request.CreateResponse<Category[]>(HttpStatusCode.OK, list);

               return response;
           });
        }

        [HttpPost]
        [Route("delete")]
        public HttpResponseMessage DeleteCategory(HttpRequestMessage request, [FromBody]int categoryId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _categoryService.DeleteCategory(categoryId);
                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
            
        }
    }
}
