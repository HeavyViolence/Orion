using Newtonsoft.Json;

using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;

namespace Orion.Main.Saving
{
    public sealed class ToFileSavingSystem : SavingSystem
    {
        private const string _SaveFileName = "Orion";
        private const string _SaveFileExtension = ".json";

        private string SavePath => Path.Combine(Application.persistentDataPath, _SaveFileName + _SaveFileExtension);

        protected override void Save(IEnumerable<KeyValuePair<string, string>> states)
        {
            string jsonStates = JsonConvert.SerializeObject(states);
            File.WriteAllText(SavePath, jsonStates);
        }

        protected override bool TryLoad(out IEnumerable<KeyValuePair<string, string>> states)
        {
            if (File.Exists(SavePath) == true)
            {
                try
                {
                    string jsonStates = File.ReadAllText(SavePath);
                    states = JsonConvert.DeserializeObject<IEnumerable<KeyValuePair<string, string>>>(jsonStates);
                    return true;
                }
                catch (JsonException)
                {
                    states = Enumerable.Empty<KeyValuePair<string, string>>();
                    return false;
                }
            }

            states = Enumerable.Empty<KeyValuePair<string, string>>();
            return false;
        }
    }
}