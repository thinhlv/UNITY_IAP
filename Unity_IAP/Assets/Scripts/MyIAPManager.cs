using UnityEngine;
using System.Collections;
using UnityEngine.Purchasing;
using System;
using UnityEngine.Purchasing.Security;

public class MyIAPManager : IStoreListener
{
    private IStoreController controller;
    private IExtensionProvider extensions;

    public MyIAPManager()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct("100_gold_coins_google", ProductType.Consumable, new IDs
        {
            {"100_gold_coins_google", GooglePlay.Name},
            {"100_gold_coins_mac", MacAppStore.Name }
        });

        UnityPurchasing.Initialize(this, builder);
    }

    /// <summary>
    /// call when unity IAP is ready to make purchase
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="extensions"></param>
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
        this.extensions = extensions;

        //restore purchase if user reinstall app
        //for apple app

        extensions.GetExtension<IAppleExtensions>().RestoreTransactions(result =>
        {
            if (result)
            {
                //infom that restore complete
                //this does not mean everything was restored
            }
            else
            {
                //restore failed
            }
        });

        extensions.GetExtension<IAppleExtensions>().RefreshAppReceipt(receipt =>
        {
            // This handler is invoked if the request is successful.
            // Receipt will be the latest app receipt.
            Console.WriteLine(receipt);
        }, () =>
        {
            // This handler will be invoked if the request fails,
            // such as if the network is unavailable or the user
            // enters the wrong password.
        });
    }

    /// <summary>
    /// call when unity IAP encounter an unrecoverable  init error
    /// </summary>
    /// <param name="error"></param>
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// call when purchase fails
    /// handler all purchase fails: purchase failure, Network error, device setting, payment falure,...
    /// </summary>
    /// <param name="i"></param>
    /// <param name="p"></param>
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        if (p == PurchaseFailureReason.PurchasingUnavailable)
        {
            //IAP may be disable in device setting
        }
    }

    /// <summary>
    /// call when purchase completes
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        bool validPurchase = true;
        //Unity IAP validation is only include on these platform
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX
        var validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.bundleIdentifier);
        try
        {
            //On google play, result has single product ID
            //On apple stores, receipts contain multiple product
            var result = validator.Validate(e.purchasedProduct.receipt);
            Debug.Log("Receipt is valid. Contents:");
            foreach (IPurchaseReceipt productReceipt in result)
            {
                Debug.Log(productReceipt.productID);
                Debug.Log(productReceipt.purchaseDate);
                Debug.Log(productReceipt.transactionID);

                GooglePlayReceipt google = productReceipt as GooglePlayReceipt;
                if (null != google)
                {
                    // This is Google's Order ID.
                    // Note that it is null when testing in the sandbox
                    // because Google's sandbox does not provide Order IDs.
                    Debug.Log(google.transactionID);
                    Debug.Log(google.purchaseState);
                    Debug.Log(google.purchaseToken);
                }

                AppleInAppPurchaseReceipt apple = productReceipt as AppleInAppPurchaseReceipt;
                if (null != apple)
                {
                    Debug.Log(apple.originalTransactionIdentifier);
                    Debug.Log(apple.subscriptionExpirationDate);
                    Debug.Log(apple.cancellationDate);
                    Debug.Log(apple.quantity);
                }

            }
        }
        catch (IAPSecurityException)
        {
            Debug.Log("Invalid receipt, not unlocking content");
            validPurchase = false;
        }
#endif
        if (validPurchase)
        {
            // Unlock the appropriate content here.
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseClicked(string productId)
    {
        controller.InitiatePurchase(productId);
    }
}
