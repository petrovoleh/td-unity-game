using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System.Text;
//using SharedLibrary;

namespace _Scripts {
    public static class HttpClient
    {
        public static async Task<T> Post<T>(string endpoint, object payload, string token = null) {
            var postRequest = CreateRequest(endpoint, RequestType.POST, payload);
            if (token != null)
                postRequest.SetRequestHeader("Authorization", token);
            postRequest.SendWebRequest();

            while (!postRequest.isDone) await Task.Delay(10);
            return  JsonUtility.FromJson<T>(postRequest.downloadHandler.text);
        }

        public static async Task<T> Get<T>(string endpoint, string token) {
            var getRequest = CreateRequest(endpoint);
            getRequest.SetRequestHeader("Authorization", token);
            getRequest.SendWebRequest();
            while (!getRequest.isDone) await Task.Delay(10);
            return  JsonUtility.FromJson<T>(getRequest.downloadHandler.text);
        }

        private static UnityWebRequest CreateRequest(string path, RequestType type = RequestType.GET, object data = null){
            path="http://193.219.91.103:5756/"+path;
            var request = new UnityWebRequest(path, type.ToString());
            request.certificateHandler = new CertificateWhore();
            if (data != null) {
                var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            };

            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            return request;
        }

        private static void AttachHeader(UnityWebRequest request, string key, string value) {
            request.SetRequestHeader(key, value);
        }
    }

    public enum RequestType {
        GET = 0,
        POST = 1,
        PUT = 2
    }
}
