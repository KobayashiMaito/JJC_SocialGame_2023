    using System.Collections;
    using Gs2.Unity.Util;
    using UnityEngine;

using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Exception;
using Gs2.Unity.Core;
using Gs2.Unity.Gs2Inventory.Model;
using Gs2.Gs2Inventory.Model;
using Gs2.Unity.Gs2Dictionary.Model;


using Gs2.Unity.Gs2Exchange.Model;
using Gs2.Unity.Gs2Experience.Model;
using Google.Protobuf.WellKnownTypes;
using Unity.Properties;
using Gs2.Core.Domain;
using Gs2.Gs2Identifier.Model;

public class GS2Manager : MonoBehaviour
{
    const string CLIENT_ID = "GKITg_ndOnKcFmHHmXNi3OjII7vK0tu7lTZAjbrQCTm2KWw_Qg_CMW5HGBkKVwKdgaXE_ueEXZBUI4q1NNpDUQKvQ==";
    const string CLIENT_SECRET = "ezrNGluCeYhTODHwyZLlHvfmAOboPHJW";

    const string ACCOUNT_NAMESPACE = "Player";
    const string ACCOUNT_ANGOU_KEY_ID = "grn:gs2:{region}:{ownerId}:key:account-encryption-key-namespace:key:account-encryption-key";

    string user_id;
    string password;

    IEnumerator Start()
    {
        // Setup variables
            
        // Setup general setting
        var profile = new Profile(
            CLIENT_ID,
            CLIENT_SECRET,
            reopener: new Gs2BasicReopener()
        );
    
        // Create GS2 client
        var initializeFuture = profile.InitializeFuture();
        yield return initializeFuture;
        if (initializeFuture.Error != null) {
            throw initializeFuture.Error;
        }
        var gs2 = initializeFuture.Result;

        // Create anonymous account

        Debug.Log("Create anonymous account");
            
        var createFuture = gs2.Account.Namespace(
            ACCOUNT_NAMESPACE
        ).Create();
        yield return createFuture;
        if (createFuture.Error != null)
        {
            throw createFuture.Error;
        }
    
        // Load created account
            
        var loadFuture = createFuture.Result.Model();
        yield return loadFuture;
        if (loadFuture.Error != null)
        {
            throw loadFuture.Error;
        }
        var account = loadFuture.Result;
    
        // Dump anonymous account
            
        Debug.Log($"UserId: {account.UserId}");
        Debug.Log($"Password: {account.Password}");

        user_id = account.UserId;
        password = account.Password;

        // Log-in created anonymous account

        // Gs2AccountAuthenticator.Gs2AccountAuthenticator(Gs2WebSocketSession, Gs2RestSession, string, string, string, string, GatewaySetting, VersionSetting)

        var loginFuture = profile.LoginFuture(
            new Gs2AccountAuthenticator(
                profile.Gs2Session,
                profile.Gs2RestSession,
                ACCOUNT_NAMESPACE,
                ACCOUNT_ANGOU_KEY_ID,
                account.UserId,
                account.Password
            )
        );
        yield return loginFuture;
        if (loginFuture.Error != null)
        {
            throw loginFuture.Error;
        }
        var gameSession = loginFuture.Result;

        // Load TakeOver settings

        var it = gs2.Account.Namespace(
            ACCOUNT_NAMESPACE
        ).Me(
            gameSession
        ).TakeOvers();
    
        while (it.HasNext())
        {
            yield return it.Next();
            if (it.Error != null)
            {
                throw it.Error;
            }
            if (it.Current != null)
            {
                // Dump TakeOver setting
                Debug.Log($"Type: {it.Current.Type}");
                Debug.Log($"Identifier: {it.Current.UserIdentifier}");
            }
        }

        // Dictionary.
        {
            var domain = gs2.Dictionary.Namespace(
                namespaceName: "HasCharaDictionary"
            );
            var it_EntryModels = domain.EntryModels(
            );

            // ディクショナリの中に含まれているエントリ情報を全て引き出す.
            List<EzEntryModel> items = new List<EzEntryModel>();
            while (it_EntryModels.HasNext())
            {
                yield return it_EntryModels.Next();
                if (it_EntryModels.Error != null)
                {
                    Gs2ClientHolder clientHolder = GameObject.Find("GS2_UIKitSample").GetComponent<Gs2ClientHolder>();
                    clientHolder.DebugErrorHandler(it_EntryModels.Error, null);
                    break;
                }
                if (it_EntryModels.Current != null)
                {
                    items.Add(it_EntryModels.Current);
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < items.Count; i++)
            {
                Debug.Log(items[i].Name);
            }
        }

        // Exchangeをする.
        {
            var domain = gs2.Exchange.Namespace(
                namespaceName: "Exchange002"
            ).Me(
                gameSession
            ).Exchange(
            );

            var future = domain.Exchange(
                "Exchange002_Chara001",
                1,
                null
            );
            yield return future;
            if (future.Error != null)
            {
                Gs2ClientHolder clientHolder = GameObject.Find("GS2_UIKitSample").GetComponent<Gs2ClientHolder>();
                clientHolder.DebugErrorHandler(future.Error, null);
                yield break;
            }

            var item = future.Result;
        }

        { 
            var domain = gs2.Dictionary.Namespace(
                namespaceName: "HasCharaDictionary"
            ).Me(
                gameSession
            );
            var it_Entries = domain.Entries(
            );
            List<EzEntry> items = new List<EzEntry>();
            while (it_Entries.HasNext())
            {
                yield return it_Entries.Next();
                if (it_Entries.Error != null)
                {
                    Gs2ClientHolder clientHolder = GameObject.Find("GS2_UIKitSample").GetComponent<Gs2ClientHolder>();
                    clientHolder.DebugErrorHandler(it_Entries.Error, null);
                    break;
                }
                if (it_Entries.Current != null)
                {
                    items.Add(it_Entries.Current);
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < items.Count; i++)
            {
                Debug.Log(items[i].Name + "を持っている");
            }
        }


//        yield return 0;
        // Finalize GS2-SDK
        yield return profile.Finalize();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(RefreshList());
        }else if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(ExecExchange("Exchange002_ClearEntry"));
        }
    }

