namespace KarolinkaUznani.Common.Auth.JWT
{
    public interface IJwtHandler
    {
        JsonWebToken Create<T>(T userId);
    }
}