using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SimplifiedS3Bucket.Models;
using System.Web;
using SimplifiedS3Bucket.Services.Interfaces;

namespace SimplifiedS3Bucket.Controllers;

public class HomeController : Controller
{
    private readonly IFileHandler _fileHandler;
    private readonly ILogger<HomeController> _logger;
    private readonly string _filePath = string.Empty;
    public HomeController(ILogger<HomeController> logger, string filePath, IFileHandler fileHandler)
    {
        _logger = logger;
        _filePath = filePath;
        _fileHandler = fileHandler;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(List<IFormFile> postedFiles)
    {
        string statusMessage = string.Empty;
        try
        {
            if (_fileHandler.UploadFile(postedFiles, _filePath, out string processedfile))
            {
                statusMessage = $"<b>{processedfile}</b> uploaded successfully.<br />";
                //Task.Delay(5000).Wait();
                ViewData["FileName"] = processedfile;
                _logger.LogInformation($"File: {processedfile} uploaded successfully.");
            }
            else
            {
                statusMessage = "<b>Only text file of size upto 1 kb allowed to upload.</b> <br />";
            }

        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in File upload. Error message : {ex.Message}, StackTrace : {ex.StackTrace}");
            statusMessage = "Error while uploading file. Please contact system administrator.";
        }

        ViewData["Message"] = statusMessage;
        return View();
    }

    [Route("Get/{filename}")]
    // Download file from the server
    public async Task<IActionResult> Get(string filename)
    {
        var memory = new MemoryStream();
        string fileWithPath = string.Empty;

        try
        {
            //Validate file is uploaded or not
            if (filename == null || string.IsNullOrEmpty(_fileHandler.FileAlreadyUploaded(filename)))
                return Content("Filename is not availble");
            fileWithPath = Path.Combine(_filePath, filename);

            memory = await _fileHandler.DownloadFile(fileWithPath);
            _logger.LogInformation($"File: {filename} downloaded successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in File Download. Error message : {ex.Message}, StackTrace : {ex.StackTrace}");
            ViewData["Message"] = "Error while downloading file. Please contact system administrator.";
        }
        return File(memory, _fileHandler.GetContentType(fileWithPath), filename);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