    IEnumerator ExecExchange(string exchangeName)
    {
        // Setup general setting
        var profile = new Profile(
            CLIENT_ID,
            CLIENT_SECRET,
            reopener: new Gs2BasicReopener()
        );

        // Create GS2 client
        var initializeFuture = profile.InitializeFuture();
        yield return initializeFuture;
        if (initializeFuture.Error != null)
        {
            throw initializeFuture.Error;
        }
        var gs2 = initializeFuture.Result;

        // login.

        var loginFuture = profile.LoginFuture(
            new Gs2AccountAuthenticator(
                profile.Gs2Session,
                profile.Gs2RestSession,
                ACCOUNT_NAMESPACE,
                ACCOUNT_ANGOU_KEY_ID,
                user_id,
                password
            )
        );
        yield return loginFuture;
        if (loginFuture.Error != null)
        {
            throw loginFuture.Error;
        }
        var gameSession = loginFuture.Result;

        // Exchangeをする.
        {
            var domain = gs2.Exchange.Namespace(
                namespaceName: "Exchange002"
            ).Me(
                gameSession
            ).Exchange(
            );

            var future = domain.Exchange(
                exchangeName,
                1,
                null
            );
            yield return future;
            if (future.Error != null)
            {
                Gs2ClientHolder clientHolder = GameObject.Find("GS2_UIKitSample").GetComponent<Gs2ClientHolder>();
                clientHolder.DebugErrorHandler(future.Error, null);
                yield break;
            }

            var item = future.Result;
        }

        yield return null;
    }


    IEnumerator RefreshList()
    {
        // Setup general setting
        var profile = new Profile(
            CLIENT_ID,
            CLIENT_SECRET,
            reopener: new Gs2BasicReopener()
        );

        // Create GS2 client
        var initializeFuture = profile.InitializeFuture();
        yield return initializeFuture;
        if (initializeFuture.Error != null)
        {
            throw initializeFuture.Error;
        }
        var gs2 = initializeFuture.Result;

        // Create anonymous account

        Debug.Log("ログインをする.");

        var loginFuture = profile.LoginFuture(
            new Gs2AccountAuthenticator(
                profile.Gs2Session,
                profile.Gs2RestSession,
                ACCOUNT_NAMESPACE,
                ACCOUNT_ANGOU_KEY_ID,
                user_id,
                password
            )
        );
        yield return loginFuture;
        if (loginFuture.Error != null)
        {
            throw loginFuture.Error;
        }
        var gameSession = loginFuture.Result;

        // 所持キャラ一覧を取得.
        {
            var domain_dictionary = gs2.Dictionary.Namespace(
                namespaceName: "HasCharaDictionary"
            ).Me(
                gameSession
            );
            var it_Entries = domain_dictionary.Entries(
            );
            List<EzEntry> items = new List<EzEntry>();
            while (it_Entries.HasNext())
            {
                yield return it_Entries.Next();
                if (it_Entries.Error != null)
                {
                    Gs2ClientHolder clientHolder = GameObject.Find("GS2_UIKitSample").GetComponent<Gs2ClientHolder>();
                    clientHolder.DebugErrorHandler(it_Entries.Error, null);
                    break;
                }
                if (it_Entries.Current != null)
                {
                    items.Add(it_Entries.Current);
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < items.Count; i++)
            {
                Debug.Log(items[i].Name + "を持っている");
            }
        }
    }
}