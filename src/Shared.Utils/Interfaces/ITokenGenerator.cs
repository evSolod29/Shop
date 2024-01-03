using System;

namespace Shared.Utils.Interfaces
{
    public interface ITokenGenerator
    {
        public string Generate(string userName, IEnumerable<string> roles);
    }
}
