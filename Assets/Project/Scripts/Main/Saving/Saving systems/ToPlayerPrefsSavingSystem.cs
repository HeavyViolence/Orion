using Newtonsoft.Json;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Orion.Main.Saving
{
    public sealed class ToPlayerPrefsSavingSystem : SavingSystem
    {
        private const string _SaveName = "Orion";

        protected override void Save(IEnumerable<KeyValuePair<string, string>> states)
        {
            string jsonStates = JsonConvert.SerializeObject(states);
            PlayerPrefs.SetString(_SaveName, jsonStates);
            PlayerPrefs.Save();
        }

        protected override bool TryLoad(out IEnumerable<KeyValuePair<string, string>> states)
        {
            if (PlayerPrefs.HasKey(_SaveName) == true)
            {
                try
                {
                    string jsonStates = PlayerPrefs.GetString(_SaveName, string.Empty);
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