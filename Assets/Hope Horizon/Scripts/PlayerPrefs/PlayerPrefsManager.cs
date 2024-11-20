using UnityEngine;
using HopeHorizon.Scripts.PlayerPrefs;

public static class PlayerPrefsManager
{
    private const string AccessTokenKey = "accessToken";
    private const string RefreshTokenKey = "refreshToken";
    private const string TokenExpiryKey = "tokenExpiry";

    public static void GetAllKeys()
    {
        Debug.Log("AccessTokenKey: " + PlayerPrefs.GetString(AccessTokenKey));
        Debug.Log("RefreshTokenKey: " + PlayerPrefs.GetString(RefreshTokenKey));
        Debug.Log("TokenExpiryKey: " + PlayerPrefs.GetFloat(TokenExpiryKey));
    }

    public static void SetAccessToken(string token)
    {
        string encryptedToken = EncryptionUtils.Encrypt(token);
        PlayerPrefs.SetString(AccessTokenKey, encryptedToken);
        PlayerPrefs.Save();
    }

    public static string GetAccessToken()
    {
        string encryptedToken = PlayerPrefs.GetString(AccessTokenKey, string.Empty);
        return string.IsNullOrEmpty(encryptedToken) ? string.Empty : EncryptionUtils.Decrypt(encryptedToken);
    }

    public static void SetRefreshToken(string token)
    {
        string encryptedToken = EncryptionUtils.Encrypt(token);
        PlayerPrefs.SetString(RefreshTokenKey, encryptedToken);
        PlayerPrefs.Save();
    }

    public static string GetRefreshToken()
    {
        string encryptedToken = PlayerPrefs.GetString(RefreshTokenKey, string.Empty);
        return string.IsNullOrEmpty(encryptedToken) ? string.Empty : EncryptionUtils.Decrypt(encryptedToken);
    }

    public static void ClearTokens()
    {
        PlayerPrefs.DeleteKey(AccessTokenKey);
        PlayerPrefs.DeleteKey(RefreshTokenKey);
        PlayerPrefs.DeleteKey(TokenExpiryKey);
        PlayerPrefs.Save();
    }

    public static bool IsUserLoggedIn()
    {
        return !string.IsNullOrEmpty(GetAccessToken());
    }

    public static void SetTokenExpiry(float expiryTime)
    {
        PlayerPrefs.SetFloat(TokenExpiryKey, expiryTime);
        PlayerPrefs.Save();
    }

    public static float GetTokenExpiry()
    {
        return PlayerPrefs.GetFloat(TokenExpiryKey, 0);
    }

    public static bool IsTokenExpired()
    {
        return Time.time >= GetTokenExpiry();
    }
}
