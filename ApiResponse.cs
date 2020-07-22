using System.Collections.Generic;

namespace BareBoneMembershipApi
{
    public class ApiResponse<T>
    {
        public string Code { get; set; }
        public string Status { get;  set; }
        public List<string> Message { get; set; }
        public IDictionary<string, string> Headers { get; set; }
        public T Data { get; set; }
    }
}
