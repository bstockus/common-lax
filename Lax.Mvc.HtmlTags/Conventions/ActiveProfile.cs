using System.Collections.Generic;

namespace Lax.Mvc.HtmlTags.Conventions {

    public class ActiveProfile {

        private readonly Stack<string> _profiles = new Stack<string>();

        public ActiveProfile() => _profiles.Push(TagConstants.Default);

        public string Name => _profiles.Peek();

        public void Push(string profile) => _profiles.Push(profile);

        public void Pop() => _profiles.Pop();

    }

}