using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using HelperLibrary.Repositories.Interfaces;
using HelperLibrary.Response;

namespace AzureFunctionCrud
{
    public class AzureCrud
    {
        public  readonly IEmployeeRepository _employeeRepository;
        public  AzureCrud(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [FunctionName("GetAll")]
        public async Task<IActionResult> GetAll(
         [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAll")] HttpRequest req,
    ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                var List = await _employeeRepository.GetAllAsync();
                return new OkObjectResult(new SharedResponse(true, 200, "", List));
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new SharedResponse(false, 400, ex.Message, null));
            }
        }

        //[FunctionName("GetById")]
        //public async Task<IActionResult> GetById(
        //  [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetById/{id}")] HttpRequest req, string id)
        //{
        //    try
        //    {
        //        var record = await _employeeRepository.GetByIdAsync(id);
        //        if (record == null)
        //            return new OkObjectResult(new SharedResponse(false, 404, "No Record Found", null));

        //        else
        //            return new OkObjectResult(new SharedResponse(true, 200, "", record));
        //        //string id = req.Query["id"];
        //    }
        //    catch (Exception ex)
        //    {
        //        return new OkObjectResult(new SharedResponse(false, 400, ex.Message, null));
        //    }
        //}

        //[FunctionName("Create")]
        //public async Task<IActionResult> Create(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Create")] HttpRequest req)
        //{
        //    try
        //    {
        //        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //        var data = JsonConvert.DeserializeObject<EmployeeDetails>(requestBody);
        //        await _employeeRepository.InsertAsync(data);
        //        return new OkObjectResult(new SharedResponse(true, 200, "Record Insert Successfully", null));
        //    }
        //    catch (Exception ex)
        //    {
        //        return new OkObjectResult(new SharedResponse(false, 400, ex.Message, null));
        //    }
        //}

        //[FunctionName("Update")]
        //public async Task<IActionResult> Update(
        //   [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "Update/{id}")] HttpRequest req, string id)
        //{
        //    try
        //    {
        //        var record = _employeeRepository.GetByIdAsync(id);
        //        if (record == null)
        //            return new OkObjectResult(new SharedResponse(false, 404, "No Record Found", null));

        //        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //        var data = JsonConvert.DeserializeObject<EmployeeDetails>(requestBody);
        //        await _employeeRepository.UpdateAsync(id, data);

        //        return new OkObjectResult(new SharedResponse(true, 200, "Record Insert Successfully", null));
        //    }
        //    catch (Exception ex)
        //    {
        //        return new OkObjectResult(new SharedResponse(false, 400, ex.Message, null));
        //    }
        //}

        //[FunctionName("Delete")]
        //public async Task<IActionResult> Delete(
        //  [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Delete/{id}")] HttpRequest req, string id)
        //{
        //    try
        //    {
        //        var record = _employeeRepository.GetByIdAsync(id);
        //        if (record == null)
        //            return new OkObjectResult(new SharedResponse(false, 404, "No Record Found", null));
        //        else
        //        {
        //            await _employeeRepository.DeleteAsync(id);
        //            return new OkObjectResult(new SharedResponse(true, 200, "Record Deleted Successfully", null));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new OkObjectResult(new SharedResponse(false, 400, ex.Message, null));
        //    }
        //}
    }
}
