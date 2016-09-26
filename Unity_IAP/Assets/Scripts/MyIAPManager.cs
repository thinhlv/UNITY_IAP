using UnityEngine;
using System.Collections;
using UnityEngine.Purchasing;
using System;

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

        /*extensions.GetExtension<IAppleExtensions>().RestoreTransactions(result =>
        {
            if(result)
            {
                //infom that restore complete
                //this does not mean everything was restored
            }
            else
            {
                //restore failed
            }
        });*/
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
        if(p == PurchaseFailureReason.PurchasingUnavailable)
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
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseClicked(string productId)
    {
        controller.InitiatePurchase(productId);
    }
}
