﻿using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Tookan.NET.Http
{
    public interface IRequest
    {
        object Body { get; set; }
        Dictionary<string, string> Headers { get; }
        HttpMethod Method { get; }
        Dictionary<string, string> Parameters { get; }
        Uri BaseAddress { get; }
        Uri Endpoint { get; }
        TimeSpan Timeout { get; }
        string ContentType { get; }
        bool AllowAutoRedirect { get; }
    }
}
