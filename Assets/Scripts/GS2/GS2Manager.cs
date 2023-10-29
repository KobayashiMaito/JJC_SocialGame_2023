    using System.Collections;
    using Gs2.Unity.Util;
    using UnityEngine;
    
    public class GS2Manager : MonoBehaviour
    {
        IEnumerator Start()
        {
            // Setup variables
    
            var clientId = "YourClientId";
            var clientSecret = "YourClientSecret";
            var accountNamespaceName = "game-0001";
            var accountEncryptionKeyId = "grn:gs2:{region}:{ownerId}:key:account-encryption-key-namespace:key:account-encryption-key";
            
            // Setup general setting
            var profile = new Profile(
                clientId,
                clientSecret,
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
                accountNamespaceName
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
    
            // Log-in created anonymous account
    
// Gs2AccountAuthenticator.Gs2AccountAuthenticator(Gs2WebSocketSession, Gs2RestSession, string, string, string, string, GatewaySetting, VersionSetting)

            var loginFuture = profile.LoginFuture(
                new Gs2AccountAuthenticator(
                    profile.Gs2Session,
                    profile.Gs2RestSession,
                    accountNamespaceName,
                    accountEncryptionKeyId,
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
                accountNamespaceName
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
            
            // Finalize GS2-SDK
    
            yield return profile.Finalize();
        }
    }