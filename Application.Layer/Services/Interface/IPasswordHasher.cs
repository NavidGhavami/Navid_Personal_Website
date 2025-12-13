namespace Application.Layer.Services.Interface
{
    public interface IPasswordHasher
    {
        string EncodePasswordMd5(string pass);
    }
}
