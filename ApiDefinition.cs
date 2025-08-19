using System;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace SDWebImage {
    
    // SDImageCacheOptions enum definition
    [Native]
    public enum SDImageCacheOptions : nuint {
        /// <summary>
        /// By default, we do not query image data when the image is already cached in memory. This mask can force to query image data at the same time.
        /// </summary>
        QueryMemoryData = 1 << 0,
        
        /// <summary>
        /// By default, when you only specify SDImageCacheQueryMemoryData, we query the memory image data asynchronously. Combined this mask as well to query the memory image data synchronously.
        /// </summary>
        QueryMemoryDataSync = 1 << 1,
        
        /// <summary>
        /// By default, when the memory cache miss, we query the disk cache asynchronously. This mask can force to query disk cache (when memory cache miss) synchronously.
        /// </summary>
        QueryDiskDataSync = 1 << 2,
        
        /// <summary>
        /// By default, images are decoded respecting their original size. On iOS, this flag will scale down the images to a size compatible with the constrained memory of devices.
        /// </summary>
        ScaleDownLargeImages = 1 << 3,
        
        /// <summary>
        /// By default, we will decode the image in the background during cache query and download from the network.
        /// </summary>
        [Obsolete("Use SDWebImageContextImageForceDecodePolicy instead")]
        AvoidDecodeImage = 1 << 4,
        
        /// <summary>
        /// By default, we decode the animated image. This flag can force decode the first frame only and produce the static image.
        /// </summary>
        DecodeFirstFrameOnly = 1 << 5,
        
        /// <summary>
        /// By default, for SDAnimatedImage, we decode the animated image frame during rendering to reduce memory usage.
        /// </summary>
        PreloadAllFrames = 1 << 6,
        
        /// <summary>
        /// By default, when you use SDWebßImageContextAnimatedImageClass, we may still use UIImage when the memory cache hit, or image decoder is not available.
        /// </summary>
        MatchAnimatedImageClass = 1 << 7
    }
    
    // typedef void (^SDExternalCompletionBlock)(UIImage * _Nullable, NSError * _Nullable, SDImageCacheType, NSURL * _Nullable);
    delegate void SDExternalCompletionHandler ([NullAllowed] UIImage image, [NullAllowed] NSError error, SDImageCacheType cacheType, [NullAllowed] NSUrl imageUrl);

    // typedef void (^SDImageLoaderProgressBlock)(NSInteger receivedSize, NSInteger expectedSize, NSURL * _Nullable targetURL);
    delegate void SDImageLoaderProgressHandler (nint receivedSize, nint expectedSize, [NullAllowed] NSUrl targetUrl);

    // typedef void (^SDInternalCompletionBlock)(UIImage * _Nullable, NSData * _Nullable, NSError * _Nullable, SDImageCacheType, BOOL, NSURL * _Nullable);
    delegate void SDInternalCompletionHandler ([NullAllowed] UIImage image, [NullAllowed] NSData data, [NullAllowed] NSError error, SDImageCacheType cacheType, bool finished, [NullAllowed] NSUrl imageUrl);

    
	[Category]
	[BaseType (typeof (UIImageView))]
	interface UIImageView_WebCache {
        
        // @property (nonatomic, strong, readonly, nullable) NSURL *sd_currentImageURL;
        [Export ("sd_currentImageURL")]
        [NullAllowed]
        NSUrl CurrentImageUrl { get; }

		// -(void)sd_setImageWithURL:(NSURL * _Nullable)url;
		[Export ("sd_setImageWithURL:")]
		void SetImage ([NullAllowed] NSUrl url);

		// -(void)sd_setImageWithURL:(NSURL * _Nullable)url placeholderImage:(UIImage * _Nullable)placeholder;
		[Export ("sd_setImageWithURL:placeholderImage:")]
		void SetImage ([NullAllowed] NSUrl url, [NullAllowed] UIImage placeholder);
        
        // -(void)sd_setImageWithURL:(NSURL * _Nullable)url placeholderImage:(UIImage * _Nullable)placeholder options:(SDWebImageOptions)options;
        [Export ("sd_setImageWithURL:placeholderImage:options:")]
        void SetImage ([NullAllowed] NSUrl url, [NullAllowed] UIImage placeholder, SDWebImageOptions options);

        // -(void)sd_setImageWithURL:(NSURL * _Nullable)url placeholderImage:(UIImage * _Nullable)placeholder options:(SDWebImageOptions)options context:(nullable SDWebImageContext *)context;
        [Export ("sd_setImageWithURL:placeholderImage:options:context:")]
        void SetImage ([NullAllowed] NSUrl url, [NullAllowed] UIImage placeholder, SDWebImageOptions options, [NullAllowed] NSDictionary context);

        // -(void)sd_setImageWithURL:(NSURL * _Nullable)url completed:(SDExternalCompletionBlock _Nullable)completedBlock;
        [Export ("sd_setImageWithURL:completed:")]
        void SetImage ([NullAllowed] NSUrl url, [NullAllowed] SDExternalCompletionHandler completedHandler);
        
        // -(void)sd_setImageWithURL:(NSURL * _Nullable)url placeholderImage:(UIImage * _Nullable)placeholder completed:(SDExternalCompletionBlock _Nullable)completedBlock;
        [Export ("sd_setImageWithURL:placeholderImage:completed:")]
        void SetImage ([NullAllowed] NSUrl url, [NullAllowed] UIImage placeholder, [NullAllowed] SDExternalCompletionHandler completedHandler);

        // -(void)sd_setImageWithURL:(NSURL * _Nullable)url placeholderImage:(UIImage * _Nullable)placeholder options:(SDWebImageOptions)options completed:(SDExternalCompletionBlock _Nullable)completedBlock;
        [Export ("sd_setImageWithURL:placeholderImage:options:completed:")]
        void SetImage ([NullAllowed] NSUrl url, [NullAllowed] UIImage placeholder, SDWebImageOptions options, [NullAllowed] SDExternalCompletionHandler completedHandler);

        // -(void)sd_setImageWithURL:(NSURL * _Nullable)url placeholderImage:(UIImage * _Nullable)placeholder options:(SDWebImageOptions)options progress:(nullable SDImageLoaderProgressBlock)progressBlock completed:(nullable SDExternalCompletionBlock)completedBlock;
        [Export ("sd_setImageWithURL:placeholderImage:options:progress:completed:")]
        void SetImage ([NullAllowed] NSUrl url, [NullAllowed] UIImage placeholder, SDWebImageOptions options, [NullAllowed] SDImageLoaderProgressHandler progressHandler, [NullAllowed] SDExternalCompletionHandler completedHandler);

        // -(void)sd_setImageWithURL:(NSURL * _Nullable)url placeholderImage:(UIImage * _Nullable)placeholder options:(SDWebImageOptions)options context:(nullable SDWebImageContext *)context progress:(nullable SDImageLoaderProgressBlock)progressBlock completed:(nullable SDExternalCompletionBlock)completedBlock;
        [Export ("sd_setImageWithURL:placeholderImage:options:context:progress:completed:")]
        void SetImage ([NullAllowed] NSUrl url, [NullAllowed] UIImage placeholder, SDWebImageOptions options, [NullAllowed] NSDictionary context, [NullAllowed] SDImageLoaderProgressHandler progressHandler, [NullAllowed] SDExternalCompletionHandler completedHandler);

        // -(void)sd_cancelCurrentImageLoad;
        [Export ("sd_cancelCurrentImageLoad")]
        void CancelCurrentImageLoad ();
	}
    
    
    // @interface WebCache (UIView)
    [Category]
    [BaseType(typeof(UIView))]
    interface UIView_WebCache
    {
        // -(void)sd_cancelCurrentImageLoad;
        [Export("sd_cancelCurrentImageLoad")]
        void CancelCurrentImageLoad();
    }
    
    
    // @protocol SDWebImageManagerDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject))]
    interface SDWebImageManagerDelegate {
    
        // @optional -(BOOL)imageManager:(SDWebImageManager *)imageManager shouldDownloadImageForURL:(NSURL *)imageURL;
        [Export ("imageManager:shouldDownloadImageForURL:")]
        bool ShouldDownloadImageForURL (SDWebImageManager imageManager, NSUrl imageURL);

        // @optional -(BOOL)imageManager:(nonnull SDWebImageManager *)imageManager shouldBlockFailedURL:(nonnull NSURL *)imageURL withError:(nonnull NSError *)error;
        [Export ("imageManager:shouldBlockFailedURL:withError:")]
        bool ShouldBlockFailedUrl (SDWebImageManager imageManager, NSUrl imageUrl, NSError error);
    }

    // @interface SDWebImageCombinedOperation : NSObject <SDWebImageOperation>
    [BaseType (typeof (NSObject))]
    interface SDWebImageCombinedOperation : SDWebImageOperation {

        // @property (nonatomic, assign, readonly, getter=isCancelled) BOOL cancelled;
        [Export ("cancelled")]
        bool IsCancelled { [Bind ("isCancelled")] get; }

        // @property (strong, nonatomic, nullable, readonly) id<SDWebImageOperation> cacheOperation;
        [Export ("cacheOperation")]
        [NullAllowed]
        SDWebImageOperation CacheOperation { get; }

        // @property (strong, nonatomic, nullable, readonly) id<SDWebImageOperation> loaderOperation;
        [Export ("loaderOperation")]
        [NullAllowed]
        SDWebImageOperation LoaderOperation { get; }
    }
    
    // @interface SDWebImageDownloadToken : NSObject <SDWebImageOperation>
    [BaseType (typeof (NSObject))]
    interface SDWebImageDownloadToken : SDWebImageOperation {

        // @property (nonatomic, strong, nullable, readonly) NSURL *url;
        [Export ("url")]
        [NullAllowed]
        NSUrl Url { get; }

        // @property (nonatomic, strong, nullable, readonly) NSURLRequest *request;
        [Export ("request")]
        [NullAllowed]
        NSUrlRequest Request { get; }

        // @property (nonatomic, strong, nullable, readonly) NSURLResponse *response;
        [Export ("response")]
        [NullAllowed]
        NSUrlResponse Response { get; }
    }

    // @interface SDWebImageDownloader : NSObject
	[BaseType (typeof (NSObject))]
	interface SDWebImageDownloader {
	
        // @property (nonatomic, copy, readonly, nonnull) SDWebImageDownloaderConfig *config;
        [Export ("config", ArgumentSemantic.Copy)]
        NSObject Config { get; }

        // @property (nonatomic, assign, getter=isSuspended) BOOL suspended;
        [Export ("suspended")]
        bool IsSuspended { [Bind ("isSuspended")] get; set; }

        // @property (nonatomic, assign, readonly) NSUInteger currentDownloadCount;
        [Export ("currentDownloadCount")]
        nuint CurrentDownloadCount { get; }

        // @property (nonatomic, readonly, nonnull) NSURLSessionConfiguration *sessionConfiguration;
        [Export ("sessionConfiguration")]
        NSUrlSessionConfiguration SessionConfiguration { get; }

        // @property (nonatomic, class, readonly, nonnull) SDWebImageDownloader *sharedDownloader;
        [Static, Export ("sharedDownloader")]
        SDWebImageDownloader SharedDownloader { get; }

        // -(nonnull instancetype)initWithConfig:(nullable SDWebImageDownloaderConfig *)config;
        [Export ("initWithConfig:")]
        [DesignatedInitializer]
        IntPtr Constructor ([NullAllowed] NSObject config);

        // -(void)setValue:(nullable NSString *)value forHTTPHeaderField:(nullable NSString *)field;
        [Export ("setValue:forHTTPHeaderField:")]
        void SetValue ([NullAllowed] string value, [NullAllowed] string field);

        // -(nullable NSString *)valueForHTTPHeaderField:(nullable NSString *)field;
        [Export ("valueForHTTPHeaderField:")]
        [return: NullAllowed]
        string ValueForHTTPHeaderField ([NullAllowed] string field);

        // -(nullable SDWebImageDownloadToken *)downloadImageWithURL:(nullable NSURL *)url completed:(nullable SDWebImageDownloaderCompletedBlock)completedBlock;
        [Export ("downloadImageWithURL:completed:")]
        [return: NullAllowed]
        SDWebImageDownloadToken DownloadImageWithURL ([NullAllowed] NSUrl url, [NullAllowed] Action<UIImage, NSData, NSError, bool> completedBlock);

        // -(nullable SDWebImageDownloadToken *)downloadImageWithURL:(nullable NSURL *)url options:(SDWebImageDownloaderOptions)options progress:(nullable SDWebImageDownloaderProgressBlock)progressBlock completed:(nullable SDWebImageDownloaderCompletedBlock)completedBlock;
        [Export ("downloadImageWithURL:options:progress:completed:")]
        [return: NullAllowed]
        SDWebImageDownloadToken DownloadImageWithURL ([NullAllowed] NSUrl url, SDWebImageDownloaderOptions options, [NullAllowed] SDImageLoaderProgressHandler progressBlock, [NullAllowed] Action<UIImage, NSData, NSError, bool> completedBlock);

        // -(nullable SDWebImageDownloadToken *)downloadImageWithURL:(nullable NSURL *)url options:(SDWebImageDownloaderOptions)options context:(nullable SDWebImageContext *)context progress:(nullable SDWebImageDownloaderProgressBlock)progressBlock completed:(nullable SDWebImageDownloaderCompletedBlock)completedBlock;
        [Export ("downloadImageWithURL:options:context:progress:completed:")]
        [return: NullAllowed]
        SDWebImageDownloadToken DownloadImageWithURL ([NullAllowed] NSUrl url, SDWebImageDownloaderOptions options, [NullAllowed] NSDictionary context, [NullAllowed] SDImageLoaderProgressHandler progressBlock, [NullAllowed] Action<UIImage, NSData, NSError, bool> completedBlock);

        // -(void)cancelAllDownloads;
        [Export ("cancelAllDownloads")]
        void CancelAllDownloads ();

        // -(void)invalidateSessionAndCancel:(BOOL)cancelPendingOperations;
        [Export ("invalidateSessionAndCancel:")]
        void InvalidateSessionAndCancel (bool cancelPendingOperations);
	}

	// SDWebImageDownloaderOperation - 这个类在最新版本中已被移除，删除此定义

    // @protocol SDWebImageOperation <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject))]
    interface SDWebImageOperation {
    
        // @required -(void)cancel;
        [Export ("cancel")]
        [Abstract]
        void Cancel ();
    }
    
   // @interface SDWebImageManager : NSObject
	[BaseType (typeof (NSObject))]
	interface SDWebImageManager {
 
		// @property (weak, nonatomic, nullable) id <SDWebImageManagerDelegate> delegate;
		[Export ("delegate", ArgumentSemantic.Weak)]
		[NullAllowed]
		NSObject WeakDelegate { get; set; }
 
		// @property (weak, nonatomic, nullable) id <SDWebImageManagerDelegate> delegate;
		[Wrap ("WeakDelegate")]
		SDWebImageManagerDelegate Delegate { get; set; }
 
		// @property (strong, nonatomic, readonly, nonnull) id<SDImageCache> imageCache;
		[Export ("imageCache")]
		NSObject ImageCache { get; }
 
		// @property (strong, nonatomic, readonly, nonnull) id<SDImageLoader> imageLoader;
		[Export ("imageLoader")]
		NSObject ImageLoader { get; }

        // @property (strong, nonatomic, nullable) id<SDImageTransformer> transformer;
        [Export ("transformer", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSObject Transformer { get; set; }

        // @property (nonatomic, strong, nullable) id<SDWebImageCacheKeyFilter> cacheKeyFilter;
        [Export ("cacheKeyFilter", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSObject CacheKeyFilter { get; set; }

        // @property (nonatomic, strong, nullable) id<SDWebImageCacheSerializer> cacheSerializer;
        [Export ("cacheSerializer", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSObject CacheSerializer { get; set; }

        // @property (nonatomic, strong, nullable) id<SDWebImageOptionsProcessor> optionsProcessor;
        [Export ("optionsProcessor", ArgumentSemantic.Strong)]
        [NullAllowed]
        NSObject OptionsProcessor { get; set; }

        // @property (nonatomic, assign, readonly, getter=isRunning) BOOL running;
        [Export ("running")]
        bool IsRunning { [Bind ("isRunning")] get; }

        // @property (nonatomic, class, nullable) id<SDImageCache> defaultImageCache;
        [Static]
        [Export ("defaultImageCache")]
        [NullAllowed]
        NSObject DefaultImageCache { get; set; }

        // @property (nonatomic, class, nullable) id<SDImageLoader> defaultImageLoader;
        [Static]
        [Export ("defaultImageLoader")]
        [NullAllowed]
        NSObject DefaultImageLoader { get; set; }

        // @property (nonatomic, class, readonly, nonnull) SDWebImageManager *sharedManager;
        [Static]
        [Export ("sharedManager")]
        SDWebImageManager SharedManager { get; }

        // -(nonnull instancetype)initWithCache:(nonnull id<SDImageCache>)cache loader:(nonnull id<SDImageLoader>)loader;
        [Export ("initWithCache:loader:")]
        [DesignatedInitializer]
        IntPtr Constructor (NSObject cache, NSObject loader);

        // -(nullable SDWebImageCombinedOperation *)loadImageWithURL:(nullable NSURL *)url options:(SDWebImageOptions)options progress:(nullable SDImageLoaderProgressBlock)progressBlock completed:(nonnull SDInternalCompletionBlock)completedBlock;
        [Export ("loadImageWithURL:options:progress:completed:")]
        [return: NullAllowed]
        SDWebImageCombinedOperation LoadImageWithURL ([NullAllowed] NSUrl url, SDWebImageOptions options, [NullAllowed] SDImageLoaderProgressHandler progressBlock, SDInternalCompletionHandler completedBlock);

        // -(nullable SDWebImageCombinedOperation *)loadImageWithURL:(nullable NSURL *)url options:(SDWebImageOptions)options context:(nullable SDWebImageContext *)context progress:(nullable SDImageLoaderProgressBlock)progressBlock completed:(nonnull SDInternalCompletionBlock)completedBlock;
        [Export ("loadImageWithURL:options:context:progress:completed:")]
        [return: NullAllowed]
        SDWebImageCombinedOperation LoadImageWithURL ([NullAllowed] NSUrl url, SDWebImageOptions options, [NullAllowed] NSDictionary context, [NullAllowed] SDImageLoaderProgressHandler progressBlock, SDInternalCompletionHandler completedBlock);

        // -(void)cancelAll;
        [Export ("cancelAll")]
        void CancelAll ();

        // -(void)removeFailedURL:(nonnull NSURL *)url;
        [Export ("removeFailedURL:")]
        void RemoveFailedUrl (NSUrl url);

        // -(void)removeAllFailedURLs;
        [Export ("removeAllFailedURLs")]
        void RemoveAllFailedUrls ();

        // -(nullable NSString *)cacheKeyForURL:(nullable NSURL *)url;
        [Export ("cacheKeyForURL:")]
        [return: NullAllowed]
        string CacheKeyForURL ([NullAllowed] NSUrl url);

        // -(nullable NSString *)cacheKeyForURL:(nullable NSURL *)url context:(nullable SDWebImageContext *)context;
        [Export ("cacheKeyForURL:context:")]
        [return: NullAllowed]
        string CacheKeyForURL ([NullAllowed] NSUrl url, [NullAllowed] NSDictionary context);
	}

    // @interface SDImageCacheToken : NSObject <SDWebImageOperation>
    [BaseType (typeof (NSObject))]
    interface SDImageCacheToken : SDWebImageOperation {

        // @property (nonatomic, strong, nullable, readonly) NSString *key;
        [Export ("key")]
        [NullAllowed]
        string Key { get; }
    }
 
    // @interface SDImageCache : NSObject
	[BaseType (typeof (NSObject))]
	interface SDImageCache {

        // @property (nonatomic, copy, nonnull, readonly) SDImageCacheConfig *config;
        [Export ("config", ArgumentSemantic.Copy)]
        NSObject Config { get; }

        // @property (nonatomic, strong, readonly, nonnull) id<SDMemoryCache> memoryCache;
        [Export ("memoryCache")]
        NSObject MemoryCache { get; }

        // @property (nonatomic, strong, readonly, nonnull) id<SDDiskCache> diskCache;
        [Export ("diskCache")]
        NSObject DiskCache { get; }

        // @property (nonatomic, copy, nonnull, readonly) NSString *diskCachePath;
        [Export ("diskCachePath")]
        string DiskCachePath { get; }

        // @property (nonatomic, class, readonly, nonnull) SDImageCache *sharedImageCache;
        [Static]
        [Export ("sharedImageCache")]
        SDImageCache SharedImageCache { get; }

        // @property (nonatomic, class, readwrite, null_resettable) NSString *defaultDiskCacheDirectory;
        [Static]
        [Export ("defaultDiskCacheDirectory")]
        [NullAllowed]
        string DefaultDiskCacheDirectory { get; set; }

        // -(nonnull instancetype)initWithNamespace:(nonnull NSString *)ns;
        [Export ("initWithNamespace:")]
        IntPtr Constructor (string ns);

        // -(nonnull instancetype)initWithNamespace:(nonnull NSString *)ns diskCacheDirectory:(nullable NSString *)directory;
        [Export ("initWithNamespace:diskCacheDirectory:")]
        IntPtr Constructor (string ns, [NullAllowed] string directory);

        // -(nonnull instancetype)initWithNamespace:(nonnull NSString *)ns diskCacheDirectory:(nullable NSString *)directory config:(nullable SDImageCacheConfig *)config;
        [Export ("initWithNamespace:diskCacheDirectory:config:")]
        [DesignatedInitializer]
        IntPtr Constructor (string ns, [NullAllowed] string directory, [NullAllowed] NSObject config);

        // -(nullable NSString *)cachePathForKey:(nullable NSString *)key;
        [Export ("cachePathForKey:")]
        [return: NullAllowed]
        string CachePathForKey ([NullAllowed] string key);

        // -(void)storeImage:(nullable UIImage *)image forKey:(nullable NSString *)key completion:(nullable SDWebImageNoParamsBlock)completionBlock;
        [Export ("storeImage:forKey:completion:")]
        void StoreImage ([NullAllowed] UIImage image, [NullAllowed] string key, [NullAllowed] Action completionBlock);

        // -(void)storeImage:(nullable UIImage *)image forKey:(nullable NSString *)key toDisk:(BOOL)toDisk completion:(nullable SDWebImageNoParamsBlock)completionBlock;
        [Export ("storeImage:forKey:toDisk:completion:")]
        void StoreImage ([NullAllowed] UIImage image, [NullAllowed] string key, bool toDisk, [NullAllowed] Action completionBlock);

        // -(void)storeImageData:(nullable NSData *)imageData forKey:(nullable NSString *)key completion:(nullable SDWebImageNoParamsBlock)completionBlock;
        [Export ("storeImageData:forKey:completion:")]
        void StoreImageData ([NullAllowed] NSData imageData, [NullAllowed] string key, [NullAllowed] Action completionBlock);

        // -(void)storeImage:(nullable UIImage *)image imageData:(nullable NSData *)imageData forKey:(nullable NSString *)key toDisk:(BOOL)toDisk completion:(nullable SDWebImageNoParamsBlock)completionBlock;
        [Export ("storeImage:imageData:forKey:toDisk:completion:")]
        void StoreImage ([NullAllowed] UIImage image, [NullAllowed] NSData imageData, [NullAllowed] string key, bool toDisk, [NullAllowed] Action completionBlock);

        // -(void)storeImageToMemory:(nullable UIImage*)image forKey:(nullable NSString *)key;
        [Export ("storeImageToMemory:forKey:")]
        void StoreImageToMemory ([NullAllowed] UIImage image, [NullAllowed] string key);

        // -(void)storeImageDataToDisk:(nullable NSData *)imageData forKey:(nullable NSString *)key;
        [Export ("storeImageDataToDisk:forKey:")]
        void StoreImageDataToDisk ([NullAllowed] NSData imageData, [NullAllowed] string key);

        // -(void)diskImageExistsWithKey:(nullable NSString *)key completion:(nullable SDImageCacheCheckCompletionBlock)completionBlock;
        [Export ("diskImageExistsWithKey:completion:")]
        void DiskImageExistsWithKey ([NullAllowed] string key, [NullAllowed] Action<bool> completionBlock);

        // -(BOOL)diskImageDataExistsWithKey:(nullable NSString *)key;
        [Export ("diskImageDataExistsWithKey:")]
        bool DiskImageDataExistsWithKey ([NullAllowed] string key);

        // -(nullable NSData *)diskImageDataForKey:(nullable NSString *)key;
        [Export ("diskImageDataForKey:")]
        [return: NullAllowed]
        NSData DiskImageDataForKey ([NullAllowed] string key);

        // -(nullable SDImageCacheToken *)queryCacheOperationForKey:(nullable NSString *)key done:(nullable SDImageCacheQueryCompletionBlock)doneBlock;
        [Export ("queryCacheOperationForKey:done:")]
        [return: NullAllowed]
        SDImageCacheToken QueryCacheOperationForKey ([NullAllowed] string key, [NullAllowed] Action<UIImage, NSData, SDImageCacheType> doneBlock);

        // -(nullable SDImageCacheToken *)queryCacheOperationForKey:(nullable NSString *)key options:(SDImageCacheOptions)options done:(nullable SDImageCacheQueryCompletionBlock)doneBlock;
        [Export ("queryCacheOperationForKey:options:done:")]
        [return: NullAllowed]
        SDImageCacheToken QueryCacheOperationForKey ([NullAllowed] string key, SDImageCacheOptions options, [NullAllowed] Action<UIImage, NSData, SDImageCacheType> doneBlock);

        // -(nullable UIImage *)imageFromMemoryCacheForKey:(nullable NSString *)key;
        [Export ("imageFromMemoryCacheForKey:")]
        [return: NullAllowed]
        UIImage ImageFromMemoryCacheForKey ([NullAllowed] string key);

        // -(nullable UIImage *)imageFromDiskCacheForKey:(nullable NSString *)key;
        [Export ("imageFromDiskCacheForKey:")]
        [return: NullAllowed]
        UIImage ImageFromDiskCacheForKey ([NullAllowed] string key);

        // -(nullable UIImage *)imageFromCacheForKey:(nullable NSString *)key;
        [Export ("imageFromCacheForKey:")]
        [return: NullAllowed]
        UIImage ImageFromCacheForKey ([NullAllowed] string key);

        // -(void)removeImageForKey:(nullable NSString *)key withCompletion:(nullable SDWebImageNoParamsBlock)completion;
        [Export ("removeImageForKey:withCompletion:")]
        void RemoveImageForKey ([NullAllowed] string key, [NullAllowed] Action completion);

        // -(void)removeImageForKey:(nullable NSString *)key fromDisk:(BOOL)fromDisk withCompletion:(nullable SDWebImageNoParamsBlock)completion;
        [Export ("removeImageForKey:fromDisk:withCompletion:")]
        void RemoveImageForKey ([NullAllowed] string key, bool fromDisk, [NullAllowed] Action completion);

        // -(void)removeImageFromMemoryForKey:(nullable NSString *)key;
        [Export ("removeImageFromMemoryForKey:")]
        void RemoveImageFromMemoryForKey ([NullAllowed] string key);

        // -(void)removeImageFromDiskForKey:(nullable NSString *)key;
        [Export ("removeImageFromDiskForKey:")]
        void RemoveImageFromDiskForKey ([NullAllowed] string key);

        // -(void)clearMemory;
        [Export ("clearMemory")]
        void ClearMemory ();

        // -(void)clearDiskOnCompletion:(nullable SDWebImageNoParamsBlock)completion;
        [Export ("clearDiskOnCompletion:")]
        void ClearDiskOnCompletion ([NullAllowed] Action completion);

        // -(void)deleteOldFilesWithCompletionBlock:(nullable SDWebImageNoParamsBlock)completionBlock;
        [Export ("deleteOldFilesWithCompletionBlock:")]
        void DeleteOldFilesWithCompletionBlock ([NullAllowed] Action completionBlock);

        // -(NSUInteger)totalDiskSize;
        [Export ("totalDiskSize")]
        nuint TotalDiskSize ();

        // -(NSUInteger)totalDiskCount;
        [Export ("totalDiskCount")]
        nuint TotalDiskCount ();

        // -(void)calculateSizeWithCompletionBlock:(nullable SDImageCacheCalculateSizeBlock)completionBlock;
        [Export ("calculateSizeWithCompletionBlock:")]
        void CalculateSizeWithCompletionBlock ([NullAllowed] Action<nuint, nuint> completionBlock);
	}
}
