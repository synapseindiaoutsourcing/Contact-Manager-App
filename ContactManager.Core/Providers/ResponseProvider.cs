
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ContactManager.Core.Provider
{
    
    public class ResponseProvider<T>
    {
        public ResponseMessage? ResponseMessage { get; set; }
        public T? Data { get; set; }

    }

    public class ResponseListProvider<T>
    {
        public ResponseMessage? ResponseMessage { get; set; }
        public List<T>? Data { get; set; }

    }

    public class ResponsePagingListProvider<T>
    {
        public ResponseMessage? ResponseMessage { get; set; }
        public List<T>? Data { get; set; }
        public long? TotalCount { get; set; }

    }

    public class ResponseMessage
    {
        //
        // Summary:
        //     Gets or sets the reason phrase which typically is sent by servers together with
        //     the status code.
        //
        // Returns:
        //     The reason phrase sent by the server.
         public string? ReasonPhrase { get; set; }
       
        //
        // Summary:
        //     Gets or sets the status code of the HTTP response.
        //
        // Returns:
        //     The status code of the HTTP response.
        public HttpStatusCode StatusCode{get; set;}

        //
        // Summary:
        //     Gets a value that indicates if the HTTP response was successful.
        //
        // Returns:
        //     A value that indicates if the HTTP response was successful. true if System.Net.Http.HttpResponseMessage.StatusCode
        //     was in the range 200-299; otherwise false.
        public bool IsSuccessStatusCode { get; set; }
    }

    public class ResponseProviderBuilder<T>
    {
        public static ResponseProvider<T> GetResponseMessage(string reasonPhrase , HttpStatusCode statusCode , bool isSuccessStatusCode, T? data)
        {

            
                return new ResponseProvider<T>()
                {

                    ResponseMessage = new ResponseMessage()
                    {
                        ReasonPhrase = reasonPhrase,
                        StatusCode = statusCode,
                        IsSuccessStatusCode = isSuccessStatusCode

                    },
                    Data = data

                };
            

            
        }
    }

    public class ResponseListProviderBuilder<T>
    {
        public static ResponseListProvider<T> GetResponseMessage(string reasonPhrase, HttpStatusCode statusCode, bool isSuccessStatusCode, List<T> data)
        {


            return new ResponseListProvider<T>()
            {

                ResponseMessage = new ResponseMessage()
                {
                    ReasonPhrase = reasonPhrase,
                    StatusCode = statusCode,
                    IsSuccessStatusCode = isSuccessStatusCode

                },
                Data = data,
            };



        }
    }

    public class ResponsePagingListProviderBuilder<T>
    {
        public static ResponsePagingListProvider<T> GetResponseMessage(string reasonPhrase, HttpStatusCode statusCode, bool isSuccessStatusCode, List<T> data, long? TotalCount)
        {


            return new ResponsePagingListProvider<T>()
            {

                ResponseMessage = new ResponseMessage()
                {
                    ReasonPhrase = reasonPhrase,
                    StatusCode = statusCode,
                    IsSuccessStatusCode = isSuccessStatusCode

                },
                Data = data,
                TotalCount = TotalCount
            };



        }
    }


}
