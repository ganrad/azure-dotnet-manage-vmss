using System;
using System.Collections.Generic;

namespace core.Interfaces
{
    public interface IParseInputs
    {
       public void GetEnvVars(ref Dictionary<string, string> envVars);
    }
}
