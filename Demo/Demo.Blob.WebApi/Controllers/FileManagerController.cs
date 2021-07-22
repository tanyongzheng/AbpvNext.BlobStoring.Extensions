using System.Threading.Tasks;
using Demo.Blob.WebApi.ApplicationServices;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Demo.Blob.WebApi.Controllers
{
    /*
    [ApiController]
    [Route("[controller]")]
    public class FileManagerController : AbpController
    {
        / *
        private readonly BlobManagerAppService _boBlobManagerAppService;

        public FileManagerController(BlobManagerAppService boBlobManagerAppService)
        {
            _boBlobManagerAppService = boBlobManagerAppService;
        }
        * /
        [HttpGet]
        [Route("test-save")]
        public async Task<bool> TestSaveAsync()
        {
            //await _boBlobManagerAppService.SaveAsync();
            return true;
        }

        [HttpGet]
        [Route("test-save")]
        public async Task<string> TestGetUrlAsync()
        {
            //return await _boBlobManagerAppService.GetUrlAsync();
            return "12321";
        }
    }
    */

}