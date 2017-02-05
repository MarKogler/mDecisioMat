using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

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
        bool ReadDataFromCsvFile();
    }
}
