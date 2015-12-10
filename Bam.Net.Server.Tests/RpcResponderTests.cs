using Bam.Net.CommandLine;
using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;
using Newtonsoft.Json.Linq;
using System.IO;
using Bam.Net.Server.Rpc;
using FakeItEasy;
using Bam.Net.ServiceProxy;
using Bam.Net.Incubation;

namespace Bam.Net.Server.Tests
{
    [Serializable]
    public class RpcResponderTests: CommandLineTestInterface
    {
        [UnitTest]
        public void ParseJArrayTest()
        {
            object parseResult = JToken.Parse("['one','two','three']");
            
            OutLine(parseResult.GetType().Name, ConsoleColor.Yellow);
            OutLine(parseResult.PropertiesToString(), ConsoleColor.Cyan);
            Expect.IsTrue(parseResult.Is<JArray>());
        }

        [UnitTest]
        public void JTokenProperties()
        {
            JToken parseResult = JToken.Parse("{name: 'monkey', tails: 'prehensile', friends: ['g','h','b']}");
                    
            OutLine(parseResult.GetType().Name, ConsoleColor.Yellow);
            OutLine(parseResult.PropertiesToString(), ConsoleColor.Cyan);
            OutLine(parseResult["friends"].GetType().Name);

            Expect.IsTrue(parseResult.Is<JObject>());
            Expect.IsTrue(parseResult["friends"].Is<JArray>());
            Expect.IsNull(parseResult["notthere"]);
        }
        
        [UnitTest("RpcResponder: Parse request without id returns notification")]
        public void ParseRequestWithoutIdShouldReturnNotification()
        {
            IRequest request = A.Fake<IRequest>();
            A.CallTo<string>(() => request.HttpMethod).Returns("POST");
            A.CallTo<Stream>(() => request.InputStream).Returns(GetNotificationStream());
            IRpcRequest msg = RpcMessage.Parse(request);
            Expect.IsInstanceOfType<RpcNotification>(msg);
        }

        [UnitTest("RpcResponder: ParseRequest with id should return RpcMessage")]
        public void ParseRequestWithIdShouldReturnRpcMessage()
        {
            IRequest request = A.Fake<IRequest>();
            A.CallTo<string>(() => request.HttpMethod).Returns("POST");
            A.CallTo<Stream>(() => request.InputStream).Returns(GetRequestStream());
            IRpcRequest msg = RpcMessage.Parse(request);
            Expect.IsInstanceOfType<RpcRequest>(msg);
        }
        [UnitTest("RpcResponder: ParseArray should return RpcBatch")]
        public void ParseRequestArrayShouldReturnRpcBatch()
        {
            IRequest request = A.Fake<IRequest>();
            A.CallTo<string>(() => request.HttpMethod).Returns("POST");
            A.CallTo<Stream>(() => request.InputStream).Returns(GetRequestBatch());
            IRpcRequest msg = RpcMessage.Parse(request);
            Expect.IsInstanceOfType<RpcBatch>(msg);
        }

        [UnitTest("RpcResponder: ParseRequest with ordered params")]
        public void ParseRequestWithOrderedParams()
        {
            IRequest request = A.Fake<IRequest>();
            A.CallTo<string>(() => request.HttpMethod).Returns("POST");
            A.CallTo<Stream>(() => request.InputStream).Returns(GetRequestWithOrderedParamsStream());
            IRpcRequest msg = RpcMessage.Parse(request);
            Expect.IsInstanceOfType<RpcRequest>(msg);
            RpcRequest rpcRequest = (RpcRequest)msg;
            Expect.IsTrue(rpcRequest.RpcParams.Ordered);
            Expect.IsTrue(rpcRequest.RpcParams.By.Position != null);
            Expect.IsFalse(rpcRequest.RpcParams.Named);
            Expect.IsTrue(rpcRequest.RpcParams.By.Name == null);
            Expect.IsNotNull(rpcRequest.Params);
            Expect.IsTrue(rpcRequest.Params.Is<JArray>(), "Params should have been a JArray");
        }

        [UnitTest("RpcResponder: ParseRequest with named params")]
        public void ParseRequestWithNamedParams()
        {
            IRequest request = A.Fake<IRequest>();
            A.CallTo<string>(() => request.HttpMethod).Returns("POST");
            A.CallTo<Stream>(() => request.InputStream).Returns(GetRequestWithNamedParamsStream());
            IRpcRequest msg = RpcMessage.Parse(request);
            Expect.IsInstanceOfType<RpcRequest>(msg);
            RpcRequest rpcRequest = (RpcRequest)msg;
            Expect.IsFalse(rpcRequest.RpcParams.Ordered);
            Expect.IsTrue(rpcRequest.RpcParams.By.Position == null);
            Expect.IsTrue(rpcRequest.RpcParams.Named);
            Expect.IsTrue(rpcRequest.RpcParams.By.Name != null);
            Expect.IsNotNull(rpcRequest.Params);
            Expect.IsTrue(rpcRequest.Params.Is<JObject>());
        }

