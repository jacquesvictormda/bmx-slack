using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Inedo.BuildMaster.Extensibility.Operations;
using Inedo.BuildMaster;
using Inedo.BuildMaster.Extensibility;
using Inedo.BuildMaster.Extensibility.Agents;
using Inedo.ExecutionEngine;
using Inedo.ExecutionEngine.Executer;
using System.Threading;

namespace BuildMasterExtension.Tests
{
    [TestClass()]
    public class SlackNotifyOperationTests
    {
        const string WehookURL = "https://hooks.slack.com/services/T00000000/B00000000/XXXXXXXXXXXXXXXXXXXXXXXX";
        [TestMethod()]
        public async Task ExecuteAsyncTest()
        {
            SlackNotifyOperation op = new SlackNotifyOperation();

            op.WebhookURL = WehookURL;
            op.Channel = "#general";
            op.Message = "This is a test message run from a unit test in BuildMasterExtension.SlackNotifyOperationTests";
            
            await op.ExecuteAsync(new ContextStub());
        }

        [TestMethod()]
        public async Task ExecuteAsyncFailureTest()
        {
            SlackNotifyOperation op = new SlackNotifyOperation();

            op.WebhookURL = WehookURL;
            op.Channel = "#channel-doesnt-exist";
            op.Message = "This is a test message run from a unit test in BuildMasterExtension.SlackNotifyOperationTests";

            var ex = await AssertEx.ThrowsAsync(() => op.ExecuteAsync(new ContextStub()));
        }

    }

    public static class AssertEx
    {
        public static async Task<TException>
          ThrowsAsync<TException>(Func<Task> action,
          bool allowDerivedTypes = true) where TException : Exception
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                if (allowDerivedTypes && !(ex is TException))
                    throw new Exception("Delegate threw exception of type " +
                      ex.GetType().Name + ", but " + typeof(TException).Name +
                      " or a derived type was expected.", ex);
                if (!allowDerivedTypes && ex.GetType() != typeof(TException))
                    throw new Exception("Delegate threw exception of type " +
                      ex.GetType().Name + ", but " + typeof(TException).Name +
                      " was expected.", ex);
                return (TException)ex;
            }
            throw new Exception("Delegate did not throw expected exception " +
              typeof(TException).Name + ".");
        }
        public static Task<Exception> ThrowsAsync(Func<Task> action)
        {
            return ThrowsAsync<Exception>(action, true);
        }
    }

    class ContextStub : IOperationExecutionContext
    {
        public BuildMasterAgent Agent
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int? ApplicationGroupId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int? ApplicationId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string BuildNumber
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public CancellationToken CancellationToken
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public DeployableInfo Deployable
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int? DeployableId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int? EnvironmentId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int ExecutionId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ExecutionStatus ExecutionStatus
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int? PromotionId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string ReleaseNumber
        {
            get
            {
                return "0.1";
            }
        }

        public int? ServerId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int? ServerRoleId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool Simulation
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string WorkingDirectory
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int? IGenericBuildMasterContext.ExecutionId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public RuntimeValue ExpandVariables(string text)
        {
            throw new NotImplementedException();
        }

        public Task<RuntimeValue> ExpandVariablesAsync(string text)
        {
            throw new NotImplementedException();
        }

        public BuildMasterAgent GetAgent(string serverName)
        {
            throw new NotImplementedException();
        }

        public Task<BuildMasterAgent> GetAgentAsync(string serverName)
        {
            throw new NotImplementedException();
        }

        public DeployableInfo GetDeployable(string deployableName)
        {
            throw new NotImplementedException();
        }

        public string ResolvePath(string path)
        {
            throw new NotImplementedException();
        }

        public void SetVariableValue(RuntimeVariableName variableName, RuntimeValue variableValue)
        {
            throw new NotImplementedException();
        }

        public void SetVariableValue(string variableName, RuntimeValue variableValue)
        {
            throw new NotImplementedException();
        }

        public RuntimeValue? TryGetFunctionValue(string functionName, params RuntimeValue[] args)
        {
            throw new NotImplementedException();
        }

        public RuntimeValue? TryGetVariableValue(RuntimeVariableName variableName)
        {
            throw new NotImplementedException();
        }

        public RuntimeValue? TryGetVariableValue(string variableName)
        {
            throw new NotImplementedException();
        }
    }
}