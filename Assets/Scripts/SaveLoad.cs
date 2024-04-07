using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.CloudSave;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using System.Threading.Tasks;

public class SaveLoad : MonoBehaviour
{
    async void Awake()
    {
        try
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    [Obsolete]
    public async static void SaveData<T>(T inData, string key)
    {
        var data = new Dictionary<string, object> { { key, inData } };
        await CloudSaveService.Instance.Data.ForceSaveAsync(data);
    }

    [Obsolete]
    public async static Task<T> LoadData<T>(string key)
    {
        Dictionary<string, string> savedData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { key } );
        var dataString = savedData[key];
        var data = JsonUtility.FromJson<T>(dataString);
        Debug.Log(savedData[key]);
        return data;
    }
}