        [UnitTest("RpcResponder: Execute ordered parameters")]
        public void ExecuteOrderedParamsTest()
        {
            Incubator inc = new Incubator();
            inc.Set(new Echo());
            string value = "hello there ".RandomLetters(8);
            object id = "some value";
            string inputString = "{{'jsonrpc': '2.0', 'method': 'Send', 'id': '{0}', 'params': ['{1}']}}"._Format(id, value);
            IHttpContext context = GetPostContextWithInput(inputString);            
            IRpcRequest parsed = RpcMessage.Parse(context);
            RpcRequest request = (RpcRequest)parsed;
            request.Incubator = inc;
            RpcResponse response = request.Execute();
            Expect.IsTrue(response.GetType().Equals(typeof(RpcResponse)));
            Expect.AreEqual(value,response.Result);
            Expect.IsNull(response.Error);
            Expect.AreEqual(id, response.Id);
        }

        [UnitTest("RpcResponder: Execute named parameters")]
        public void ExecuteNamedParamsTest()
        {
            Incubator inc = new Incubator();
            inc.Set(new Echo());
            string id = "A Value";
            RpcRequest request = new RpcRequest();
            request.Incubator = inc;
            string value = "hello there ".RandomLetters(8);
            request.Params = JToken.Parse("{{'value': '{0}'}}"._Format(value));
            request.RpcParams.By.Name = request.Params;
            request.Method = "Send";
            request.Id = id;

            IHttpContext context = GetPostContextWithInput(request.ToJson());
            request = (RpcRequest)RpcMessage.Parse(context);
            request.Incubator = inc;

            RpcResponse response = request.Execute();
            Expect.IsTrue(response.GetType().Equals(typeof(RpcResponse)));
            Expect.AreEqual(response.Result, value);
            Expect.IsNull(response.Error);
            Expect.AreEqual(request.Id, response.Id);
            Expect.AreEqual(id, response.Id);
        }

        [UnitTest("RpcResponder: Execute with object parameters")]
        public void ExecuteParameterWithObjectParamters()
        {
            Incubator inc = new Incubator();
            inc.Set(new Echo());            
            EchoData data = new EchoData();
            data.BoolProperty = true;
            data.StringProperty = "dlhsddfflk";
            data.IntProperty = 888;

            RpcRequest request = RpcRequest.Create<Echo>(inc, "TestObjectParameter", data, "some addditional stuff");
            RpcResponse response = request.Execute();
            Expect.IsNotNull(response.Result);
            OutLine(response.Result.ToString(), ConsoleColor.Cyan);
        }

        private Stream GetNotificationStream()
        {
            return GetStream("{'jsonrpc': '2.0', 'method': 'ToString'}");
        }

        private Stream GetRequestStream()
        {
            return GetStream("{'jsonrpc': '2.0', 'method': 'ToString', 'id': 'blahdeeblahdee'}");
        }

        private Stream GetRequestWithOrderedParamsStream()
        {
            return GetStream("{'jsonrpc': '2.0', 'method': 'DoSomething', 'id': 'blahdeeblahdee', 'params': [1,2,'three']}");
        }
        private Stream GetRequestWithNamedParamsStream()
        {
            return GetStream("{'jsonrpc': '2.0', 'method': 'DoSomething', 'id': 'blahdeeblahdee', 'params': {'argOne': 'value', 'arg2': 1}}");
        }

        private Stream GetRequestBatch()
        {
            string request = "[";
            request += "{'jsonrpc': '2.0', 'method': 'DoSomething', 'id': 'blahdeeblahdee', 'params': [1,2,'three']},\r\n";
            request += "{'jsonrpc': '2.0', 'method': 'DoSomething', 'id': 'kjhkj', 'params': {'argOne': 'value', 'arg2': 1}}\r\n";
            request += "]";
            return GetStream(request);
        }

        private IHttpContext GetPostContextWithInput(string input, Func<string, Stream> inputStreamFactory = null)
        {
            return GetContextWithInput("POST", input, inputStreamFactory);
        }

        private IHttpContext GetContextWithInput(string httpMethod, string input, Func<string, Stream> streamFactory = null)
        {
            streamFactory = streamFactory ?? GetStream;
            IHttpContext context = GetContext(httpMethod, () => streamFactory(input));
            return context;
        }

        private IHttpContext GetContext(string httpMethod, Func<Stream> inputStreamFactory)
        {
            IHttpContext context = A.Fake<IHttpContext>();
            IRequest request = A.Fake<IRequest>();
            IResponse response = A.Fake<IResponse>();
            A.CallTo<string>(() => request.HttpMethod).Returns(httpMethod.ToUpperInvariant());
            A.CallTo<IRequest>(() => context.Request).Returns(request);
            A.CallTo<IResponse>(() => context.Response).Returns(response);
            A.CallTo<Stream>(() => request.InputStream).Returns(inputStreamFactory());
            return context;
        }

        private Stream GetExecutableTestBatch()
        {
            return GetStream(@"[
                {'jsonrpc': '2.0', 'method': 'send', 'params': [1,2,4], 'id': '1'},
                {'jsonrpc': '2.0', 'method': 'notify_hello', 'params': [7]},
                {'jsonrpc': '2.0', 'method': 'subtract', 'params': [42,23], 'id': '2'},
                {'foo': 'boo'},
                {'jsonrpc': '2.0', 'method': 'foo.get', 'params': {'name': 'myself'}, 'id': '5'},
                {'jsonrpc': '2.0', 'method': 'get_data', 'id': '9'} 
            ]");            
        }

        private Stream GetStream(string content)
        {
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            sw.Write(content);
            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);            
            return ms;
        }
    }
}
