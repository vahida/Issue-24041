using System.Collections.Generic;

namespace BareBoneMembershipApi
{
    public class ApiResponse<T>
    {
        /// <summary>
        /// Gets or sets the status code (HTTP status code)
        /// </summary>
        /// <value>The status code.</value>
        public string Code { get; set; }

        public string Status { get;  set; }

        public List<string> Message { get; set; }

        /// <summary>
        /// Gets or sets the HTTP headers
        /// </summary>
        /// <value>HTTP headers</value>
        public IDictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Gets or sets the data (parsed HTTP body)
        /// </summary>
        /// <value>The data.</value>
        public T Data { get; set; }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="ApiResponse&lt;T&gt;" /> class.
        ///// </summary>
        ///// <param name="statusCode">HTTP status code.</param>
        ///// <param name="headers">HTTP headers.</param>
        ///// <param name="data">Data (parsed HTTP body)</param>
        //public ApiResponse(int statusCode, IDictionary<string, string> headers, T data)
        //{
        //    this. = statusCode;
        //    this.Headers = headers;
        //    this.Data = data;
        //}

    }
}
