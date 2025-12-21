namespace Application.Layer.Utilities
{
    public static class PathExtension
    {
        #region domain Address

        public static string DomainAddress = "https://jibicenter.com";


        #endregion

        #region Default Images

        public static string DefaultProfile = "/Theme/assets/image/user-profile.png";
        public static string NoImage = "/Theme/assets/image/no-image.png";


        #endregion

        #region Blog

        public static string ArticleCategoryOrigin = "/Content/Images/ArticleCategory/Origin/";
        public static string ArticleCategoryOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/ArticleCategory/Origin/");
                             
        public static string ArticleCategoryThumb = "/Content/Images/ArticleCategory/Thumb/";
        public static string ArticleCategoryThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/ArticleCategory/Thumb/");


        public static string ArticleOrigin = "/Content/Images/Article/Origin/";
        public static string ArticleOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/Article/Origin/");

        public static string ArticleThumb = "/Content/Images/Article/Thumb/";
        public static string ArticleThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/Article/Thumb/");

        #endregion

        #region Uploader

        public static string UploaderImage = "/Theme/assets/ckEditorImageUpload/";
        public static string UploaderImageServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Theme/assets/ckEditorImageUpload/");


        #endregion

        #region User Profile

        public static string UserProfileOrigin = "/Content/Images/UserProfile/Origin/";
        public static string UserProfileOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/UserProfile/Origin/");

        public static string UserProfileThumb = "/Content/Images/UserProfile/Thumb/";
        public static string UserProfileThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/UserProfile/Thumb/");

        #endregion


    }
}
