using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace WPFPresentationCore
{
  
    public interface IConfigFileService 
    {
        string GetConfigEntryAsString([NotNull] string key);
        int GetConfigEntryAsInt([NotNull] string key);
        bool GetConfigEntryAsBool([NotNull] string key);
        string GetConfigEntryAsPassword(string key, string encryptionKey);
        IEnumerable<string> GetConfigEntryAsStringArray([NotNull] string key);
    }
}
