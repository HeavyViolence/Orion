using System;
using System.Collections.Generic;

using UnityEngine;

using VContainer.Unity;

namespace Orion.Main.Saving
{
    public abstract class SavingSystem : IInitializable, IDisposable
    {
        private Dictionary<string, string> _states;
        private readonly HashSet<ISavable> _savables = new(new ISavableEqualityComparer());

        private bool _initialized = false;

        public bool Register(ISavable savable)
        {
            if (_initialized == false)
            {
                if (TryLoad(out var states) == true)
                {
                    _states = new(states);
                }
                else
                {
                    _states = new();
                }
                
                _initialized = true;
            }

            if (_savables.Add(savable ?? throw new ArgumentNullException(nameof(savable))) == true)
            {
                if (_states.TryGetValue(savable.StateName, out string state) == true)
                {
                    savable.SetState(state);
                }

                return true;
            }

            return false;
        }

        public bool Deregister(ISavable savable)
        {
            if (_savables.Remove(savable ?? throw new ArgumentNullException(nameof(savable))) == true)
            {
                _states[savable.StateName] = savable.GetState();
                return true;
            }

            return false;
        }

        public void Initialize()
        {
            Application.wantsToQuit += OnApplicationWantsToQuit;
        }

        public void Dispose()
        {
            Application.wantsToQuit -= OnApplicationWantsToQuit;
        }

        private bool OnApplicationWantsToQuit()
        {
            foreach (var entry in CaptureStates())
            {
                _states[entry.Key] = entry.Value;
            }

            Save(_states);
            return true;
        }

        private IEnumerable<KeyValuePair<string, string>> CaptureStates()
        {
            Dictionary<string, string> states = new();

            foreach (ISavable savable in _savables)
            {
                states[savable.StateName] = savable.GetState();
            }

            return states;
        }

        protected abstract void Save(IEnumerable<KeyValuePair<string, string>> states);
        protected abstract bool TryLoad(out IEnumerable<KeyValuePair<string, string>> states);
    }
}