using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SME_WEB_ApiManagement.Models;
using System.Net;

namespace SME_WEB_ApiManagement.DAO
{
    
    public class SystemDAO
    {
        private readonly IConfiguration _configuration;
        protected static string APIpath;

        public SystemDAO(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public static List<MSystemModels> GetSystem(string apipath = null, string TokenStr = null)
        {


            APIpath = apipath + "MSystem";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(APIpath);
            httpWebRequest.ContentType = "application/json";
            //  httpWebRequest.Headers.Add("Authorization", "Bearer " + TokenStr);
            httpWebRequest.Method = "GET";
            List<MSystemModels> Llist = new List<MSystemModels>();
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    var result = streamReader.ReadToEnd();
                    Llist = JsonConvert.DeserializeObject<List<MSystemModels>>(result);
                    return Llist;

                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static List<MRegisterModels> GetRegister(ViewRegisterApiModels vm, string apipath = null, string flagCount = null, int currentpage = 0, int PageSize = 0, string TokenStr = null)
        {


            APIpath = apipath + "MRegister/GetRegisterBySearch";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(APIpath);
            httpWebRequest.ContentType = "application/json";
            //  httpWebRequest.Headers.Add("Authorization", "Bearer " + TokenStr);
            httpWebRequest.Method = "POST";
            List<MRegisterModels> Llist = new List<MRegisterModels>();
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    MRegisterModels Req; // กำหนด Type ชัดเจน
                    if (vm.MRegister != null)
                    {
                        Req = vm.MRegister;
                    }
                    else
                    {
                        Req = new MRegisterModels();
                    }
                    if (flagCount != "Y")
                    {
                        Req.rowOFFSet = (currentpage - 1) * PageSize;
                        Req.rowFetch = PageSize;
                    }
                    else
                    {
                        Req.rowOFFSet = 0;
                        Req.rowFetch = 0;

                    }
                    var json = JsonConvert.SerializeObject(Req, Formatting.Indented);


                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    var result = streamReader.ReadToEnd();
                    Llist = JsonConvert.DeserializeObject<List<MRegisterModels>>(result);
                    return Llist;

                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static int? UpsertRegister(UpSertRegisterApiModels vm =null,string apipath = null, string TokenStr = null)
        {
           

            APIpath = apipath + "MRegister/UpsertRegister";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(APIpath);
            httpWebRequest.ContentType = "application/json";
            //  httpWebRequest.Headers.Add("Authorization", "Bearer " + TokenStr);
            httpWebRequest.Method = "POST";
            int? Llist = 0;
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var Req = vm;


                    var json = JsonConvert.SerializeObject(Req, Formatting.Indented);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    var result = streamReader.ReadToEnd();
                    Llist = JsonConvert.DeserializeObject<int?>(result);
                    return Llist;

                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static int? UpsertSystemApi(UpSerTSystemApiMappingModels vm = null, string apipath = null, string TokenStr = null)
        {


            APIpath = apipath + "TSystemAPI/UpsertSystemApi";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(APIpath);
            httpWebRequest.ContentType = "application/json";
            //  httpWebRequest.Headers.Add("Authorization", "Bearer " + TokenStr);
            httpWebRequest.Method = "POST";
            int? Llist = 0;
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var Req = vm;


                    var json = JsonConvert.SerializeObject(Req, Formatting.Indented);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    var result = streamReader.ReadToEnd();
                    Llist = JsonConvert.DeserializeObject<int?>(result);
                    return Llist;

                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<TApiPermisionMappingModels> GetTApiMappingBySearch(TApiPermisionMappingModels vm, string apipath = null, string TokenStr = null)
        {


            APIpath = apipath + "TAPIMapping/GetTApiMappingBySearch";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(APIpath);
            httpWebRequest.ContentType = "application/json";
            //  httpWebRequest.Headers.Add("Authorization", "Bearer " + TokenStr);
            httpWebRequest.Method = "POST";
            List<TApiPermisionMappingModels> Llist = new List<TApiPermisionMappingModels>();
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    TApiPermisionMappingModels Req; // กำหนด Type ชัดเจน
                    if (vm != null)
                    {
                        Req = vm;
                    }
                    else
                    {
                        Req = new TApiPermisionMappingModels();
                    }

                    var json = JsonConvert.SerializeObject(Req, Formatting.Indented);


                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    var result = streamReader.ReadToEnd();
                    Llist = JsonConvert.DeserializeObject<List<TApiPermisionMappingModels>>(result);
                    return Llist;

                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static List<TSystemApiMappingModels> GetTSystemMappingBySearch(MSystemModels vm, string apipath = null, string TokenStr = null)
        {


            APIpath = apipath + "TSystemAPI/GetTSystemMappingBySearch";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(APIpath);
            httpWebRequest.ContentType = "application/json";
            //  httpWebRequest.Headers.Add("Authorization", "Bearer " + TokenStr);
            httpWebRequest.Method = "POST";
            List<TSystemApiMappingModels> Llist = new List<TSystemApiMappingModels>();
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    MSystemModels Req; // กำหนด Type ชัดเจน
                    if (vm != null)
                    {
                        Req = vm;
                    }
                    else
                    {
                        Req = new MSystemModels();
                    }

                    var json = JsonConvert.SerializeObject(Req, Formatting.Indented);


                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    var result = streamReader.ReadToEnd();
                    Llist = JsonConvert.DeserializeObject<List<TSystemApiMappingModels>>(result);
                    return Llist;

                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<MSystemModels> GetSystemBySearch(MSystemModels vm, string apipath = null ,string flagCount=null, int currentpage = 0, int PageSize = 0, string TokenStr = null)
        {


            APIpath = apipath + "MSystem/GetSystemBySearch";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(APIpath);
            httpWebRequest.ContentType = "application/json";
            //  httpWebRequest.Headers.Add("Authorization", "Bearer " + TokenStr);
            httpWebRequest.Method = "POST";
            List<MSystemModels> Llist = new List<MSystemModels>();
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    MSystemModels Req; // กำหนด Type ชัดเจน
                  
                    if (vm != null)
                    {
                        Req = vm;
                    }
                    else
                    {
                        Req = new MSystemModels();
                    }

                    if (flagCount != "Y")
                    {
                        Req.rowOFFSet = (currentpage - 1) * PageSize;
                        Req.rowFetch = PageSize;
                    }
                    else
                    {
                        Req.rowOFFSet = 0;
                        Req.rowFetch = 0;
                      
                    }
                    var json = JsonConvert.SerializeObject(Req, Formatting.Indented);


                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    var result = streamReader.ReadToEnd();
                    Llist = JsonConvert.DeserializeObject<List<MSystemModels>>(result);
                    return Llist;

                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static int? UpsertSystem(MSystemModels vm = null, string apipath = null, string TokenStr = null)
        {


            APIpath = apipath + "MSystem/UpsertSystem";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(APIpath);
            httpWebRequest.ContentType = "application/json";
            //  httpWebRequest.Headers.Add("Authorization", "Bearer " + TokenStr);
            httpWebRequest.Method = "POST";
            int? Llist = 0;
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var Req = vm;


                    var json = JsonConvert.SerializeObject(Req, Formatting.Indented);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    var result = streamReader.ReadToEnd();
                    Llist = JsonConvert.DeserializeObject<int?>(result);
                    return Llist;

                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string? DeleteSystem(string id =null,  string apipath = null, string TokenStr = null)
        {


            APIpath = apipath + "MSystem/"+id;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(APIpath);
            httpWebRequest.ContentType = "application/json";
            //  httpWebRequest.Headers.Add("Authorization", "Bearer " + TokenStr);
            httpWebRequest.Method = "DELETE";
          
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    var result = streamReader.ReadToEnd();
                   string Llist = JsonConvert.DeserializeObject<string>(result);
                    return Llist;

                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static int? UpsertOrg(MOrganizationModels vm = null, string apipath = null, string TokenStr = null)
        {


            APIpath = apipath + "MOrganization/UpsertOrg";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(APIpath);
            httpWebRequest.ContentType = "application/json";
            //  httpWebRequest.Headers.Add("Authorization", "Bearer " + TokenStr);
            httpWebRequest.Method = "POST";
            int? Llist = 0;
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var Req = vm;


                    var json = JsonConvert.SerializeObject(Req, Formatting.Indented);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    var result = streamReader.ReadToEnd();
                    Llist = JsonConvert.DeserializeObject<int?>(result);
                    return Llist;

                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static List<MOrganizationModels> GetOrgBySeach(MOrganizationModels vm, string apipath = null, string flagCount = null, int currentpage = 0, int PageSize = 0, string TokenStr = null)
        {


            APIpath = apipath + "MOrganization/GetOrgBySeach";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(APIpath);
            httpWebRequest.ContentType = "application/json";
            //  httpWebRequest.Headers.Add("Authorization", "Bearer " + TokenStr);
            httpWebRequest.Method = "POST";
            List<MOrganizationModels> Llist = new List<MOrganizationModels>();
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    MOrganizationModels Req; // กำหนด Type ชัดเจน
                    if (vm != null)
                    {
                        Req = vm;
                    }
                    else
                    {
                        Req = new MOrganizationModels();
                    }
                    if (flagCount != "Y")
                    {
                        Req.rowOFFSet = (currentpage - 1) * PageSize;
                        Req.rowFetch = PageSize;
                    }
                    else
                    {
                        Req.rowOFFSet = 0;
                        Req.rowFetch = 0;

                    }
                    var json = JsonConvert.SerializeObject(Req, Formatting.Indented);


                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    var result = streamReader.ReadToEnd();
                    Llist = JsonConvert.DeserializeObject<List<MOrganizationModels>>(result);
                    return Llist;

                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string? DeleteOrg(string id = null, string apipath = null, string TokenStr = null)
        {


            APIpath = apipath + "MOrganization/" + id;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(APIpath);
            httpWebRequest.ContentType = "application/json";
            //  httpWebRequest.Headers.Add("Authorization", "Bearer " + TokenStr);
            httpWebRequest.Method = "DELETE";

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {

                    var result = streamReader.ReadToEnd();
                    string Llist = JsonConvert.DeserializeObject<string>(result);
                    return Llist;

                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
