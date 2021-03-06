#region Copyright notice and license

// Copyright 2019 The gRPC Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using Shared;
using System.Net.Http;

namespace Client
{
    public class Program
    {
        static async Task Main(string[] args)
        {

            var httpHandler = new HttpClientHandler();

            // WARNING: Return `true` to allow certificates that are untrusted/invalid!
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = channel.CreateGrpcService<IGreeterService>();

            var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);

            Console.WriteLine("Shutting down");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
