﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Net.Http;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Proxy
{
    internal sealed class ProxyService
    {
        public ProxyService(IOptions<ProxyOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            Options = options.Value;
#if NET46
            Client = new HttpClient(Options.MessageHandler ?? new WinHttpHandler { AutomaticRedirection = false });
#else
            Client = new HttpClient(Options.MessageHandler ?? new HttpClientHandler { AllowAutoRedirect = false });
#endif
        }

        public ProxyOptions Options { get; private set; }
        public HttpClient Client { get; private set; }
    }
}
