using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using Unity.Services.Core;
public class paymentVars : MonoBehaviour
{
    public static paymentVars Instance { get; private set; }
    public static pymentItems payments;
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    // void Awake()
    // {
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }

//     void Start()
//     {
//         APIcalls.onPaymentReady += InitializeOnPaymentReady;
        
//     }
//     void OnDestroy()
//     {
//         // Unsubscribe to prevent memory leaks
//         APIcalls.onPaymentReady -= InitializeOnPaymentReady;
//     }

//     private void InitializeOnPaymentReady()
//     {
//         // Check if IAP is already initialized to prevent re-initialization
//         if (!IsInitialized())
//         {
//             InitializeUnityServices();
//         }

//         // Optionally unsubscribe if initialization should only happen once
//         // apicalls.onPaymentReady -= InitializeOnPaymentReady;
//     }


// private async void InitializeUnityServices()
//     {
//         try
//         {
//             await UnityServices.InitializeAsync();
//             Debug.Log("Unity Services initialized successfully.");
//             InitializePurchasing();
//         }
//         catch (System.Exception e)
//         {
//             Debug.LogError($"Failed to initialize Unity Services: {e}");
//             // Handle any additional logic for a failed initialization
//         }
//     }


//     public void InitializePurchasing()
//     {
//         if (IsInitialized())
//         {
//             return;
//         }

//         var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

//         // Add each product from payments
//         foreach (paymentItem item in payments.items)
//         {
//             builder.AddProduct(item.id, ProductType.Consumable);
//             // If you have different product types, you need to adjust this accordingly
//         }

//         UnityPurchasing.Initialize(this, builder);
//     }

//     private bool IsInitialized()
//     {
//         return m_StoreController != null && m_StoreExtensionProvider != null;
//     }

//     public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//     {
//         m_StoreController = controller;
//         m_StoreExtensionProvider = extensions;
//         // Handle any initialization logic here
//     }

//     public void OnInitializeFailed(InitializationFailureReason error)
//     {
//         // Handle initialization failure here
//     }

 

//     // Process a successful purchase
//     public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
//     {
//         // Here, you should handle the successful purchase, e.g., unlocking a feature or delivering a product
//         Debug.Log("Purchase successful: " + args.purchasedProduct.definition.id);
        
//         // Return a flag indicating whether this product has completely been handled, or if there's more processing to do
//         return PurchaseProcessingResult.Complete;
//     }

//     // Handle a failed purchase
//     public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
//     {
//         // Here, you should handle the failed purchase
//         Debug.Log("Purchase failed: " + product.definition.id + ", Reason: " + failureReason);
//     }

//     public void OnInitializeFailed(InitializationFailureReason error, string message)
//     {
//         throw new System.NotImplementedException();
//     }


    // Implement other IStoreListener methods here...
    // For example, OnPurchaseFailed, OnPurchaseComplete etc.

}

// Define your paymentItem class if not already defined

