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

    // Lưu Access Token
    public static void SetAccessToken(string token)
    {
        string encryptedToken = EncryptionUtils.Encrypt(token);
        PlayerPrefs.SetString(AccessTokenKey, encryptedToken);
        PlayerPrefs.Save();
    }

    // Lấy Access Token
    public static string GetAccessToken()
    {
        string encryptedToken = PlayerPrefs.GetString(AccessTokenKey, string.Empty);
        return string.IsNullOrEmpty(encryptedToken) ? string.Empty : EncryptionUtils.Decrypt(encryptedToken);
    }

    // Lưu Refresh Token
    public static void SetRefreshToken(string token)
    {
        string encryptedToken = EncryptionUtils.Encrypt(token);
        PlayerPrefs.SetString(RefreshTokenKey, encryptedToken);
        PlayerPrefs.Save();
    }

    // Lấy Refresh Token
    public static string GetRefreshToken()
    {
        string encryptedToken = PlayerPrefs.GetString(RefreshTokenKey, string.Empty);
        return string.IsNullOrEmpty(encryptedToken) ? string.Empty : EncryptionUtils.Decrypt(encryptedToken);
    }

    // Xóa Tất Cả Token
    public static void ClearTokens()
    {
        PlayerPrefs.DeleteKey(AccessTokenKey);
        PlayerPrefs.DeleteKey(RefreshTokenKey);
        PlayerPrefs.DeleteKey(TokenExpiryKey);
        PlayerPrefs.Save();
    }

    // Kiểm tra xem người dùng đã đăng nhập chưa
    public static bool IsUserLoggedIn()
    {
        return !string.IsNullOrEmpty(GetAccessToken());
    }

    // Lưu thời gian hết hạn token
    public static void SetTokenExpiry(float expiryTime)
    {
        PlayerPrefs.SetFloat(TokenExpiryKey, expiryTime);
        PlayerPrefs.Save();
    }

    // Lấy thời gian hết hạn token
    public static float GetTokenExpiry()
    {
        return PlayerPrefs.GetFloat(TokenExpiryKey, 0);
    }

    // Kiểm tra thời gian hết hạn
    public static bool IsTokenExpired()
    {
        return Time.time >= GetTokenExpiry();
    }
}
