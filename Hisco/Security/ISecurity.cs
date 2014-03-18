namespace Hisco.Security
{
    public interface ISecurity
    {
        string GenerateHash(string[] keys);
    }
}