using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using SharedClassDLL;
using System.Runtime.Serialization;


namespace SharedClassDLL
{
    /// <summary>
    /// Interface for the communication. Contains two Methods to send data to the client.
    /// </summary>
    [ServiceContract]
    public interface RuleSyncInterface
    {
        [OperationContract]
        bool IsInitialized();

        [OperationContract]
        string[] GetAvailableRuleSets();

        [OperationContract]
        RuleSet GetSpecificRule(string nameOfRuleSet);
    }
}
