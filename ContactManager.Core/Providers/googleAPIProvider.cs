using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ContactManager.Core.Providers
{
    public static class googleAPIProvider
    {
        public static List<double> GetCoordinates(string address,string key)
        {
            string message = string.Empty;
            List<double> coordinates = new List<double>();
            try
            {

                string addr = address.Trim();                
                string requestUri = string.Format(
                    "https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}",
                    Uri.EscapeDataString(addr),
                    Uri.EscapeDataString(key));

                JArray objects = GetRequest(requestUri);

                if (objects.Count > 0)
                {
                    var lat = objects[0]["results"][0]["geometry"]["location"]["lat"];
                    var lng = objects[0]["results"][0]["geometry"]["location"]["lng"];
                    if (!(lat == null || lng == null))
                    {
                        coordinates.Add(Convert.ToDouble(lat));
                        coordinates.Add(Convert.ToDouble(lng));
                    }
                    else
                    {
                        coordinates.Add(0);
                        coordinates.Add(0);
                    }

                }

            }
            catch (Exception ex)
            {
                message = ex.Message + "," + ex.StackTrace;

            }

            return coordinates;


        }

        private static JArray GetRequest(string url)
        {
            //Sends requests and returns json array results

            JArray objects = new JArray { };
            try
            {

                WebRequest request = WebRequest.Create(url);
                // Set the credentials.
                // Get the response.
                HttpWebResponse response = null;
                try
                {
                    // This is where the HTTP GET actually occurs.
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (Exception ex)
                {
                    objects = JArray.Parse("[\"Error\"]");
                    //Write Error into Log File
                }
                // Display the status. You want to see "OK" here.
                var des = response.StatusDescription;
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);
                // Read the content. This is the Json that represents all the orders .
                string responseFromServer = reader.ReadToEnd();



                // change this to array.
                var json = "[" + responseFromServer + "]";
                // parse as array 
                objects = JArray.Parse(json);


                // Cleanup the streams and the response.
                reader.Close();
                dataStream.Close();
                response.Close();


            }
            catch (Exception ex)
            {
                objects = JArray.Parse("[\"Error\"]");
                //Write Error into Log File
            }


            return objects;
        }
    }
}
