using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using SharedClassDLL;

namespace SharedClassDLL
{
    /// <summary>
    /// Interface for the communication.
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
