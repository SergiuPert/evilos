using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using TMPro;

public class GPGSManager : MonoBehaviour
{
    private PlayGamesClientConfiguration clientConfiguration;

    //public TextMeshProUGUI log;
    //public TextMeshProUGUI loadedData;
    [SerializeField]
    private GameObject notSignedInPanel;
    [SerializeField]
    private GameObject signedInPanel;

    private void Start()
    {
        if(Social.localUser.authenticated)
        {
            notSignedInPanel.SetActive(false);
            signedInPanel.SetActive(true);
        }
        else
        {
            ConfigureGPGS();
            SignIntoGPGS(SignInInteractivity.CanPromptOnce, clientConfiguration);
        }
    }

    internal void ConfigureGPGS()
    {
        clientConfiguration = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames()
            .Build();
    }

    internal void SignIntoGPGS(SignInInteractivity interactivity, PlayGamesClientConfiguration configuration)
    {
        configuration = clientConfiguration;
        PlayGamesPlatform.InitializeInstance(configuration);
        PlayGamesPlatform.Activate();

        PlayGamesPlatform.Instance.Authenticate(interactivity, (code) =>
        {
            if (code == SignInStatus.Success)
            {
                Debug.Log("Successfully Authenticated");
                //log.text = "Successfully Authenticated";
                Debug.Log("Hello " + Social.localUser.userName + "You have an ID of " + Social.localUser.id); /////////////////////////
                //log.text += "Hello " + Social.localUser.userName + "You have an ID of " + Social.localUser.id; /////////////////////////
                OpenSave(false);
                notSignedInPanel.SetActive(false);
                signedInPanel.SetActive(true);
            }
            else
            {
                Debug.Log("Failed to Authenticate");
                //log.text += "Failed to Authenticate";
                Debug.Log("Failed to Authenticate, reason: " + code);
                //log.text += "Failed to Authenticate, reason: " + code;
            }
        });
    }

    public void BasicSignInBtn()
    {
        ConfigureGPGS();
        SignIntoGPGS(SignInInteractivity.CanPromptAlways, clientConfiguration);
    }

    public void SignOutBtn()
    {
        PlayGamesPlatform.Instance.SignOut();
        signedInPanel.SetActive(false);
        notSignedInPanel.SetActive(true);
        //log.text = "Logged Out";
    }

    #region SavedGames

    private bool isSaving;
    public void OpenSave(bool saving)
    {
        if (Social.localUser.authenticated)
        {
            //log.text += " %User Authenticated& ";
            isSaving = saving;
            ((PlayGamesPlatform)Social.Active).SavedGame.FetchAllSavedGames(DataSource.ReadCacheOrNetwork, CheckForSaves); // check first, if there are saves, if there are no saves make a new one
        }
        else
        {
            GameManager.Instance.ChangeScene();
        }
    }

    private void CheckForSaves(SavedGameRequestStatus status, List<ISavedGameMetadata> saves)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (saves.Count == 0)
            {
                //log.text += " %Making new save& ";
                ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution("SaveFile", DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, CreateNewSave);
            }
            else
            {
                //log.text += " %Save exists& ";
                ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution("SaveFile", DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, SaveGameOpen);
            }
        }
        else
        {
            Debug.Log("DUMNEZEI");
        }
    }

    private void CreateNewSave(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        if (status == SavedGameRequestStatus.Success)
        {

            UserSave newSave = new UserSave();
            //log.text += " %1& ";

            newSave.UserId = Social.localUser.id;
            //log.text += " %2& ";

            newSave.Name = Social.localUser.userName;
            //log.text += " %3& ";

            GameManager.Instance.userSave = newSave;
            //log.text += " %4& ";

            // convert the UserSave object to a byte array
            byte[] myData = ObjectToByteArray(newSave);
            //log.text += " %5& ";

            //update the metadata
            SavedGameMetadataUpdate updateForMetadata = new SavedGameMetadataUpdate.Builder().WithUpdatedDescription("Updated at: " + DateTime.Now.ToString()).Build();
            //log.text += " %Saving new save& ";

            // commit the save
            ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(meta, updateForMetadata, myData, SaveCallBack);
        }
        else
        {
            //log.text += " %failed saving new save& " + status;
        }
    }

    private void SaveGameOpen(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (isSaving) // saving
            {
                //log.text += " %Saving& ";

                // get the user info from the game manager for the save
                UserSave user = GameManager.Instance.userSave;
                byte[] saveData = ObjectToByteArray(user);
                //update the metadata
                SavedGameMetadataUpdate updateForMetadata = new SavedGameMetadataUpdate.Builder().WithUpdatedDescription("Updated at: " + DateTime.Now.ToString()).Build();

                // commit the save
                ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(meta, updateForMetadata, saveData, SaveCallBack);
            }
            else // loading
            {
                //log.text += " %Loading& ";

                ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(meta, LoadCallback);
            }
        }
    }

    private void SaveCallBack(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("File saved successfully");
            GameManager.Instance.ChangeScene();
            //I CAN TELL THE GAME MANAGER THAT THE SAVE IS DONE AND THEN LAUNCH AN EVENT TO LOAD SCENE OR SOMETHING ELSE
            //log.text += " %Save Succesful& ";
        }
        else
        {
            Debug.Log("File save failed");
            //log.text += " %Save failed& ";

        }
    }

    private void LoadCallback(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            //log.text += " %Loaded data& ";

            UserSave user = ByteArrayToObject(data);
            GameManager.Instance.userSave = user;
            //loadedData.text += " % " + GameManager.Instance.userSave.Name + " & ";
            //loadedData.text += " % " + user.Name + " & ";
            //loadedData.text += " % " + GameManager.Instance.userSave.Gold + " & ";
            //loadedData.text += " % " + user.Gold + " & ";

        }
    }

    public byte[] ObjectToByteArray(UserSave obj)
    {
        if (obj == null)
            return null;
        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();
        bf.Serialize(ms, obj);
        return ms.ToArray();
    }

    public UserSave ByteArrayToObject(byte[] arrBytes)
    {
        MemoryStream memStream = new MemoryStream();
        BinaryFormatter binForm = new BinaryFormatter();
        memStream.Write(arrBytes, 0, arrBytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);
        UserSave obj = (UserSave)binForm.Deserialize(memStream); /////////// think about using extended classes instead of modifying the user class itself
        return obj;
    }

    #endregion
}
