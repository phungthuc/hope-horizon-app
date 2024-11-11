using UnityEngine.AddressableAssets;

namespace Game.Scripts.Extensions
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