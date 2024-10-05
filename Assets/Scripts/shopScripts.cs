
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.Purchasing;

public class shopScripts : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
    public string productId = "1";
    public APIcalls apiHolder;

    void Start()
    {
        // Initialize the Purchasing system.
        APIcalls.onPaymentReady += InitializeOnPaymentReady;
        
    }
    void OnDestroy()
    {
        APIcalls.onPaymentReady -= InitializeOnPaymentReady;
    }
    void InitializeOnPaymentReady(){
InitializePurchasing();
    }
    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add a product to sell / restore by way of its identifier, associating the general identifier with its store-specific identifiers.
        foreach (paymentItem item in paymentVars.payments.items)
        {
#if UNITY_ANDROID
            Debug.Log(item.google_product_id);
           builder.AddProduct(item.google_product_id, ProductType.Consumable);
#endif
#if UNITY_IOS
           builder.AddProduct(item.apple_product_id, ProductType.Consumable);
#endif
           Debug.Log("product is here " + item.google_product_id);
           // If you have different product types, you need to adjust this accordingly
        }
#if UNITY_ANDROID
        builder.AddProduct("diamondvip", ProductType.Subscription);
        builder.AddProduct("goldvip", ProductType.Subscription);
        builder.AddProduct("silvervip", ProductType.Subscription);
#endif
#if UNITY_IOS
        builder.AddProduct("diamond", ProductType.Subscription);
        builder.AddProduct("silver", ProductType.Subscription);
        builder.AddProduct("gold", ProductType.Subscription);
#endif
#if UNITY_IOS || UNITY_ANDROID

        UnityPurchasing.Initialize(this, builder);
#endif

        // Initialize the Purchasing system.
        // UnityPurchasing.Initialize(this, builder);
    }
    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing system initialization successful.
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.LogError($"IAP Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        // Handle successful purchases.
        // Access product information from purchaseEvent
        Product product = purchaseEvent.purchasedProduct;
        
        // Print product name and info to the console
        Debug.Log("Purchased: " + product.definition.id);
        Debug.Log("Product Name: " + product.metadata.localizedTitle);
        Debug.Log("Product Info: " + product.metadata.localizedDescription);
#if UNITY_IOS
        string transactionId = purchaseEvent.purchasedProduct.receipt;

        apiHolder.SendApplePrWrapper(product.definition.id, transactionId);
#endif

#if UNITY_ANDROID
                    string transactionId = purchaseEvent.purchasedProduct.transactionID;

                apiHolder.SendGooglePrWrapper(product.definition.id, transactionId);
                        Debug.Log(transactionId);

#endif


        // You might want to perform additional actions here based on the purchase.

        // Indicate that the purchase was successful.
        return PurchaseProcessingResult.Complete;
    }
    public void PurchaseProduct(string pId)
    {
        // Check if the Purchasing system is initialized.
        if (IsInitialized())
        {

            // Make the purchase.
            Product product = m_StoreController.products.WithID(pId);
            Debug.Log(product);
            //apiHolder.SendCheckOutWrap(product.definition.id);
            if (product != null && product.availableToPurchase)
            {
                m_StoreController.InitiatePurchase(product,"suerId="+ProfileInfo.Player.id);
            }
        }
    }

}
