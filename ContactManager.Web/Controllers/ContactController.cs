using Azure;
using ContactManager.Core.DTO;
using ContactManager.Core.Entities;
using ContactManager.Core.Interfaces;
using ContactManager.Core.Providers;
using ContactManager.Core.Responses;
using ContactManager.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IContactService _contactService;
        private readonly IConfiguration _configuration;
        public ContactController(ILogger<ContactController> logger, IContactService contactService, IConfiguration configuration)
        {
            _logger = logger;
            _contactService = contactService;
            _configuration = configuration;
        }
        /*
        Declares an asynchronous action method named  Index that returns a task which eventually produces an IActionResult
        Calls the GetContactsAsync method of _contactService to asynchronously retrieve a list of contacts.The await keyword is used to wait for the task to complete
        Returns a view, passing list.Data (the data containing the list of contacts) to the view. This data will be used to display the contact information in the view.
        */
        public async Task<IActionResult> Index()
        {
            var list = await _contactService.GetContactsAsync();
            return View(list.Data);
        }
        /*
        this action method responds to HTTP GET requests. This is typically used for actions that display data or return a form
        Declares an asynchronous action method named Add that returns a task which eventually produces an IActionResult
        Instantiates a new ContactsDTO object. This object is used to transfer data between the controller and the view
        Returns a view, passing the newly created ContactsDTO object to the view. This object will be used to populate the form in the view for adding a new contact
         */
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var dto = new ContactsDTO();

            return View(dto);
        }
        /*
         this action method responds to HTTP POST requests. This is typically used for form submissions.
        Declares an asynchronous action method named Add that takes a ContactsDTO object as a parameter and returns a task which eventually produces an IActionResult.
        Checks if the model state is valid, meaning all required fields are correctly filled out and any validation rules are satisfied.
        Getting Calls the GetCoordinates method to get the latitude and longitude for the provided address.
        Setting Calls the contact service to create or update the contact asynchronously.
        Redirects to the Index action upon successful operation.
        If the model state is not valid, displays an error message and returns the view with the provided DTO to show validation errors
         */
        [HttpPost]
        public async Task<IActionResult> Add(ContactsDTO dTO)
        {

            if (ModelState.IsValid)
            {
                string key = string.Empty;
                key = _configuration["AppSettings:googleAPIKey"];
                var lstLatLong = googleAPIProvider.GetCoordinates(Convert.ToString(dTO.Address), key);
                if (lstLatLong.Count() > 0)
                {
                    dTO.Lat = lstLatLong.FirstOrDefault().ToString();
                    dTO.Long = lstLatLong.LastOrDefault().ToString();
                }

                await _contactService.CreateOrUpdateContactAsync(dTO);

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Please fill required fields.";
                return View(dTO);
            }
        }

        /*
        Declares an asynchronous action method named EditDetails that takes an int parameter id and returns a task which eventually produces a JsonResult
        Calls the GetContactDetailsAsync method of _contactService to asynchronously retrieve the contact details for the specified id. The await keyword is used to wait for the task to complete.
        Checks if the retrieved contact details (dto) are not null, indicating that the contact was found.
        If the contact details are found, returns a JSON response with a result of true and includes the retrieved contact details in the response property
        If the contact details are not found, returns a JSON response with a result of false and includes a failure message in the message property.
         */
        public async Task<JsonResult> EditDetails(int id)
        {
            var dto = await _contactService.GetContactDetailsAsync(id);
            if (dto != null)
            {
                return Json(new { result = true, response = dto.Data });
            }
            else
            {
                return Json(new { result = false, message = "failed" });
            }


        }

        /*
        Declares an asynchronous action method named UpdateContact that takes multiple string parameters for contact details and returns a task which eventually produces a JsonResult
        Calls the contact service to create or update the contact asynchronously.
        If the contact details are found, returns a JSON response with a result of true and includes the retrieved contact details in the response property
        If the contact details are not found, returns a JSON response with a result of false and includes a failure message in the message property.
         */
        public async Task<JsonResult> UpdateContact(string Id, string FirstName, string LastName, string PhoneNumber, string EmailAddress, string DOB, string CompanyName, string Address)
        {

            ContactsDTO dto = new ContactsDTO();
            try
            {
                string key = string.Empty;
                key = _configuration["AppSettings:googleAPIKey"];
                var lstLatLong = googleAPIProvider.GetCoordinates(Convert.ToString(Address), key);

                dto.ContactId = Convert.ToInt32(Id);
                dto.FirstName = FirstName;
                dto.LastName = LastName;
                dto.PhoneNumber = PhoneNumber;
                dto.EmailAddress = EmailAddress;
                dto.DOB = Convert.ToDateTime(DOB);
                dto.CompanyName = CompanyName;
                dto.Address = Address;
                if (lstLatLong.Count() > 0)
                {
                    dto.Lat = lstLatLong.FirstOrDefault().ToString();
                    dto.Long = lstLatLong.LastOrDefault().ToString();
                }

                var response = await _contactService.CreateOrUpdateContactAsync(dto);
                if (response != null)
                {
                    return Json(new { result = true, message = "success" });
                }
                else
                {
                    return Json(new { result = false, message = "failed" });
                }


            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message });
            }


        }

        /*
        Declares an asynchronous action method named Delete that takes an integer parameter id and returns a task which eventually produces a JsonResult
        Calls the RemoveContactAsync method of _contactService to asynchronously remove the contact with the specified id. The await keyword is used to wait for the task to complete
        Checks if the response from the contact service is not null, indicating that the contact was successfully removed.
        If the response is not null, returns a JSON response with a result of true and a message of "success".
        If the response is null, returns a JSON response with a result of false and a message of "failed".
         */
        public async Task<JsonResult> Delete(int id)
        {
            var response = await _contactService.RemoveContactAsync(id);

            if (response != null)
            {
                return Json(new { result = true, message = "success" });
            }
            else
            {
                return Json(new { result = false, message = "failed" });
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
