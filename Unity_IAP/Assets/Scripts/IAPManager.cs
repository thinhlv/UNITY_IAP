#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// You must obfuscate your secrets using Window > Unity IAP > Receipt Validation Obfuscator
// before receipt validation will compile in this sample.
// #define RECEIPT_VALIDATION
#endif

using UnityEngine;
using System.Collections;
using UnityEngine.Purchasing;
using System;
#if RECEIPT_VALIDATION
using UnityEngine.Purchasing.Security;
#endif

public class IAPManager : MonoBehaviour, IStoreListener
{

    #region [Properties]
    private IStoreController _controller;
    private IExtensionProvider _extension;
    private bool _isPurchaseInprogress;

#if RECEIPT_VALIDATION
    private CrossPlatformValidator validator;
#endif

    #endregion

    #region [Implement]
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        _controller = controller;
        _extension = extensions;

        foreach (var item in controller.products.all)
        {
            if (item.availableToPurchase)
            {
                Debug.Log(string.Join(" - ",
                    new[]
                    {
                        item.metadata.localizedTitle,
                        item.metadata.localizedDescription,
                        item.metadata.isoCurrencyCode,
                        item.metadata.localizedPrice.ToString(),
                        item.metadata.localizedPriceString,
                        item.transactionID,
                        item.receipt
                    }));
            }
        }
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("failed to initialize!");
        switch (error)
        {
            case InitializationFailureReason.AppNotKnown:
                Debug.LogError("Is your App correctly uploaded on the relevant publisher console?");
                break;
            case InitializationFailureReason.PurchasingUnavailable:
                Debug.Log("Billing disabled!");
                break;
            case InitializationFailureReason.NoProductsAvailable:
                Debug.Log("No products available for purchase!");
                break;
        }
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        Debug.Log("Purchase OK: " + e.purchasedProduct.definition.id);
        Debug.Log("Receipt: " + e.purchasedProduct.receipt);

        _isPurchaseInprogress = false;
#if RECEIPT_VALIDATION
        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            try
            {
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
                return PurchaseProcessingResult.Complete;
            }
        }
#endif


        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Debug.Log("Purchase failed: " + i.definition.id);
        Debug.Log(p);
        _isPurchaseInprogress = false;
    }
    #endregion

    #region [Command Method]
    public void BuyItem1()
    {
        Debug.Log("Buy Item 1");
        _controller.InitiatePurchase(_controller.products.all[0]);
        _isPurchaseInprogress = true;
    }

    public void BuyItem2()
    {
        Debug.Log("Buy Item 2");
        _controller.InitiatePurchase(_controller.products.all[1]);
        _isPurchaseInprogress = true;
    }

    public void BuyItem3()
    {
        Debug.Log("Buy Item 3");
        _controller.InitiatePurchase(_controller.products.all[2]);
        _isPurchaseInprogress = true;
    }

    public void BuyItem4()
    {
        Debug.Log("Buy Item 4");
        _controller.InitiatePurchase(_controller.products.all[3]);
        _isPurchaseInprogress = true;
    }

    public void BuyItem5()
    {
        Debug.Log("Buy Item 5");
        _controller.InitiatePurchase(_controller.products.all[4]);
        _isPurchaseInprogress = true;
    }

    public void BuyItem6()
    {
        Debug.Log("Buy Item 6");
        _controller.InitiatePurchase(_controller.products.all[5]);
        _isPurchaseInprogress = true;
    }

    public void BuyItem7()
    {
        Debug.Log("Buy Item 7");
        _controller.InitiatePurchase(_controller.products.all[6]);
        _isPurchaseInprogress = true;
    }

    public void BuyItem8()
    {
        Debug.Log("Buy Item 8");
        _controller.InitiatePurchase(_controller.products.all[7]);
        _isPurchaseInprogress = true;
    }
    #endregion

    #region [Unity Method]
    public void Awake()
    {
        //this configure will make all purchase will return success
        var module = StandardPurchasingModule.Instance();
        module.useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
        var builder = ConfigurationBuilder.Instance(module);
        builder.Configure<IMicrosoftConfiguration>().useMockBillingSystem = true;

        //define our product
        for (int i = 1; i <= 8; i++)
        {
            builder.AddProduct("item" + i, ProductType.Consumable, new IDs
            {
                {"item" + i, GooglePlay.Name }
            });
        }

#if RECEIPT_VALIDATION
		validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.bundleIdentifier);
#endif

        //Initialize Unity IAP
        UnityPurchasing.Initialize(this, builder);
    }

    public void OnUpdate()
    {
    }
    #endregion

    #region [Extra Tools]

    #endregion
}
