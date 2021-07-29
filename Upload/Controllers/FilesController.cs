using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Upload.Factory;
using Upload.FilesData;
using Upload.Models;

namespace Upload.Controllers
{
    [ApiController]
    public class FilesController : ControllerBase
    {
        private IFilesData _filesData;
        private IWebHostEnvironment _webHostEnvironment;

        public FilesController(IFilesData filesData, IWebHostEnvironment webHostEnvironment)
        {
            _filesData = filesData;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Route("api/[controller]/currency/{curr}")]
        public async Task<IActionResult> GetFilesByCurrency(string curr)
        {
            return Ok(await _filesData.GetFilesByCurrency(curr));
        }

        [HttpGet]
        [Route("api/[controller]/{d1}/{d2}")]
        public async Task<IActionResult> GetFilesByDataRange(DateTime d1, DateTime d2)
        {
            return Ok(await _filesData.GetFilesByDataRange(d1, d2));
        }

        [HttpGet]
        [Route("api/[controller]/status/{s}")]
        public async Task<IActionResult> GetFilesByStatus(string s)
        {
            return Ok(await _filesData.GetFilesByStatus(s));
        }

        [HttpPost]
        [Route("api/[controller]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UploadFiles(IFormFile file)
        {
            try
            {
                Dictionary<string, IFileExt> type = new Dictionary<string, IFileExt>();
                List<Files> ListFiles = new List<Files>();
                string path = _webHostEnvironment.WebRootPath + "\\Files\\";
                FileInfo fi = new FileInfo(file.FileName);

                if (file.Length > 0)
                {
                    type.Add("csv", new CSV(file));
                    type.Add("xml", new XML(file));

                    if (fi.Extension != "csv" && fi.Extension == "xml")
                        return NotFound("Unknown File Format");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    IFileExt newCSV = type[fi.Extension];
                    
                    await _filesData.AddData(newCSV.getDataFromFile(path));

                    return Ok(ListFiles);
                    
                }
                //return "Failed";
                return BadRequest("Invalid Data");
            }
            catch(Exception ex)
            {
                return BadRequest("Invalid Data. \n" + ex.Message.ToString());
            }
        }

    }
}
