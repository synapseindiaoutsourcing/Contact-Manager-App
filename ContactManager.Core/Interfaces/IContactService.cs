using ContactManager.Core.DTO;
using ContactManager.Core.Provider;
using ContactManager.Core.Responses;


namespace ContactManager.Core.Interfaces
{
    public interface IContactService
    {

        Task<ResponseProvider<List<ContactsResponse>>> GetContactsAsync();
        Task<ResponseProvider<ContactsResponse>> GetContactDetailsAsync(int Id);
        Task<ResponseProvider<ContactsResponse>> RemoveContactAsync(int Id);
        Task<ResponseProvider<ContactsResponse>> CreateOrUpdateContactAsync(ContactsDTO dto);
    }
}
