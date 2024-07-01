using ContactManager.Core.DTO;
using ContactManager.Core.Entities;
using ContactManager.Core.Interfaces;
using ContactManager.Core.Provider;
using ContactManager.Core.Responses;
using System.Net;
using System.Runtime.InteropServices;

namespace ContactManager.Core.Services
{
    public class ContactService : IContactService
    {
        private readonly ILogService _logService;
        private readonly IAsyncRepository<Contacts> _contactsRepository;
        public ContactService(
            ILogService logService,
            IAsyncRepository<Contacts> contactsRepository

            ) {
            _logService = logService;
            _contactsRepository = contactsRepository;
        }

        public async Task<ResponseProvider<List<ContactsResponse>>> GetContactsAsync()
        {
            try
            {
                var contacts = _contactsRepository.GetAllQuerable().
                    Select(s => new ContactsResponse()
                    {
                       Address = s.Address,
                       CompanyName= s.CompanyName,
                       Id=s.Id,
                       EmailAddress=s.EmailAddress,
                       FirstName=s.FirstName,
                       LastName=s.LastName,
                       Lat= s.Lat,
                       Long=s.Long,
                       PhoneNumber= s.PhoneNumber,
                       DOB = s.DOB
                    }).ToList().OrderBy(m => m.FirstName).ToList();
                return ResponseProviderBuilder<List<ContactsResponse>>.GetResponseMessage("Success.", HttpStatusCode.OK, true, contacts);
            }
            catch (Exception exception)
            {

                //log error to db
                await _logService.CreateLog(0, exception);

                //return error response
                return ResponseProviderBuilder<List<ContactsResponse>>.GetResponseMessage("Server error occurred, please contact admin.", HttpStatusCode.InternalServerError, false, null);
            }
        }
        public async Task<ResponseProvider<ContactsResponse>> GetContactDetailsAsync(int Id)
        {
            try
            {
                var contact = _contactsRepository.GetAllQuerable().Where(s => s.Id == Id).
                    Select(s => new ContactsResponse()
                    {
                        Address = s.Address,
                        CompanyName = s.CompanyName,
                        Id = s.Id,
                        EmailAddress = s.EmailAddress,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Lat = s.Lat,
                        Long = s.Long,
                        PhoneNumber = s.PhoneNumber,
                        DOB=s.DOB,
                    }).FirstOrDefault();
                return ResponseProviderBuilder<ContactsResponse>.GetResponseMessage("Success.", HttpStatusCode.OK, true, contact);
            }
            catch (Exception exception)
            {

                //log error to db
                await _logService.CreateLog(0, exception);

                //return error response
                return ResponseProviderBuilder<ContactsResponse>.GetResponseMessage("Server error occurred, please contact admin.", HttpStatusCode.InternalServerError, false, null);
            }
        }
        public async Task<ResponseProvider<ContactsResponse>> RemoveContactAsync(int Id)
        {
            try
            {
                var contact =await _contactsRepository.FirstOrDefaultAsync(m=>m.Id == Id);
                await _contactsRepository.DeleteAsync(contact);
                return ResponseProviderBuilder<ContactsResponse>.GetResponseMessage("Success.", HttpStatusCode.OK, true, null);
            }
            catch (Exception exception)
            {

                //log error to db
                await _logService.CreateLog(0, exception);

                //return error response
                return ResponseProviderBuilder<ContactsResponse>.GetResponseMessage("Server error occurred, please contact admin.", HttpStatusCode.InternalServerError, false, null);
            }
        }
        public async Task<ResponseProvider<ContactsResponse>> CreateOrUpdateContactAsync(ContactsDTO dto)
        {
            try
            {
                var contact = await _contactsRepository.FirstOrDefaultAsync(m => m.Id == dto.ContactId);
                if (contact == null)
                {
                    var contactEntity = new Contacts()
                    {
                        Address = dto.Address,
                        CompanyName = dto.CompanyName,
                        CreatedDate = DateTime.Now,
                        EmailAddress = dto.EmailAddress,
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        PhoneNumber = dto.PhoneNumber,
                        DOB=dto.DOB,
                        Lat=dto.Lat,
                        Long=dto.Long
                    };
                    await _contactsRepository.AddAsync(contactEntity);
                }
                else
                {
                    contact.Address = dto.Address;
                    contact.CompanyName = dto.CompanyName;
                    contact.CreatedDate = DateTime.Now;
                    contact.EmailAddress = dto.EmailAddress;
                    contact.FirstName = dto.FirstName;
                    contact.LastName = dto.LastName;
                    contact.PhoneNumber = dto.PhoneNumber;
                    contact.DOB = dto.DOB;
                    contact.Lat = dto.Lat;
                    contact.Long = dto.Long;
                    await _contactsRepository.UpdateAsync(contact);
                }
                return ResponseProviderBuilder<ContactsResponse>.GetResponseMessage("Success.", HttpStatusCode.OK, true, null);
            }
            catch (Exception exception)
            {

                //log error to db
                await _logService.CreateLog(0, exception);

                //return error response
                return ResponseProviderBuilder<ContactsResponse>.GetResponseMessage("Server error occurred, please contact admin.", HttpStatusCode.InternalServerError, false, null);
            }

        }
    
    }
}
