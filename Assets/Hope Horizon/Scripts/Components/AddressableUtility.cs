using UnityEngine.AddressableAssets;

namespace HopeHorizon.Scripts.Components
{
    public static class AddressableUtility
    {
        public static bool IsAddressableKeyExists(string key)
        {
            var handle = Addressables.LoadResourceLocationsAsync(key);
            handle.WaitForCompletion();
            return handle.Result.Count > 0;
        }
    }
}
